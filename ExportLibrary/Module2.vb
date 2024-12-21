Imports Microsoft.Office.Interop
Imports OfficeOpenXml
Imports System.IO

Module Module2



    Public Sub ExportToExcel(ByVal dataGridView As DataGridView, ByVal saveFileDialog As SaveFileDialog)
        Try

            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx"
            saveFileDialog.Title = "Save as Excel File"

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Dim filePath As String = saveFileDialog.FileName

                Dim excelApp As New Excel.Application()
                Dim workbook As Excel.Workbook = excelApp.Workbooks.Add()
                Dim worksheet As Excel.Worksheet = CType(workbook.Sheets(1), Excel.Worksheet)
                For col As Integer = 0 To dataGridView.Columns.Count - 1
                    worksheet.Cells(1, col + 1).Value = dataGridView.Columns(col).HeaderText
                Next
                For row As Integer = 0 To dataGridView.Rows.Count - 1
                    For col As Integer = 0 To dataGridView.Columns.Count - 1
                        worksheet.Cells(row + 2, col + 1).Value = dataGridView.Rows(row).Cells(col).Value
                    Next
                Next

                ' Save the Excel file
                workbook.SaveAs(filePath)
                workbook.Close()
                excelApp.Quit()

                ' Release COM objects
                ReleaseObject(worksheet)
                ReleaseObject(workbook)
                ReleaseObject(excelApp)

                MessageBox.Show("Data exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while exporting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Release COM objects to prevent memory leaks
    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            If obj IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
                obj = Nothing
            End If
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
End Module
