Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Win32
Imports System.Management
Module cruds
    Public Function ToTitleCase(input As String) As String
        Dim excludedWords As String() = {"to", "of", "in", "and", "the", "for", "a", "an", "on"}
        Dim words As String() = input.ToLower().Split(New Char() {" "c, vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim result As New List(Of String)

        For i As Integer = 0 To words.Length - 1
            If words(i).Length > 0 Then
                If i = 0 OrElse Not excludedWords.Contains(words(i)) Then
                    ' Capitalize the first letter of the word
                    result.Add(Char.ToUpper(words(i)(0)) & words(i).Substring(1))
                Else
                    ' Keep the word in lowercase
                    result.Add(words(i))
                End If
            End If
        Next

        Return String.Join(" ", result)
    End Function

    Public Function Crud(sql As String, sqlParameters As List(Of SqlParameter)) As DataTable
        Dim dt As New DataTable()
        Using conn As New SqlConnection(cs)
            Try
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    If sqlParameters IsNot Nothing Then
                        cmd.Parameters.AddRange(sqlParameters.ToArray())
                    End If
                    If sql.Trim().ToUpper().StartsWith("SELECT") Then
                        Using da As New SqlDataAdapter(cmd)
                            da.Fill(dt)
                        End Using
                    Else
                        cmd.ExecuteNonQuery()
                    End If
                End Using
            Catch ex As Exception
                MsgBox("An error occurred: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using

        Return dt
    End Function
    Public Function ExecuteScalar(query As String, params As List(Of SqlParameter)) As Object
        Dim con As New SqlConnection(cs)
        con.Open()
        Dim cmd As New SqlCommand(query, con)
        For Each param In params
            cmd.Parameters.Add(param)
        Next
        Return cmd.ExecuteScalar()
    End Function
    Public Async Function UpdateDecimalFieldsAsyncold(connectionString As String) As Task
        Try
            Using con As New SqlConnection(connectionString)
                Await con.OpenAsync()
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandTimeout = 0 ' Set the command timeout to infinite

                ' Get all tables in the database
                cmd.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
                Dim tables As New List(Of String)
                Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                    While Await reader.ReadAsync()
                        tables.Add(reader("TABLE_NAME").ToString())
                    End While
                End Using

                ' Loop through each table
                For Each table As String In tables
                    ' Get all decimal fields in the table
                    cmd.CommandText = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}' AND DATA_TYPE = 'decimal'"
                    Dim decimalFields As New List(Of String)
                    Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                        While Await reader.ReadAsync()
                            decimalFields.Add(reader("COLUMN_NAME").ToString())
                        End While
                    End Using

                    ' Alter each decimal field to have 2 decimal places
                    For Each field As String In decimalFields
                        cmd.CommandText = $"ALTER TABLE {table} ALTER COLUMN {field} DECIMAL(18, 3)"
                        Await cmd.ExecuteNonQueryAsync()
                    Next
                Next

                MsgBox("Decimal fields updated successfully.")
            End Using
        Catch ex As Exception
            MsgBox($"An error occurred: {ex.Message}")
        End Try
    End Function

    Public Async Function UpdateDecimalFieldsAsync(connectionString As String) As Task
        Try
            Using con As New SqlConnection(connectionString)
                Await con.OpenAsync()
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandTimeout = 0 ' Set the command timeout to infinite

                ' Get all tables in the database
                cmd.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
                Dim tables As New List(Of String)
                Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                    While Await reader.ReadAsync()
                        tables.Add(reader("TABLE_NAME").ToString())
                    End While
                End Using

                ' Loop through each table
                For Each table As String In tables
                    ' Get all decimal fields in the table
                    cmd.CommandText = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}' AND DATA_TYPE = 'decimal'"
                    Dim decimalFields As New List(Of String)
                    Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                        While Await reader.ReadAsync()
                            decimalFields.Add(reader("COLUMN_NAME").ToString())
                        End While
                    End Using

                    ' Loop through each decimal field in the table
                    For Each field As String In decimalFields
                        ' If the field is named "Amount", allow maximum precision (38 total digits, 38 decimal places)
                        If field.Equals("Amount", StringComparison.OrdinalIgnoreCase) Or field.Equals("Change", StringComparison.OrdinalIgnoreCase) Then
                            cmd.CommandText = $"ALTER TABLE {table} ALTER COLUMN {field} DECIMAL(38, 18)"
                        Else
                            ' For other fields, use DECIMAL(18, 3)
                            cmd.CommandText = $"ALTER TABLE {table} ALTER COLUMN {field} DECIMAL(18, 3)"
                        End If
                        Await cmd.ExecuteNonQueryAsync()
                    Next
                Next

                MsgBox("Decimal fields updated successfully.")
            End Using
        Catch ex As Exception
            MsgBox($"An error occurred: {ex.Message}")
        End Try
    End Function


    Public Function ExecuteReader(query As String, params As List(Of SqlParameter)) As SqlDataReader
        Dim con As New SqlConnection(cs)
        con.Open()
        Dim cmd As New SqlCommand(query, con)
        For Each param In params
            cmd.Parameters.Add(param)
        Next
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function
    Function GetHardwareID() As String
        Dim hardwareID As String = String.Empty
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")
            For Each obj As ManagementObject In searcher.Get()
                hardwareID = obj("ProcessorId").ToString()
                Exit For
            Next
        Catch ex As Exception
            MsgBox("Unable to retrieve hardware ID: " & ex.Message)
        End Try
        Return hardwareID
    End Function

    Public Function CheckAndWriteHardwareID() As Boolean
        Dim appPath As String = AppDomain.CurrentDomain.BaseDirectory
        Dim filePath As String = filePathfirst_time
        Dim currentHardwareID As String = GetHardwareID()
        If File.Exists(filePath) Then
            Dim fileHardwareID As String = File.ReadAllText(filePath, Encoding.UTF8)
            If fileHardwareID = currentHardwareID Then
                Return True
            Else
                File.WriteAllText(filePath, currentHardwareID, Encoding.UTF8)
                Return False
            End If
        Else
            File.WriteAllText(filePath, currentHardwareID, Encoding.UTF8)
            Return False
        End If
    End Function


    Private Sub AddSubMenuItemsToDatabase(menuItem As ToolStripMenuItem, connection As SqlConnection)
        For Each subItem As ToolStripItem In menuItem.DropDownItems
            If TypeOf subItem Is ToolStripMenuItem Then
                Dim subMenuItem As ToolStripMenuItem = CType(subItem, ToolStripMenuItem)

                ' Check if the menu item already exists in the database
                Dim query As String = "SELECT COUNT(*) FROM [dbo].[ASECURITY] WHERE [Menu] = @Menu"
                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@Menu", subMenuItem.Text)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    ' If the menu item does not exist, insert it
                    If count = 0 Then
                        query = "INSERT INTO [dbo].[ASECURITY] ([Menu], [LEV1], [LEV2], [LEV3]) VALUES (@Menu, @LEV1, @LEV2, @LEV3)"
                        Using insertCmd As New SqlCommand(query, connection)
                            insertCmd.Parameters.AddWithValue("@Menu", subMenuItem.Text)
                            insertCmd.Parameters.AddWithValue("@LEV1", False) ' Set default value for LEV1
                            insertCmd.Parameters.AddWithValue("@LEV2", False) ' Set default value for LEV2
                            insertCmd.Parameters.AddWithValue("@LEV3", False) ' Set default value for LEV3
                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                ' Recursively add submenu items
                AddSubMenuItemsToDatabase(subMenuItem, connection)
            End If
        Next
    End Sub

    Sub AddMenuItemsToDatabase(connectionString As String, menuStrip As MenuStrip)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                For Each menuItem As ToolStripMenuItem In menuStrip.Items.OfType(Of ToolStripMenuItem)()
                    AddSubMenuItemsToDatabase(menuItem, con)
                Next
            End Using
        Catch ex As Exception
            MsgBox($"An error occurred: {ex.Message}")
        End Try
    End Sub




    Public Function GetCustomerByInvoiceNo(invNo As String) As Customer
        Dim cus_sql As String

        ' Check if invNo is NULL or empty
        If String.IsNullOrEmpty(invNo) Then
            ' Select from CUSTOMER table only
            cus_sql = "SELECT CC_ID, RTRIM(CustomerID) AS CustomerID, RTRIM(Name) AS Name, RTRIM(ContactNo) AS ContactNo, 
                   RTRIM(Address) AS Address, RTRIM(TRN) AS TRN, RegistrationDate, RTRIM(Active) AS Active, 
                   RTRIM(RateType) AS RateType, RTRIM(Discount) AS Discount, RTRIM(TradeName) AS TradeName, 
                   RTRIM(Street) AS Street, RTRIM(Email) AS Email, RTRIM(VatNumber) AS VatNumber, 
                   RTRIM(Province) AS Province, RTRIM(HouseNo) AS HouseNo, RTRIM(City) AS City, RTRIM(Cuspin) AS Cuspin
                   FROM CUSTOMER"
        Else
            ' Select from CUSTOMER and join with INVOICEINFO if invNo is provided
            cus_sql = $"SELECT CC_ID, RTRIM(CustomerID) AS CustomerID, RTRIM(Name) AS Name, RTRIM(ContactNo) AS ContactNo, 
                   RTRIM(Address) AS Address, RTRIM(TRN) AS TRN, RegistrationDate, RTRIM(Active) AS Active, 
                   RTRIM(RateType) AS RateType, RTRIM(Discount) AS Discount, RTRIM(TradeName) AS TradeName, 
                   RTRIM(Street) AS Street, RTRIM(Email) AS Email, RTRIM(VatNumber) AS VatNumber, 
                   RTRIM(Province) AS Province, RTRIM(HouseNo) AS HouseNo, RTRIM(City) AS City, RTRIM(Cuspin) AS Cuspin
                   FROM CUSTOMER 
                   INNER JOIN INVOICEINFO ON CUSTOMER.CC_ID = INVOICEINFO.CustID 
                   WHERE INVOICEINFO.InvoiceNo = '{invNo}'"
        End If

        Dim dt As DataTable = Crud(cus_sql, Nothing)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)

            ' Create and populate the Customer object
            Dim customer As New Customer() With {
            .CC_ID = Convert.ToInt32(row("CC_ID")),
            .CustomerID = row("CustomerID").ToString(),
            .Name = row("Name").ToString(),
            .ContactNo = row("ContactNo").ToString(),
            .Address = row("Address").ToString(),
            .TRN = row("TRN").ToString(),
            .RegistrationDate = If(IsDBNull(row("RegistrationDate")), CType(Nothing, DateTime?), Convert.ToDateTime(row("RegistrationDate"))),
            .Active = row("Active").ToString(),
            .RateType = row("RateType").ToString(),
            .Discount = row("Discount").ToString(),
            .TradeName = row("TradeName").ToString(),
            .Street = row("Street").ToString(),
            .Email = row("Email").ToString(),
            .VatNumber = row("VatNumber").ToString(),
            .Province = row("Province").ToString(),
            .HouseNo = row("HouseNo").ToString(),
            .City = row("City").ToString(),
            .Cuspin = row("Cuspin").ToString()
        }

            Return customer
        End If
        Return Nothing
    End Function


End Module
