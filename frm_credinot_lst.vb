

Imports System.Data.SqlClient

Public Class frm_credinot_lst
    Private Sub frm_company_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.Show()
    End Sub
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_SYSCOMMAND As Integer = &H112
        Const SC_MINIMIZE As Integer = &HF020

        ' Check if the minimize button was clicked
        If m.Msg = WM_SYSCOMMAND AndAlso m.WParam.ToInt32() = SC_MINIMIZE Then
            ' Minimize the entire application
            For Each frm As Form In Application.OpenForms
                frm.WindowState = FormWindowState.Minimized
            Next
        End If

        MyBase.WndProc(m)
    End Sub
    Private Sub frmdgwrecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now.Date.ToString("dd/MM/yyyy")
        DateTimePicker2.Value = Now.Date.ToString("dd/MM/yyyy")
        LoadData("")

    End Sub
    Dim sumTotal As Double = 0
    Dim vatTotal As Double = 0
    Dim totalExcl As Double = 0
    Function LoadData(datap As String) As Boolean
        dgw.Rows.Clear()
        sumTotal = 0
        vatTotal = 0
        totalExcl = 0
        ' Define the SQL query
        Dim sql As String = "
        SELECT 
           cm.[TxnId],
           cm.[CustomerName],
           cm.[TxnDate],
           cm.[CustomerListId],
           cm.[Amount],
           cm.[CreditNoteNumber],
           cm.[Subtotal],
           cm.[SalesTaxPercentage],
           cm.[SalesTaxTotal],
           cm.[TotalAmount],
           cm.[CreditRemaining],
           cm.[Message],
           cm.[HavanoZimraStatus],
           SUM(CASE WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount ELSE 0 END) AS TotalVat,
           SUM(it.Amount) AS Total,
           SUM(it.Amount) - SUM(CASE WHEN LOWER(it.vat) = 's'   or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount ELSE 0 END) AS Total_Exclusive
        FROM [CreditMemo] cm
        INNER JOIN [Item] it ON it.TxnId = cm.TxnId
        WHERE cm.[TxnDate] >= @StartDate AND cm.[TxnDate] <= @EndDate"

        ' Add conditions based on `datap` input
        If Not String.IsNullOrWhiteSpace(datap) Then
            sql += " AND (cm.[CreditNoteNumber] = @SearchText OR cm.[TxnId] LIKE '%' + @SearchText + '%')"
        End If

        ' Group the data for aggregate calculations
        sql += "
        GROUP BY 
           cm.[TxnId],
           cm.[CustomerName],
           cm.[TxnDate],
           cm.[CustomerListId],
           cm.[Amount],
           cm.[CreditNoteNumber],
           cm.[Subtotal],
           cm.[SalesTaxPercentage],
           cm.[SalesTaxTotal],
           cm.[TotalAmount],
           cm.[CreditRemaining],
           cm.[Message],
           cm.[HavanoZimraStatus]"

        ' Define SQL parameters
        Dim parameters As New List(Of SqlParameter) From {
        New SqlParameter("@StartDate", DateTimePicker1.Value),
        New SqlParameter("@EndDate", DateTimePicker2.Value)
    }
        If Not String.IsNullOrWhiteSpace(datap) Then
            parameters.Add(New SqlParameter("@SearchText", datap))
        End If

        ' Execute the query
        Dim dt As DataTable = Crud(sql, parameters)

        ' Initialize totals


        ' Process the results
        For Each row As DataRow In dt.Rows.Cast(Of DataRow).Reverse()
            Dim havanoStatus As Boolean = SafeConvertToBoolean(row("HavanoZimraStatus"))
            Dim statusDesc As String = If(havanoStatus, "Submitted", "Pending")

            ' Add data to DataGridView
            dgw.Rows.Add(row("CreditNoteNumber"), row("TxnId"), row("CustomerName"), row("TxnDate"),
                     row("TotalVat"), row("Total_Exclusive"), row("Total"), statusDesc)

            ' Accumulate totals
            sumTotal += Val(row("Total"))
            totalExcl += Val(row("Total_Exclusive"))
            vatTotal += Val(row("TotalVat"))
        Next
        LoadData_creditnot_on_salesinvoice(TextBox1.Text, "")
        ' Update labels
        ' lbltotalExcl.Text = totalExcl.ToString("N2")
        'lbltotalsum.Text = sumTotal.ToString("N2")
        'lbltotalvat.Text = vatTotal.ToString("N2")

        ' Return True if data exists, otherwise False
        Return dt.Rows.Count > 0
    End Function



    Public Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, DateTimePicker2.ValueChanged, DateTimePicker1.ValueChanged
        LoadData(TextBox1.Text)
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            creditnotePrintA4(TextBox1.Text.Trim())
            e.Handled = True
            TextBox1.Clear()
        End If
    End Sub

    Private Sub dgw_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgw.MouseDoubleClick
        If dgw.CurrentRow IsNot Nothing Then
            Dim column1Value As String = dgw.CurrentRow.Cells(0).Value.ToString()
            creditnotePrintA4(column1Value)
        End If
    End Sub

    Private Sub GelButton2_Click(sender As Object, e As EventArgs) Handles GelButton2.Click
        If TextBox1.Text.Trim().Length > 0 Then
            If Not creditnotePrintA4(TextBox1.Text.Trim()) Then
                dgw_MouseDoubleClick(sender, e)
            End If
        Else
            dgw_MouseDoubleClick(sender, e)
        End If
    End Sub

    Private Sub GelButton1_Click(sender As Object, e As EventArgs) Handles GelButton1.Click
        ExportToExcel(dgw, saveFileDialog)
    End Sub


















    Function LoadData_creditnot_on_salesinvoice(searchText As String, selectedCurrency As String) As Boolean
        '       dgw.Rows.Clear()
        Dim sql As String = "
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
        SUM(CASE WHEN LOWER(it.vat) = 's' OR RTRIM(it.vat) = '15' THEN (15.0 / 115.0) * it.Amount ELSE 0 END) AS TotalVat,
        SUM(it.Amount) AS Total,
        SUM(it.Amount) - SUM(CASE WHEN LOWER(it.vat) = 's' OR RTRIM(it.vat) = '15' THEN (15.0 / 115.0) * it.Amount ELSE 0 END) AS Total_Exclusive
    FROM [Invoice] i
    INNER JOIN [Item] it ON it.TxnId = i.TxnId
    WHERE i.[TxnDate] >= @StartDate AND i.[TxnDate] <= @EndDate AND i.[Subtotal] < 0"

        ' Add optional filters
        If Not String.IsNullOrWhiteSpace(searchText) Then
            sql += " AND (i.[InvoiceNumber] = @SearchText OR i.[TxnId] LIKE '%' + @SearchText + '%')"
        End If
        If Not String.IsNullOrWhiteSpace(selectedCurrency) Then
            sql += " AND i.[Currency] = @Currency"
        End If

        ' Group by Invoice fields
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
        i.[HavanoZimraStatus]"

        ' Prepare parameters
        Dim parameters As New List(Of SqlParameter) From {
        New SqlParameter("@StartDate", DateTimePicker1.Value),
        New SqlParameter("@EndDate", DateTimePicker2.Value)
    }
        If Not String.IsNullOrWhiteSpace(searchText) Then
            parameters.Add(New SqlParameter("@SearchText", searchText))
        End If
        If Not String.IsNullOrWhiteSpace(selectedCurrency) Then
            parameters.Add(New SqlParameter("@Currency", selectedCurrency))
        End If

        ' Execute query
        Dim dt As DataTable = Crud(sql, parameters)

        ' Populate DataGridView
        For Each row As DataRow In dt.Rows.Cast(Of DataRow).Reverse()
            Dim statusDesc As String = If(SafeConvertToBoolean(row("HavanoZimraStatus")), "Submitted", "Pending")
            dgw.Rows.Add(row("InvoiceNumber"), row("TxnId"), row("CustomerName"), row("TxnDate"), row("TotalVat"), row("Total_Exclusive"), row("Total"),
                   statusDesc)
            sumTotal += Convert.ToDouble(row("Total"))
            totalExcl += Convert.ToDouble(row("Total_Exclusive"))
            vatTotal += Convert.ToDouble(row("TotalVat"))
        Next

        ' Update labels
        lbltotalExcl.Text = totalExcl.ToString("N2")
        lbltotalsum.Text = sumTotal.ToString("N2")
        lbltotalvat.Text = vatTotal.ToString("N2")

        ' Return True if records found
        Return dt.Rows.Count > 0
    End Function

End Class