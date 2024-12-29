Imports System.Data.SqlClient

Public Class frmdgwrecords
    Private Sub frmdgwrecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now.Date.ToString("dd/MM/yyyy")
        DateTimePicker2.Value = Now.Date.ToString("dd/MM/yyyy")
        LoadData("", "")
        Dim query As String = "SELECT DISTINCT [Currency] FROM Invoice"
        dt = Crud(query, Nothing)
        ComboBox1.Items.Clear()
        For Each row As DataRow In dt.Rows
            ComboBox1.Items.Add(row("Currency").ToString())
        Next
    End Sub

    Function LoadData(datap As String, currency As String) As Boolean
        dgw.Rows.Clear()
        Dim sql As String
        If Not String.IsNullOrWhiteSpace(datap) Then
            sql = "
        SELECT 
            [id],
            [TxnId],
            [CustomerName],
            [TxnDate],
            [CustomerListId],
            [Amount],
            [AppliedAmount],
            [Subtotal],
            [SalesTaxPercentage],
            [SalesTaxTotal],
            [BalanceRemaining],
            [Currency],
            [ExchangeRate],
            [BalanceRemainingInHomeCurrency],
            [InvoiceNumber]
        FROM [Invoice]
        WHERE [TxnDate] >= @StartDate AND [TxnDate] <= @EndDate
          AND ([InvoiceNumber] = @SearchText OR [TxnId] LIKE '%' + @SearchText + '%')"
        Else
            sql = "
        SELECT 
            [id],
            [TxnId],
            [CustomerName],
            [TxnDate],
            [CustomerListId],
            [Amount],
            [AppliedAmount],
            [Subtotal],
            [SalesTaxPercentage],
            [SalesTaxTotal],
            [BalanceRemaining],
            [Currency],
            [ExchangeRate],
            [BalanceRemainingInHomeCurrency],
            [InvoiceNumber]
        FROM [Invoice]
        WHERE [TxnDate] >= @StartDate AND [TxnDate] <= @EndDate"
        End If
        ' Add condition for Currency if it is supplied
        If Not String.IsNullOrWhiteSpace(currency) Then
            sql += " AND [Currency] = @Currency"
        End If
        ' Prepare parameters
        Dim parameters As New List(Of SqlParameter) From {
        New SqlParameter("@StartDate", DateTimePicker1.Value),
        New SqlParameter("@EndDate", DateTimePicker2.Value)
    }
        If Not String.IsNullOrWhiteSpace(datap) Then
            parameters.Add(New SqlParameter("@SearchText", datap))
        End If

        If Not String.IsNullOrWhiteSpace(currency) Then
            parameters.Add(New SqlParameter("@Currency", currency))
        End If

        ' Execute the query and process the results
        Dim dt As DataTable = Crud(sql, parameters)
        Dim sumtotal, vattotal, totalexcl As Double
        sumtotal = 0 : vattotal = 0 : totalexcl = 0

        For Each row As DataRow In dt.Rows
            dgw.Rows.Add(row("InvoiceNumber"), row("TxnId"), row("CustomerName"), row("TxnDate"), row("SalesTaxTotal"), row("Subtotal"), row("BalanceRemaining"),
                     row("Currency"))
            sumtotal += Val(row("BalanceRemaining"))
            totalexcl += Val(row("Subtotal"))
            vattotal += Val(row("SalesTaxTotal"))
        Next

        lbltotalExcl.Text = totalexcl.ToString()
        lbltotalsum.Text = sumtotal.ToString()
        lbltotalvat.Text = vattotal.ToString()

        ' Return True if records are found, otherwise False
        Return dt.Rows.Count > 0
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, DateTimePicker2.ValueChanged, DateTimePicker1.ValueChanged
        LoadData(TextBox1.Text, "")
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            PrintA4(TextBox1.Text.Trim())
            e.Handled = True
            TextBox1.Clear()
        End If
    End Sub

    Private Sub dgw_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgw.MouseDoubleClick
        If dgw.CurrentRow IsNot Nothing Then
            Dim column1Value As String = dgw.CurrentRow.Cells(0).Value.ToString()
            PrintA4(column1Value)
        End If
    End Sub

    Private Sub GelButton2_Click(sender As Object, e As EventArgs) Handles GelButton2.Click
        If TextBox1.Text.Trim().Length > 0 Then
            If Not PrintA4(TextBox1.Text.Trim()) Then
                dgw_MouseDoubleClick(sender, e)
            End If
        Else
            dgw_MouseDoubleClick(sender, e)
        End If
    End Sub

    Private Sub GelButton1_Click(sender As Object, e As EventArgs) Handles GelButton1.Click
        ExportToExcel(dgw, saveFileDialog)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LoadData("", ComboBox1.Text)
    End Sub
End Class