Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.ServiceProcess
Imports System.Text
Imports System.Threading.Tasks
Imports Microsoft.Office.Interop.Excel

Module database_modules



    Public Sub EnsureErpFieldExists()
        Using conn As New SqlConnection(cs)
            conn.Open()

            ' Check and add 'erp' column in 'InvoiceInfo' table if not exists
            Dim checkInvoiceInfoQuery As String = "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'InvoiceInfo') AND name = 'erp') " &
                                              "BEGIN ALTER TABLE InvoiceInfo ADD erp BIT DEFAULT 0 END"
            Using checkInvoiceInfoCmd As New SqlCommand(checkInvoiceInfoQuery, conn)
                checkInvoiceInfoCmd.ExecuteNonQuery()
            End Using

            ' Check and add 'erp' column in 'SalesReturn' table if not exists
            Dim checkSalesReturnQuery As String = "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'SalesReturn') AND name = 'erp') " &
                                              "BEGIN ALTER TABLE SalesReturn ADD erp BIT DEFAULT 0 END"
            Using checkSalesReturnCmd As New SqlCommand(checkSalesReturnQuery, conn)
                checkSalesReturnCmd.ExecuteNonQuery()
            End Using

            ' Check and add 'Printername2' column in 'PosPrinterSetting' table if not exists
            Dim checkPosPrinterSettingQuery As String = "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'PosPrinterSetting') AND name = 'Printername2') " &
                                                    "BEGIN ALTER TABLE PosPrinterSetting ADD Printername2 NVARCHAR(255) END"
            Using checkPosPrinterSettingCmd As New SqlCommand(checkPosPrinterSettingQuery, conn)
                checkPosPrinterSettingCmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Public Function Dispatchto_invoiceinfo() As Integer
        Using con As New SqlConnection(cs)
            con.Open()

            ' Query to check and add columns if they do not exist
            Dim checkAndAddColumnQuery As String = "
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.columns 
            WHERE Name = N'dispatch' 
            AND Object_ID = Object_ID(N'Invoiceinfo')
        )
        BEGIN
            ALTER TABLE Invoiceinfo 
            ADD dispatch BIT NOT NULL DEFAULT 0
        END

        IF NOT EXISTS (
            SELECT 1 
            FROM sys.columns 
            WHERE Name = N'dispatchby' 
            AND Object_ID = Object_ID(N'Invoiceinfo')
        )
        BEGIN
            ALTER TABLE Invoiceinfo 
            ADD dispatchby NCHAR(255) NULL
        END"

            Using cmd As New SqlCommand(checkAndAddColumnQuery, con)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        Return 0
    End Function

    Public Sub CreateBranchesTableIfNotExists()
        Using con As New SqlConnection(cs)
            con.Open()
            Dim checkTableQuery As String = "
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'branches')
            BEGIN
                CREATE TABLE [dbo].[branches] (
                    id INT IDENTITY(1,1) PRIMARY KEY,
                    BranchName CHAR(255)
                )
            END"

            Using cmd As New SqlCommand(checkTableQuery, con)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Sub AddPackQtyAndPackNameIfNotExists()
        Dim checkPackQtyQuery As String = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Purchase_Join' AND COLUMN_NAME = 'packqty'"
        Dim checkPackNameQuery As String = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Purchase_Join' AND COLUMN_NAME = 'packname'"
        Dim addPackQtyQuery As String = "ALTER TABLE Purchase_Join ADD packqty DECIMAL"
        Dim addPackNameQuery As String = "ALTER TABLE Purchase_Join ADD packname CHAR(255)"
        Using con As New SqlConnection(cs)
            con.Open()
            Dim packQtyExists As Boolean
            Using checkCmd As New SqlCommand(checkPackQtyQuery, con)
                packQtyExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
            End Using
            If Not packQtyExists Then
                Using addCmd As New SqlCommand(addPackQtyQuery, con)
                    addCmd.ExecuteNonQuery()
                End Using
            End If
            Dim packNameExists As Boolean
            Using checkCmd As New SqlCommand(checkPackNameQuery, con)
                packNameExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
            End Using
            If Not packNameExists Then
                Using addCmd As New SqlCommand(addPackNameQuery, con)
                    addCmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    Function Stock_adjst_dated_columCheckAndAlterDateField()
        Dim query As String = "IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'stock_adjustment_card' AND COLUMN_NAME = 'dated' AND DATA_TYPE = 'date') " &
                          "BEGIN " &
                          "    ALTER TABLE stock_adjustment_card ALTER COLUMN dated datetime " &
                          "END"
        Try
            Using con As New SqlConnection(cs)
                con.Open()
                Using cmd As New SqlCommand(query, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True ' Indicate success
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False ' Indicate failure
        End Try
    End Function

    Function first_time_login_sql()
        Dim query As String = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Registration' AND COLUMN_NAME = 'flogin') " &
                    "BEGIN " &
                    "    ALTER TABLE Registration ADD flogin BIT DEFAULT 1; " &
                    "END;"
        Try
            Using con As New SqlConnection(cs)
                Using cmd As New SqlCommand(query, con)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True ' Field added successfully or already exists
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        Return False ' Failed to add or already exists
    End Function

    Public Async Function return_creation_of_db() As Task(Of Integer)
        '   ReadApiCredentials()
        Dim tasks As New List(Of Task)


        '   tasks.Add(Task.Run(AddressOf ProcessSalesReturnsAsync))
        Await Task.WhenAll(tasks)
        Return 0
    End Function
    ''''''''''''''''''ENCRYPTION AND DECRYSTION
    Public Function EncryptToBase64(ByVal input As String) As String
        Dim bytesToEncode As Byte() = Encoding.UTF8.GetBytes(input)
        Dim base64String As String = Convert.ToBase64String(bytesToEncode)
        Return base64String
    End Function

    Function LogFunc(st1 As String, st2 As String)
        con = New SqlConnection(cs)
        con.Open()
        Dim cb As String = "insert into Logs(UserID,Date,Operation) VALUES (@d1,@d2,@d3)"
        cmd = New SqlCommand(cb)
        cmd.Connection = con
        cmd.Parameters.AddWithValue("@d1", st1)
        cmd.Parameters.AddWithValue("@d2", System.DateTime.Now)
        cmd.Parameters.AddWithValue("@d3", st2)
        cmd.CommandTimeout = 0
        cmd.ExecuteReader()
        con.Close()
        Return 0
    End Function

    Public Function addfield_mixtitle_mixstatus()
        Dim query As String = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POS_HoldBill' AND COLUMN_NAME = 'mixtitle')
                          ALTER TABLE POS_HoldBill ADD mixtitle nvarchar(50)"
        query &= ";"
        query &= "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'POS_HoldBillItems ' AND COLUMN_NAME = 'mixstatus')
                            ALTER TABLE POS_HoldBillItems  ADD mixstatus bit"
        Using con As New SqlConnection(cs)
            Using cmd As New SqlCommand(query, con)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
        Return 0
    End Function



    Public Function DeleteDuplicateStockData(cons As String)
        Dim sqlDelete As String = "DELETE FROM Temp_Stock_Company WHERE ProductID IN (SELECT ProductID FROM (SELECT ProductID, ROW_NUMBER() OVER (PARTITION BY ProductID ORDER BY (SELECT NULL)) AS RowNumber FROM Temp_Stock_Company) AS T WHERE RowNumber > 1);"
        sqlDelete = "DELETE TOP (1) FROM Temp_Stock_Company WHERE ProductID IN (SELECT ProductID FROM (SELECT ProductID, ROW_NUMBER() OVER (PARTITION BY ProductID ORDER BY (SELECT NULL)) AS RowNumber FROM Temp_Stock_Company) AS T WHERE RowNumber > 1);"
        Using con As New SqlConnection(cons)
            con.Open()
            Using command As New SqlCommand(sqlDelete, con)
                command.ExecuteNonQuery()
            End Using
        End Using
        '  MessageBox.Show("One instance of duplicate data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return "One instance of duplicate data deleted successfully"
    End Function
    Public Sub INCLUDE_IFNOTEXIST(cons As String)
        ' SQL to select all products
        Dim selectSql As String = "SELECT PID, SALESCOST, BARCODE, PURCHASECOST FROM PRODUCT"

        ' SQL to check existence and insert if not exists
        Dim checkAndInsertSql As String = "
        IF NOT EXISTS (SELECT 1 FROM Temp_Stock_Company WHERE ProductID = @ProductID)
        BEGIN
            INSERT INTO Temp_Stock_Company (ProductID, Barcode, PurchaseRate, SalesRate, ManufacturingDate, ExpiryDate, Qty)
            VALUES (@ProductID, @Barcode, @PurchaseRate, @SalesRate, NULL, NULL, 0)
        END"

        Using con As New SqlConnection(cons)
            con.Open()

            ' Fetch all products
            Using selectCommand As New SqlCommand(selectSql, con)
                Using reader As SqlDataReader = selectCommand.ExecuteReader()
                    While reader.Read()
                        ' Extract product details
                        Dim productId As Integer = reader("PID")
                        Dim salesCost As Decimal = reader("SALESCOST")
                        Dim barcode As String = reader("BARCODE").ToString()
                        Dim purchaseCost As Decimal = reader("PURCHASECOST")
                        If barcode.Length > 30 Then
                            barcode = barcode.Substring(0, 30)
                        End If
                        ' Check existence and insert if not exists
                        Using checkAndInsertCommand As New SqlCommand(checkAndInsertSql, con)
                            checkAndInsertCommand.Parameters.AddWithValue("@ProductID", productId)
                            checkAndInsertCommand.Parameters.AddWithValue("@Barcode", barcode)
                            checkAndInsertCommand.Parameters.AddWithValue("@PurchaseRate", purchaseCost)
                            checkAndInsertCommand.Parameters.AddWithValue("@SalesRate", salesCost)

                            checkAndInsertCommand.ExecuteNonQuery()
                        End Using
                    End While
                End Using
            End Using
        End Using
    End Sub


    Public Function BlockTest_UpdateDatedColumnToDateTime(connectionString As String)
        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim checkQuery As String = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS " &
                                   "WHERE TABLE_NAME = 'block_test' AND COLUMN_NAME = 'Dated'"

            Dim currentDataType As String = String.Empty
            Using checkCmd As New SqlCommand(checkQuery, con)
                Dim reader As SqlDataReader = checkCmd.ExecuteReader()
                If reader.Read() Then
                    currentDataType = reader("DATA_TYPE").ToString()
                End If
                reader.Close()
            End Using

            ' If the current data type is already datetime, no need to change
            If currentDataType.ToLower() = "datetime" Then
                Return 0
            End If

            ' Step 1: Add a new column of type datetime
            Dim addColumnQuery As String = "ALTER TABLE block_test ADD Dated_New datetime"
            Using addColumnCmd As New SqlCommand(addColumnQuery, con)
                addColumnCmd.ExecuteNonQuery()
            End Using

            ' Step 2: Update the new column with converted values from the old column
            Dim updateColumnQuery As String = "UPDATE block_test SET Dated_New = " &
                                          "CASE WHEN ISDATE(Dated) = 1 THEN CAST(Dated AS datetime) ELSE NULL END"
            Using updateColumnCmd As New SqlCommand(updateColumnQuery, con)
                updateColumnCmd.ExecuteNonQuery()
            End Using

            ' Step 3: Drop the old column
            Dim dropColumnQuery As String = "ALTER TABLE block_test DROP COLUMN Dated"
            Using dropColumnCmd As New SqlCommand(dropColumnQuery, con)
                dropColumnCmd.ExecuteNonQuery()
            End Using

            ' Step 4: Rename the new column to the original column name
            Dim renameColumnQuery As String = "EXEC sp_rename 'block_test.Dated_New', 'Dated', 'COLUMN'"
            Using renameColumnCmd As New SqlCommand(renameColumnQuery, con)
                renameColumnCmd.ExecuteNonQuery()
            End Using
        End Using
        Return 0
    End Function

    Public Sub CheckHavanoZimraField()
        ' SQL queries to check if the columns exist and to add them if they do not
        Dim checkColumnIshavanozimra As String = "IF COL_LENGTH('Company', 'Ishavanozimra') IS NULL ALTER TABLE Company ADD Ishavanozimra BIT"
        Dim checkColumnHavanozimrakey As String = "IF COL_LENGTH('Company', 'havanozimrakey') IS NULL ALTER TABLE Company ADD havanozimrakey VARCHAR(500)"
        Dim checkVcode As String = "IF COL_LENGTH('InvoiceInfo', 'Vcode') IS NULL ALTER TABLE InvoiceInfo ADD Vcode VARCHAR(450)"

        Using connection As New SqlConnection(cs)
            Try
                ' Open the connection to the database
                connection.Open()
                ' Create a command to execute the check and add column queries
                Using command As New SqlCommand()
                    command.Connection = connection
                    ' Check and add Ishavanozimra column if it doesn't exist
                    command.CommandText = checkColumnIshavanozimra
                    command.ExecuteNonQuery()
                    ' Check and add havanozimrakey column if it doesn't exist
                    command.CommandText = checkColumnHavanozimrakey
                    command.ExecuteNonQuery()
                    '------------------------
                    command.CommandText = checkVcode
                    command.ExecuteNonQuery()
                End Using


            Catch ex As Exception

            End Try
        End Using
    End Sub

    Public Sub EnsureSqlBrowserServiceEnabled()
        Try
            Dim sqlBrowserService As New ServiceController("SQLBrowser")
            If sqlBrowserService.Status <> ServiceControllerStatus.Running Then
                sqlBrowserService.Start()
                sqlBrowserService.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10))
                MessageBox.Show("SQL Browser service was stopped and has been started successfully.")
            Else
                '  MessageBox.Show("SQL Browser service is already running.")
            End If
        Catch ex As InvalidOperationException
            MessageBox.Show("SQL Browser service is not installed on this machine.")
        Catch ex As Exception
            MessageBox.Show("An error occurred while enabling SQL Browser service: " & ex.Message)
        End Try
    End Sub

End Module


