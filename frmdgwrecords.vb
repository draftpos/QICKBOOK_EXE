Imports System.Data.SqlClient

Public Class frmdgwrecords
    Private Sub frmdgwrecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now.Date.ToString()
        DateTimePicker2.Value = Now.Date.ToString()
        LoadData("", "")
        Dim query As String = "SELECT DISTINCT [Currency] FROM Invoice"
        dt = Crud(query, Nothing)
        ComboBox1.Items.Clear()
        For Each row As DataRow In dt.Rows
            ComboBox1.Items.Add(row("Currency").ToString())
        Next
        ' InitializeRefreshTimer()
    End Sub
    Private Sub frm_company_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.Show()
    End Sub

    Function LoadData(datap As String, currency As String) As Boolean
        dgw.Rows.Clear()
        Dim sql As String
        sql = "
    SELECT 
        i.[id],
        i.[TxnId],
        i.[CustomerName],
        i.[TxnDate],
        i.[CustomerListId],
        i.[Amount],
        i.[AppliedAmount],
        i.[Subtotal],
        i.[SalesTaxPercentage],
        i.[SalesTaxTotal],
        i.[BalanceRemaining],
        i.[Currency],
        i.[ExchangeRate],
        i.[BalanceRemainingInHomeCurrency],
        i.[InvoiceNumber],
        i.[HavanoZimraStatus],
        SUM(CASE WHEN LOWER(it.vat) = 's' or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount ELSE 0 END) AS TotalVat,
        SUM(it.Amount) AS Total,
        SUM(it.Amount) - SUM(CASE WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount ELSE 0 END) AS Total_Exclusive
    FROM [Invoice] i
    INNER JOIN [Item] it ON it.TxnId = i.TxnId
    WHERE i.[TxnDate] >= @StartDate AND i.[TxnDate] <= @EndDate "
        ' Add filters for SearchText if provided
        If Not String.IsNullOrWhiteSpace(datap) Then
            sql += " AND (i.[InvoiceNumber] = @SearchText OR i.[TxnId] LIKE '%' + @SearchText + '%') "
        End If

        ' Add filter for Currency if provided
        If Not String.IsNullOrWhiteSpace(currency) Then
            sql += " AND i.[Currency] = @Currency "
        End If

        ' Group by Invoice fields for aggregate functions
        sql += "
        GROUP BY 
            i.[id],
            i.[TxnId],
            i.[CustomerName],
            i.[TxnDate],
            i.[CustomerListId],
            i.[Amount],
            i.[AppliedAmount],
            i.[Subtotal],
            i.[SalesTaxPercentage],
            i.[SalesTaxTotal],
            i.[BalanceRemaining],
            i.[Currency],
            i.[ExchangeRate],
            i.[BalanceRemainingInHomeCurrency],
            i.[InvoiceNumber],
            i.[HavanoZimraStatus] "

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
        Dim sumTotal, vatTotal, totalExcl As Double
        sumTotal = 0 : vatTotal = 0 : totalExcl = 0

        For Each row As DataRow In dt.Rows.Cast(Of DataRow).Reverse()
            Dim statusDesc As String = If(SafeConvertToBoolean(row("HavanoZimraStatus")), "Submitted", "Pending")
            dgw.Rows.Add(row("InvoiceNumber"), row("TxnId"), row("CustomerName"), row("TxnDate"), row("TotalVat"), row("Total_Exclusive"), row("Total"),
                     row("Currency"), statusDesc)
            sumTotal += Val(row("Total"))
            totalExcl += Val(row("Total_Exclusive"))
            vatTotal += Val(row("TotalVat"))
        Next

        lbltotalExcl.Text = totalExcl.ToString("N2")
        lbltotalsum.Text = sumTotal.ToString("N2")
        lbltotalvat.Text = vatTotal.ToString("N2")

        ' Return True if records are found, otherwise False
        Return dt.Rows.Count > 0
    End Function

    Public Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, DateTimePicker2.ValueChanged, DateTimePicker1.ValueChanged
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