Imports System.Data.SqlClient

Public Class frmdgwrecords
    Private Sub frmdgwrecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now.Date
        DateTimePicker2.Value = Now.Date
        LoadData("")
    End Sub

    Function LoadData(datap As String)
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
        Dim parameters As New List(Of SqlParameter) From {
        New SqlParameter("@StartDate", DateTimePicker1.Value),
        New SqlParameter("@EndDate", DateTimePicker2.Value)
    }
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            parameters.Add(New SqlParameter("@SearchText", datap))
        End If
        Dim dt As DataTable = Crud(sql, parameters)
        For Each row As DataRow In dt.Rows
            dgw.Rows.Add(row("InvoiceNumber"), row("TxnId"), row("CustomerName"), row("TxnDate"), row("Subtotal"), row("BalanceRemaining"),
                 row("Currency"))
        Next
        If dt.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, DateTimePicker2.ValueChanged, DateTimePicker1.ValueChanged
        LoadData(TextBox1.Text)
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
End Class