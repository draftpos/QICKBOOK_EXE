Imports System.Data.SqlClient

Public Class FrmTest
    Private Sub btnAddInvoice_Click(sender As Object, e As EventArgs) Handles btnAddInvoice.Click
        ' Define the data to insert
        Dim txnId As String = txtid.Text
        Dim customerName As String = "John Doe"
        Dim txnDate As String = Now.ToShortDateString
        Dim customerListId As String = "CL12345"
        Dim amount As Decimal = 100
        Dim appliedAmount As Decimal = 100
        Dim subtotal As Decimal = 100
        Dim salesTaxPercentage As Decimal = 15
        Dim salesTaxTotal As Decimal = 15
        Dim balanceRemaining As Decimal = 0
        Dim currency As String = "USD"
        Dim exchangeRate As Decimal = 1
        Dim balanceRemainingInHomeCurrency As Decimal = 0
        Dim invoiceNumber As String = "INV-2024-003"

        ' SQL Insert statement
        Dim query As String = "INSERT INTO Invoice (TxnId, CustomerName, TxnDate, CustomerListId, Amount, AppliedAmount, Subtotal, SalesTaxPercentage, SalesTaxTotal, BalanceRemaining, Currency, ExchangeRate, BalanceRemainingInHomeCurrency, InvoiceNumber,HavanoZimraStatus) " &
                              "VALUES (@TxnId, @CustomerName, @TxnDate, @CustomerListId, @Amount, @AppliedAmount, @Subtotal, @SalesTaxPercentage, @SalesTaxTotal, @BalanceRemaining, @Currency, @ExchangeRate, @BalanceRemainingInHomeCurrency, @InvoiceNumber,@HavanoZimraStatus)"

        ' Use a connection and command to execute the query
        Using connection As New SqlConnection(cs)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@TxnId", txnId)
                command.Parameters.AddWithValue("@CustomerName", customerName)
                command.Parameters.AddWithValue("@TxnDate", txnDate)
                command.Parameters.AddWithValue("@CustomerListId", customerListId)
                command.Parameters.AddWithValue("@Amount", amount)
                command.Parameters.AddWithValue("@AppliedAmount", appliedAmount)
                command.Parameters.AddWithValue("@Subtotal", subtotal)
                command.Parameters.AddWithValue("@SalesTaxPercentage", salesTaxPercentage)
                command.Parameters.AddWithValue("@SalesTaxTotal", salesTaxTotal)
                command.Parameters.AddWithValue("@BalanceRemaining", balanceRemaining)
                command.Parameters.AddWithValue("@Currency", currency)
                command.Parameters.AddWithValue("@ExchangeRate", exchangeRate)
                command.Parameters.AddWithValue("@BalanceRemainingInHomeCurrency", balanceRemainingInHomeCurrency)
                command.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber)
                command.Parameters.AddWithValue("@HavanoZimraStatus", False)


                ' Open the connection
                connection.Open()

                ' Execute the query
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                MsgBox($"{rowsAffected} row(s) inserted.")
            End Using
        End Using
    End Sub

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click

        ' Define the data to insert
        Dim listId As String = "LIST12345"
        Dim name As String = "MILO"
        Dim qty As Single = 1
        Dim rate As Decimal = 25
        Dim amount As Decimal = qty * rate
        Dim txnId As String = txtid.Text
        Dim vat As String = "15"

        ' SQL Insert statement
        Dim query As String = "INSERT INTO Item (ListId, Name, Qty, Rate, Amount, TxnId, Vat) " &
                              "VALUES (@ListId, @Name, @Qty, @Rate, @Amount, @TxnId, @Vat)"

        ' Use a connection and command to execute the query
        Using connection As New SqlConnection(cs)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@ListId", listId)
                command.Parameters.AddWithValue("@Name", name)
                command.Parameters.AddWithValue("@Qty", qty)
                command.Parameters.AddWithValue("@Rate", rate)
                command.Parameters.AddWithValue("@Amount", amount)
                command.Parameters.AddWithValue("@TxnId", txnId)
                command.Parameters.AddWithValue("@Vat", vat)

                ' Open the connection
                connection.Open()

                ' Execute the query
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                MsgBox($"{rowsAffected} row(s) inserted.")
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' SQL Insert query
        Dim query As String = "INSERT INTO CreditMemo (TxnId, CustomerName, TxnDate, CustomerListId,Currency, Amount, CreditNoteNumber, Subtotal, SalesTaxPercentage, SalesTaxTotal, TotalAmount, CreditRemaining, Message) " &
                              "VALUES (@TxnId, @CustomerName, @TxnDate, @CustomerListId,@Currency,@Amount, @CreditNoteNumber, @Subtotal, @SalesTaxPercentage, @SalesTaxTotal, @TotalAmount, @CreditRemaining, @Message)"

        ' Sample data to insert
        Dim txnId As String = "TXN12347"
        Dim customerName As String = "John Doe"
        Dim txnDate As String = DateTime.Now.ToShortDateString
        Dim customerListId As String = "CUST123"
        Dim Currency As String = "USD"
        Dim amount As Decimal = 1000.123
        Dim creditNoteNumber As String = "CN12345"
        Dim subtotal As Decimal = 900
        Dim salesTaxPercentage As Decimal = 10
        Dim salesTaxTotal As Decimal = 90
        Dim totalAmount As Decimal = 990
        Dim creditRemaining As Decimal = 10
        Dim message As String = "This is a sample message."

        Try
            Using connection As New SqlConnection(cs)
                connection.Open()

                Using command As New SqlCommand(query, connection)
                    ' Add parameters with values
                    command.Parameters.AddWithValue("@TxnId", txnId)
                    command.Parameters.AddWithValue("@CustomerName", customerName)
                    command.Parameters.AddWithValue("@TxnDate", txnDate)
                    command.Parameters.AddWithValue("@CustomerListId", customerListId)
                    command.Parameters.AddWithValue("@Currency", Currency)
                    command.Parameters.AddWithValue("@Amount", amount)
                    command.Parameters.AddWithValue("@CreditNoteNumber", creditNoteNumber)
                    command.Parameters.AddWithValue("@Subtotal", subtotal)
                    command.Parameters.AddWithValue("@SalesTaxPercentage", salesTaxPercentage)
                    command.Parameters.AddWithValue("@SalesTaxTotal", salesTaxTotal)
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount)
                    command.Parameters.AddWithValue("@CreditRemaining", creditRemaining)
                    command.Parameters.AddWithValue("@Message", message)

                    ' Execute the query
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MsgBox("Record inserted successfully.")
                    Else
                        MsgBox("Failed to insert the record.")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Define the data to insert
        Dim listId As String = "LIST12346"
        Dim name As String = "Millo"
        Dim qty As Single = 1
        Dim rate As Decimal = 25
        Dim amount As Decimal = qty * rate
        Dim txnId As String = "CN12345"
        Dim vat As String = "0"

        ' SQL Insert statement
        Dim query As String = "INSERT INTO Item (ListId, Name, Qty, Rate, Amount, TxnId, Vat) " &
                              "VALUES (@ListId, @Name, @Qty, @Rate, @Amount, @TxnId, @Vat)"

        ' Use a connection and command to execute the query
        Using connection As New SqlConnection(cs)
            Using command As New SqlCommand(query, connection)
                ' Add parameters to the command
                command.Parameters.AddWithValue("@ListId", listId)
                command.Parameters.AddWithValue("@Name", name)
                command.Parameters.AddWithValue("@Qty", qty)
                command.Parameters.AddWithValue("@Rate", rate)
                command.Parameters.AddWithValue("@Amount", amount)
                command.Parameters.AddWithValue("@TxnId", txnId)
                command.Parameters.AddWithValue("@Vat", vat)

                ' Open the connection
                connection.Open()

                ' Execute the query
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                MsgBox($"{rowsAffected} row(s) inserted.")
            End Using
        End Using
    End Sub
End Class