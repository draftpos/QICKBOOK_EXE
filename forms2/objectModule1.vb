Imports System.Data.SqlClient

Module objectModule1
    Public Sub FillInvoiceData(txnId As String, ByRef invoice As Invoice)
        Dim invoiceQuery As String = "SELECT * FROM Invoice WHERE TxnId = @TxnId"
        Dim itemQuery As String = "SELECT * FROM Item WHERE TxnId = @TxnId"
        Dim parameters As New List(Of SqlParameter) From {
        New SqlParameter("@TxnId", txnId)
    }

        Dim invoiceTable As DataTable = Crud(invoiceQuery, parameters)
        If invoiceTable.Rows.Count > 0 Then
            Dim row As DataRow = invoiceTable.Rows(0)
            invoice.Id = Convert.ToInt32(row("id"))
            invoice.TxnId = row("TxnId").ToString()
            invoice.CustomerName = row("CustomerName").ToString()
            invoice.TxnDate = row("TxnDate").ToString()
            invoice.CustomerListId = row("CustomerListId").ToString()
            invoice.Amount = If(IsDBNull(row("Amount")), Nothing, Convert.ToDecimal(row("Amount")))
            invoice.AppliedAmount = If(IsDBNull(row("AppliedAmount")), Nothing, Convert.ToDecimal(row("AppliedAmount")))
            invoice.Subtotal = If(IsDBNull(row("Subtotal")), Nothing, Convert.ToDecimal(row("Subtotal")))
            invoice.SalesTaxPercentage = If(IsDBNull(row("SalesTaxPercentage")), Nothing, Convert.ToDecimal(row("SalesTaxPercentage")))
            invoice.SalesTaxTotal = If(IsDBNull(row("SalesTaxTotal")), Nothing, Convert.ToDecimal(row("SalesTaxTotal")))
            invoice.BalanceRemaining = If(IsDBNull(row("BalanceRemaining")), Nothing, Convert.ToDecimal(row("BalanceRemaining")))
            invoice.Currency = If(IsDBNull(row("Currency")), Nothing, row("Currency").ToString())
            invoice.ExchangeRate = If(IsDBNull(row("ExchangeRate")), Nothing, Convert.ToDecimal(row("ExchangeRate")))
            invoice.BalanceRemainingInHomeCurrency = If(IsDBNull(row("BalanceRemainingInHomeCurrency")), Nothing, Convert.ToDecimal(row("BalanceRemainingInHomeCurrency")))
            invoice.InvoiceNumber = If(IsDBNull(row("InvoiceNumber")), Nothing, row("InvoiceNumber").ToString())
        Else
            MsgBox("No invoice found for TxnId: " & txnId)
            Exit Sub
        End If
        Dim itemTable As DataTable = Crud(itemQuery, parameters)
        invoice.Items = New List(Of Item)()
        For Each itemRow As DataRow In itemTable.Rows
            Dim item As New Item() With {
            .Id = Convert.ToInt32(itemRow("Id")),
            .ListId = If(IsDBNull(itemRow("ListId")), Nothing, itemRow("ListId").ToString()),
            .Name = If(IsDBNull(itemRow("Name")), Nothing, itemRow("Name").ToString()),
            .Qty = If(IsDBNull(itemRow("Qty")), Nothing, Convert.ToSingle(itemRow("Qty"))),
            .Rate = If(IsDBNull(itemRow("Rate")), Nothing, Convert.ToDecimal(itemRow("Rate"))),
            .Amount = If(IsDBNull(itemRow("Amount")), Nothing, Convert.ToDecimal(itemRow("Amount"))),
            .TxnId = itemRow("TxnId").ToString()
        }
            invoice.Items.Add(item)
        Next
    End Sub
    Public Sub FillCreditMemoData(txnId As String, ByRef creditMemo As CreditMemo)
        Dim creditMemoQuery As String = "SELECT * FROM CreditMemo WHERE TxnId = @TxnId"
        Dim creditMemoParameters As New List(Of SqlParameter) From {
        New SqlParameter("@TxnId", txnId)
    }
        Dim creditMemoDataTable As DataTable = Crud(creditMemoQuery, creditMemoParameters)
        If creditMemoDataTable.Rows.Count > 0 Then
            Dim row As DataRow = creditMemoDataTable.Rows(0)
            creditMemo.Id = Convert.ToInt32(row("id"))
            creditMemo.TxnId = If(IsDBNull(row("TxnId")), Nothing, row("TxnId").ToString())
            creditMemo.CustomerName = If(IsDBNull(row("CustomerName")), Nothing, row("CustomerName").ToString())
            creditMemo.TxnDate = If(IsDBNull(row("TxnDate")), Nothing, row("TxnDate").ToString())
            creditMemo.CustomerListId = If(IsDBNull(row("CustomerListId")), Nothing, row("CustomerListId").ToString())
            creditMemo.Amount = If(IsDBNull(row("Amount")), Nothing, Convert.ToDecimal(row("Amount")))
            creditMemo.CreditNoteNumber = If(IsDBNull(row("CreditNoteNumber")), Nothing, row("CreditNoteNumber").ToString())
            creditMemo.Subtotal = If(IsDBNull(row("Subtotal")), Nothing, Convert.ToDecimal(row("Subtotal")))
            creditMemo.SalesTaxPercentage = If(IsDBNull(row("SalesTaxPercentage")), Nothing, Convert.ToDecimal(row("SalesTaxPercentage")))
            creditMemo.SalesTaxTotal = If(IsDBNull(row("SalesTaxTotal")), Nothing, Convert.ToDecimal(row("SalesTaxTotal")))
            creditMemo.TotalAmount = If(IsDBNull(row("TotalAmount")), Nothing, Convert.ToDecimal(row("TotalAmount")))
            creditMemo.CreditRemaining = If(IsDBNull(row("CreditRemaining")), Nothing, Convert.ToDecimal(row("CreditRemaining")))
            creditMemo.Message = If(IsDBNull(row("Message")), Nothing, row("Message").ToString())
            creditMemo.Items = FetchItemsForCreditMemo(txnId)
        Else
            MessageBox.Show("No Credit Memo found for TxnId: " & txnId)
        End If
    End Sub
    Private Function FetchItemsForCreditMemo(txnId As String) As List(Of Item)
        Dim items As New List(Of Item)()
        Dim itemQuery As String = "SELECT * FROM Item WHERE TxnId = @TxnId"
        Dim itemParameters As New List(Of SqlParameter) From {
        New SqlParameter("@TxnId", txnId)
    }
        Dim itemDataTable As DataTable = Crud(itemQuery, itemParameters)
        For Each row As DataRow In itemDataTable.Rows
            Dim item As New Item With {
            .Id = Convert.ToInt32(row("Id")),
            .ListId = If(IsDBNull(row("ListId")), Nothing, row("ListId").ToString()),
            .Name = If(IsDBNull(row("Name")), Nothing, row("Name").ToString()),
            .Qty = If(IsDBNull(row("Qty")), Nothing, Convert.ToSingle(row("Qty"))),
            .Rate = If(IsDBNull(row("Rate")), Nothing, Convert.ToDecimal(row("Rate"))),
            .Amount = If(IsDBNull(row("Amount")), Nothing, Convert.ToDecimal(row("Amount"))),
            .TxnId = If(IsDBNull(row("TxnId")), Nothing, row("TxnId").ToString())
        }
            items.Add(item)
        Next

        Return items
    End Function


End Module

Public Class Invoice
    Public Property Id As Integer
    Public Property TxnId As String
    Public Property CustomerName As String
    Public Property TxnDate As String
    Public Property CustomerListId As String
    Public Property Amount As Decimal?
    Public Property AppliedAmount As Decimal?
    Public Property Subtotal As Decimal?
    Public Property SalesTaxPercentage As Decimal?
    Public Property SalesTaxTotal As Decimal?
    Public Property BalanceRemaining As Decimal?
    Public Property Currency As String
    Public Property ExchangeRate As Decimal?
    Public Property BalanceRemainingInHomeCurrency As Decimal?
    Public Property InvoiceNumber As String
    Public Property Items As List(Of Item)
End Class

Public Class Item
    Public Property Id As Integer
    Public Property ListId As String
    Public Property Name As String
    Public Property Qty As Single
    Public Property Rate As Decimal
    Public Property Amount As Decimal
    Public Property TxnId As String
End Class

Public Class CreditMemo
    Public Property Id As Integer
    Public Property TxnId As String
    Public Property CustomerName As String
    Public Property TxnDate As String
    Public Property CustomerListId As String
    Public Property Amount As Decimal?
    Public Property CreditNoteNumber As String
    Public Property Subtotal As Decimal?
    Public Property SalesTaxPercentage As Decimal?
    Public Property SalesTaxTotal As Decimal?
    Public Property TotalAmount As Decimal?
    Public Property CreditRemaining As Decimal?
    Public Property Message As String
    Public Property Items As List(Of Item) ' List of associated items
End Class



