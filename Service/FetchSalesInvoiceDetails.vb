Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Timers
Imports MySql.Data.MySqlClient

Module FetchSalesInvoiceDetails
    Private fetchTimer As Timer
    Dim logFilePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceLog.txt")

    Public Sub Main()
        ' Start the process with a timer
        fetchTimer = New Timer(10000) ' Set interval to 30 seconds (30000 ms)
        AddHandler fetchTimer.Elapsed, AddressOf OnTimedEvent
        fetchTimer.AutoReset = True
        fetchTimer.Enabled = True
        LogMessage(logFilePath, $"SQL database. {cs}")
        Console.WriteLine("FetchSalesInvoiceDetails service started. Press [Enter] to exit.")


        ' Log the start message
        LogMessage(logFilePath, "FetchSalesInvoiceDetails service started. Press [Enter] to exit.")

        Console.ReadLine() ' Keep the application running
    End Sub

    Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        If IsInternetAvailable() Then
            Console.WriteLine($"Internet available. Running fetch process at {DateTime.Now}.")
            LogMessage(logFilePath, $"Internet available. Running fetch process at {DateTime.Now}.")
            FetchAndInsertSalesInvoices()
        Else
            Console.WriteLine($"No internet connection detected at {DateTime.Now}. Retrying in 30 seconds.")
        End If
    End Sub

    Private Function IsInternetAvailable() As Boolean
        Try
            Return NetworkInterface.GetIsNetworkAvailable()
        Catch ex As Exception
            Console.WriteLine($"Error checking internet availability: {ex.Message}")
            Return False
        End Try
    End Function

    Private Function GetConnectionStringValue(connectionString As String, key As String) As String
        Dim startIndex As Integer = connectionString.IndexOf(key & "=") + key.Length + 1
        Dim endIndex As Integer = connectionString.IndexOf(";", startIndex)
        If endIndex = -1 Then endIndex = connectionString.Length
        Return connectionString.Substring(startIndex, endIndex - startIndex).Trim()
    End Function

    Private Sub FetchAndInsertSalesInvoices()
        ' ERPNext MySQL database connection details
        Dim erpServer As String = "89.250.67.36"
        Dim erpDatabase As String = "_f6c6c961c16d9264"
        Dim erpUser As String = "frapperemote"
        Dim erpPassword As String = "frapperemote@123$#"
        Dim erpPort As String = "3306" ' Default MySQL/MariaDB port
        Dim erpConnectionString As String = $"Server={erpServer};Port={erpPort};Database={erpDatabase};Uid={erpUser};Pwd={erpPassword};"
        Dim sqlConnectionString As String = cs
        LogMessage(logFilePath, $"SQL database. {cs}")

        ' SQL Server connection details
        ' Dim sqlServer As String = "DESKTOP-TKNB1T8"
        ' Dim sqlDatabase As String = "FetchInv"
        ' Dim sqlUser As String = "sa"
        ' Dim sqlPassword As String = "12345"
        ' Dim sqlConnectionString As String = $"Server={sqlServer};Database={sqlDatabase};User Id={sqlUser};Password={sqlPassword};"

        ' Query to fetch sales invoices and items
        Dim fetchQuery As String = "
            SELECT 
                si.name AS invoice_name, 
                si.posting_date, 
                si.customer, 
                si.grand_total, 
                si.customer_address,
                si.total_taxes_and_charges,
                si.outstanding_amount,
                si.currency,
                si.conversion_rate,
                sii.item_code, 
                sii.item_name, 
                sii.qty, 
                sii.rate, 
                sii.amount,
                sii.item_tax_template,
                cu.custom_customer_address,
                cu.custom_customer_tin,
                cu.custom_customer_vat,
                cu.custom_customer_email,
                cu.custom_customer_phone
            FROM 
                `tabSales Invoice` si
            INNER JOIN 
                `tabSales Invoice Item` sii 
                ON 
                si.name = sii.parent
            INNER JOIN 
                `tabCustomer` cu 
                ON 
                cu.name = si.customer
           
            WHERE 
                si.docstatus = 1 
                AND si.status != 'Draft'
                AND custom_syc_status != 'Synced'
                Order by si.name asc;  

        "

        'INNER JOIN 
        '`tabItem Tax` tax 
        'ON 
        'tax.name = cu.tax_id'

        Try
            Using erpConnection As New MySqlConnection(erpConnectionString)

                Try
                    erpConnection.Open()
                Catch Exception As Exception
                    Console.WriteLine($"Failed to connect to the ERPNEXT Server database. {Exception}")
                    LogMessage(logFilePath, $"Failed to connect to the ERPNEXT Server database. {Exception}")
                End Try
                If erpConnection.State = ConnectionState.Closed Then
                    Console.WriteLine("Failed to connect to the ERPNEXT Server database.")
                    LogMessage(logFilePath, "Failed to connect to the ERPNEXT Server database.")
                Else
                    Console.WriteLine("Connected to the ERPNext database.")
                    LogMessage(logFilePath, "Connected to the ERPNext database.")
                End If

                Using fetchCommand As New MySqlCommand(fetchQuery, erpConnection)
                    Using reader As MySqlDataReader = fetchCommand.ExecuteReader()


                        Using sqlConnection As New SqlConnection(sqlConnectionString)
                            Try
                                sqlConnection.Open()
                            Catch Exception As Exception
                                Console.WriteLine($"Failed to connect to the SQL Server database. {Exception}")
                                LogMessage(logFilePath, $"Failed to connect to the SQL Server database. {Exception}")
                            End Try
                            If sqlConnection.State = ConnectionState.Closed Then
                                Console.WriteLine("Failed to connect to the SQL Server database.")
                                LogMessage(logFilePath, "Failed to connect to the SQL Server database.")
                            Else
                                Console.WriteLine("Connected to the SQL Server database.")
                                LogMessage(logFilePath, "Connected to the SQL Server database.")
                            End If


                            Dim currentInvoice As String = String.Empty
                            Dim currentCustomer As String = String.Empty

                            While reader.Read()
                                Dim invoiceName As String = reader("invoice_name").ToString()
                                Dim customerName As String = reader("customer").ToString()
                                Dim customerCode As String = reader("customer").ToString()
                                Dim custom_customer_address As String = reader("custom_customer_address").ToString()
                                Dim custom_customer_tin As String = reader("custom_customer_tin").ToString()
                                Dim custom_customer_vat As String = reader("custom_customer_vat").ToString()
                                Dim custom_customer_phone As String = reader("custom_customer_phone").ToString()
                                Dim custom_customer_email As String = reader("custom_customer_email").ToString()
                                Dim currency As String = reader("currency").ToString()

                                ' Insert invoice details only once per invoice
                                If invoiceName <> currentInvoice Then
                                    Dim invoiceInsertQuery As String = "
                                        INSERT INTO [dbo].[Invoice]
                                           ([TxnId], [CustomerName], [TxnDate], [CustomerListId], 
                                            [Amount], [AppliedAmount], [Subtotal], 
                                            [SalesTaxPercentage], [SalesTaxTotal], 
                                            [BalanceRemaining], [Currency], 
                                            [ExchangeRate], [BalanceRemainingInHomeCurrency], [InvoiceNumber], [CustomerAddress], [CustomerTin], [CustomerVat], [CustomerEmail], [CustomerPhone], [ReceiptNo])
                                        VALUES
                                           (@TxnId, @CustomerName, @TxnDate, @CustomerListId, 
                                            @Amount, @AppliedAmount, @Subtotal, 
                                            @SalesTaxPercentage, @SalesTaxTotal, 
                                            @BalanceRemaining, @Currency, 
                                            @ExchangeRate, @BalanceRemainingInHomeCurrency, @InvoiceNumber, @CustomerAddress, @CustomerTin, @CustomerVat, @CustomerEmail, @CustomerPhone, @ReceiptNo);
                                    "

                                    Using invoiceCommand As New SqlCommand(invoiceInsertQuery, sqlConnection)
                                        invoiceCommand.Parameters.AddWithValue("@TxnId", invoiceName)
                                        invoiceCommand.Parameters.AddWithValue("@CustomerName", reader("customer").ToString())
                                        invoiceCommand.Parameters.AddWithValue("@TxnDate", Convert.ToDateTime(reader("posting_date")).ToString("yyyy-MM-dd"))
                                        invoiceCommand.Parameters.AddWithValue("@CustomerListId", reader("customer").ToString()) ' Update with appropriate field if available
                                        invoiceCommand.Parameters.AddWithValue("@Amount", Convert.ToDecimal(reader("grand_total")))
                                        invoiceCommand.Parameters.AddWithValue("@AppliedAmount", Convert.ToDecimal(reader("grand_total"))) ' Update with appropriate field if available
                                        invoiceCommand.Parameters.AddWithValue("@Subtotal", Convert.ToDecimal(reader("grand_total"))) ' Update with appropriate field if available
                                        invoiceCommand.Parameters.AddWithValue("@SalesTaxPercentage", DBNull.Value) ' Update with appropriate field if available
                                        invoiceCommand.Parameters.AddWithValue("@SalesTaxTotal", Convert.ToDecimal(reader("total_taxes_and_charges"))) ' Update with appropriate field if available 
                                        invoiceCommand.Parameters.AddWithValue("@BalanceRemaining", Convert.ToDecimal(reader("outstanding_amount"))) ' Assuming 0 for closed invoices 
                                        invoiceCommand.Parameters.AddWithValue("@Currency", reader("currency").ToString()) ' Replace with appropriate field if available  
                                        invoiceCommand.Parameters.AddWithValue("@ExchangeRate", reader("conversion_rate").ToString()) ' Replace with appropriate field if available   
                                        invoiceCommand.Parameters.AddWithValue("@BalanceRemainingInHomeCurrency", Convert.ToDecimal(reader("outstanding_amount"))) ' Assuming 0 for closed invoices
                                        invoiceCommand.Parameters.AddWithValue("@InvoiceNumber", invoiceName)
                                        invoiceCommand.Parameters.AddWithValue("@CustomerAddress", reader("custom_customer_address").ToString())
                                        invoiceCommand.Parameters.AddWithValue("@CustomerTin", reader("custom_customer_tin").ToString())
                                        invoiceCommand.Parameters.AddWithValue("@CustomerVat", reader("custom_customer_vat").ToString())
                                        invoiceCommand.Parameters.AddWithValue("@CustomerEmail", reader("custom_customer_email").ToString())
                                        invoiceCommand.Parameters.AddWithValue("@CustomerPhone", reader("custom_customer_phone").ToString())
                                        invoiceCommand.Parameters.AddWithValue("@ReceiptNo", invoiceName)

                                        invoiceCommand.ExecuteNonQuery()
                                        Console.WriteLine($"Inserted invoice: {invoiceName}")
                                        LogMessage(logFilePath, $"Inserted invoice: {invoiceName}")
                                    End Using

                                    currentInvoice = invoiceName
                                    currentCustomer = customerName
                                    Console.WriteLine($"Entered customer update: {customerName}")
                                    LogMessage(logFilePath, $"Entered customer update: {customerName}")
                                    ERPNextSyncHelper.AddOrUpdateCustomer(sqlConnectionString, customerName, customerCode, custom_customer_address, custom_customer_tin, custom_customer_vat, custom_customer_phone, custom_customer_email, currency)
                                    ERPNextSyncHelper.UpdateIsSynced(erpConnectionString, currentInvoice)

                                End If

                                ' Insert item details
                                Dim itemInsertQuery As String = "
                                    INSERT INTO [dbo].[Item]
                                       ([ListId], [Name], [Qty], [Rate], [Amount], [TxnId], [Vat])
                                    VALUES
                                       (@ListId, @Name, @Qty, @Rate, @Amount, @TxnId, @Vat);
                                "

                                Using itemCommand As New SqlCommand(itemInsertQuery, sqlConnection)
                                    itemCommand.Parameters.AddWithValue("@ListId", reader("item_code").ToString())
                                    itemCommand.Parameters.AddWithValue("@Name", reader("item_name").ToString())
                                    itemCommand.Parameters.AddWithValue("@Qty", Convert.ToDecimal(reader("qty")))
                                    itemCommand.Parameters.AddWithValue("@Rate", Convert.ToDecimal(reader("rate")))
                                    itemCommand.Parameters.AddWithValue("@Amount", Convert.ToDecimal(reader("amount")))
                                    itemCommand.Parameters.AddWithValue("@TxnId", invoiceName)
                                    itemCommand.Parameters.AddWithValue("@Vat", reader("item_tax_template").ToString()) ' Replace with VAT if available

                                    itemCommand.ExecuteNonQuery()
                                    Console.WriteLine($"Inserted item: {reader("item_name").ToString()} for invoice: {invoiceName}")
                                    LogMessage(logFilePath, $"Inserted item: {reader("item_name").ToString()} for invoice: {invoiceName}")
                                End Using

                            End While
                            sqlConnection.Close()
                            Console.WriteLine("Disconnected from the SQL Server database.")
                            erpConnection.Close()
                            Console.WriteLine("Disconnected from the ERPNext database.")
                            LogMessage(logFilePath, "Disconnected from the SQL database.")
                            LogMessage(logFilePath, "Disconnected from the ERPNext database.")

                        End Using
                    End Using
                End Using



            End Using
        Catch ex As Exception
            Console.WriteLine($"An error occurred: {ex.Message}")
            LogMessage(logFilePath, $"An error occurred: {ex.Message}")
        End Try
    End Sub


    Sub LogMessage(filePath As String, message As String)
        Try
            ' Ensure the directory exists
            Dim directoryPath As String = Path.GetDirectoryName(filePath)
            If Not Directory.Exists(directoryPath) Then
                Directory.CreateDirectory(directoryPath)
            End If

            ' Append the message to the log file with a timestamp
            Using writer As New StreamWriter(filePath, True)
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}")
            End Using
        Catch ex As Exception
            ' Handle exceptions (optional)
            Console.WriteLine($"Error logging message: {ex.Message}")
        End Try
    End Sub


End Module
