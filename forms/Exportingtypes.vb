Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Module Exportingtypes
    Public advancesettingsvar As advancesetting = GetAdvanceSettings()
    Public Sub ExportToPDF(ByVal dataGridView As DataGridView, ByVal saveFileDialog As SaveFileDialog)
        ' Create a Document object
        Dim document As New Document()

        Try
            ' Show the SaveFileDialog to get the file path
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                ' Create a PdfWriter object to write to the specified file path
                Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(saveFileDialog.FileName, FileMode.Create))

                ' Open the document
                document.Open()

                ' Create a PdfPTable with the same number of columns as the DataGridView
                Dim pdfTable As New PdfPTable(dataGridView.ColumnCount)

                ' Add column headers from the DataGridView to the PdfPTable
                For j As Integer = 0 To dataGridView.Columns.Count - 1
                    pdfTable.AddCell(dataGridView.Columns(j).HeaderText)
                Next

                ' Add row data from the DataGridView to the PdfPTable
                For i As Integer = 0 To dataGridView.Rows.Count - 1
                    For j As Integer = 0 To dataGridView.Columns.Count - 1
                        If dataGridView.Rows(i).Cells(j).Value IsNot Nothing Then
                            pdfTable.AddCell(dataGridView.Rows(i).Cells(j).Value.ToString())
                        Else
                            pdfTable.AddCell("")
                        End If
                    Next
                Next

                ' Add the PdfPTable to the document
                document.Add(pdfTable)
            End If
        Catch ex As Exception
            ' Handle any exceptions
            MessageBox.Show("Error exporting to PDF: " & ex.Message)
        Finally
            ' Close the document
            document.Close()
        End Try
    End Sub

    Public Function GetAdvanceSettings() As advancesetting
        Dim settings As New advancesetting()
        Dim query As String = "SELECT dmarkup, phar, dispatch, CUSCHARSTAT FROM Advance_settings"
        Using con As New SqlConnection(cs)
            con.Open()
            Using cmd As New SqlCommand(query, con)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        settings.dmarkup = reader("dmarkup").ToString()
                        settings.phar = If(reader.IsDBNull(reader.GetOrdinal("phar")), False, reader.GetBoolean(reader.GetOrdinal("phar")))
                        settings.dispatch = If(reader.IsDBNull(reader.GetOrdinal("dispatch")), False, reader.GetBoolean(reader.GetOrdinal("dispatch")))
                        settings.CUSCHARSTAT = If(reader.IsDBNull(reader.GetOrdinal("CUSCHARSTAT")), False, reader.GetBoolean(reader.GetOrdinal("CUSCHARSTAT")))
                    Else
                        settings.dmarkup = ""
                        settings.phar = False
                        settings.dispatch = False
                        settings.CUSCHARSTAT = False
                    End If
                End Using
            End Using
        End Using
        Return settings
    End Function
    Sub ExportExcel(ByVal st As Object)
        Dim officeType As Type = Type.GetTypeFromProgID("Excel.Application")
        If officeType Is Nothing Then
            MessageBox.Show("Microsoft Excel is not installed in this PC.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        Try
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
            xlApp.Visible = True

            rowsTotal = st.RowCount
            colsTotal = st.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = st.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = st.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12

                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
            End With
        Catch ex As Exception
            '   MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

End Module
Public Class advancesetting
    Public Property dmarkup As String
    Public Property phar As Boolean
    Public Property dispatch As Boolean
    Public Property CUSCHARSTAT As Boolean
End Class
