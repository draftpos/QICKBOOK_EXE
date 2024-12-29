Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Module ERPNextSyncHelper
    ''' <summary>
    ''' Updates the `is_synced` column in the `tabSales Invoice` table for the specified invoice.
    ''' </summary>
    ''' <param name="erpConnectionString">The connection string for the ERPNext MySQL database.</param>
    ''' <param name="invoiceName">The name of the invoice to update.</param>
    Public Sub UpdateIsSynced(erpConnectionString As String, invoiceName As String)
        Dim updateSyncQuery As String = "
            UPDATE `tabSales Invoice`
            SET custom_syc_status = 'Synced'
            WHERE name = @InvoiceName;
        "

        Try
            ' Declare the connection outside the Using block to reuse it
            Dim erpConnection As New MySqlConnection(erpConnectionString)

            ' Check if the connection is already open
            If erpConnection.State = ConnectionState.Closed Then
                erpConnection.Open()
            End If

            ' Execute the update query
            Using updateCommand As New MySqlCommand(updateSyncQuery, erpConnection)
                updateCommand.Parameters.AddWithValue("@InvoiceName", invoiceName)
                updateCommand.ExecuteNonQuery()
                Console.WriteLine($"Updated is_synced to 'Yes' for invoice: {invoiceName} in ERPNext database.")
            End Using

            ' Close the connection if it was opened here
            ' If erpConnection.State = ConnectionState.Open Then
            'erpConnection.Close()
            'End If
        Catch ex As Exception
            Console.WriteLine($"Error updating is_synced for invoice {invoiceName}: {ex.Message}")
        End Try
    End Sub


    Public Sub AddOrUpdateCustomer(
        sqlConnectionString As String,
        currentCustomer As String,
        customerCode As String,
        customCustomerAddress As String,
        customCustomerTin As String,
        customCustomerVat As String,
        customCustomerPhone As String,
        customCustomerEmail As String,
        customCustomerCurrency As String)

        Try
            Using connection As New SqlConnection(sqlConnectionString)
                connection.Open()

                ' Check if the customer already exists by name
                Dim checkQuery As String = "SELECT COUNT(1) FROM [dbo].[Customer] WHERE [sClientName] = @ClientName"
                Dim exists As Boolean = False

                Using checkCommand As New SqlCommand(checkQuery, connection)
                    checkCommand.Parameters.AddWithValue("@ClientName", currentCustomer)
                    exists = Convert.ToBoolean(checkCommand.ExecuteScalar())
                End Using

                If exists Then
                    ' Update existing customer details
                    Dim updateQuery As String = "
                        UPDATE [dbo].[Customer]
                        SET 
                            [sClientCode] = @ClientCode,
                            [sClientGrpCode] = @ClientGrpCode,
                            [sClientGrpName] = @ClientGrpName,
                            [sRepCode] = @RepCode,
                            [sClassification] = @Classification,
                            [sAddress] = @Address,
                            [sPostCode] = @PostCode,
                            [sCity] = @City,
                            [sCountry] = @Country,
                            [sEmail] = @Email,
                            [sPhone] = @Phone,
                            [sPayTerms] = @PayTerms,
                            [sDistrictName] = @DistrictName,
                            [sProv] = @Prov,
                            [sTerrName] = @TerrName,
                            [sControlType] = @ControlType,
                            [sBannerName] = @BannerName,
                            [iAge] = @Age,
                            [dDateOfBirth] = @DateOfBirth,
                            [Sex] = @Sex,
                            [sSegment] = @Segment,
                            [sMarket] = @Market,
                            [sCustomerVat] = @CustomerVat,
                            [sCurrency] = @Currency,
                            [sCustomerTin] = @CustomerTin
                        WHERE [sClientName] = @ClientName"

                    Using updateCommand As New SqlCommand(updateQuery, connection)
                        AddCustomerParameters(updateCommand, currentCustomer, customerCode, customCustomerAddress, customCustomerTin, customCustomerVat, customCustomerPhone, customCustomerEmail, customCustomerCurrency)
                        updateCommand.ExecuteNonQuery()
                    End Using

                    Console.WriteLine("Customer details updated successfully.")
                Else
                    ' Insert new customer
                    Dim insertQuery As String = "
                        INSERT INTO [dbo].[Customer] (
                            [sClientCode], [sClientName], [sClientGrpCode], [sClientGrpName], [sRepCode], 
                            [sClassification], [sAddress], [sPostCode], [sCity], [sCountry], [sEmail], 
                            [sPhone], [sPayTerms], [sDistrictName], [sProv], [sTerrName], [sControlType], 
                            [sBannerName], [iAge], [dDateOfBirth], [Sex], [sSegment], [sMarket], [sCustomerVat], 
                            [sCurrency], [sCustomerTin]
                        ) 
                        VALUES (
                            @ClientCode, @ClientName, @ClientGrpCode, @ClientGrpName, @RepCode, 
                            @Classification, @Address, @PostCode, @City, @Country, @Email, 
                            @Phone, @PayTerms, @DistrictName, @Prov, @TerrName, @ControlType, 
                            @BannerName, @Age, @DateOfBirth, @Sex, @Segment, @Market, @CustomerVat, 
                            @Currency, @CustomerTin
                        )"

                    Using insertCommand As New SqlCommand(insertQuery, connection)
                        AddCustomerParameters(insertCommand, currentCustomer, customerCode, customCustomerAddress, customCustomerTin, customCustomerVat, customCustomerPhone, customCustomerEmail, customCustomerCurrency)
                        insertCommand.ExecuteNonQuery()
                    End Using

                    Console.WriteLine("New customer inserted successfully.")
                End If
            End Using
        Catch ex As Exception
            Console.WriteLine($"An error occurred: {ex.Message}")
        End Try
    End Sub

    Private Sub AddCustomerParameters(
        command As SqlCommand,
        currentCustomer As String,
        customerCode As String,
        customCustomerAddress As String,
        customCustomerTin As String,
        customCustomerVat As String,
        customCustomerPhone As String,
        customCustomerEmail As String,
        customCustomerCurrency As String)

        ' Add parameters to the SQL command
        command.Parameters.AddWithValue("@ClientCode", customerCode)
        command.Parameters.AddWithValue("@ClientName", If(currentCustomer, DBNull.Value))
        command.Parameters.AddWithValue("@ClientGrpCode", DBNull.Value)
        command.Parameters.AddWithValue("@ClientGrpName", DBNull.Value)
        command.Parameters.AddWithValue("@RepCode", DBNull.Value)
        command.Parameters.AddWithValue("@Classification", DBNull.Value)
        command.Parameters.AddWithValue("@Address", If(customCustomerAddress, DBNull.Value))
        command.Parameters.AddWithValue("@PostCode", DBNull.Value)
        command.Parameters.AddWithValue("@City", DBNull.Value)
        command.Parameters.AddWithValue("@Country", DBNull.Value)
        command.Parameters.AddWithValue("@Email", customCustomerEmail)
        command.Parameters.AddWithValue("@Phone", If(customCustomerPhone, DBNull.Value))
        command.Parameters.AddWithValue("@PayTerms", DBNull.Value)
        command.Parameters.AddWithValue("@DistrictName", DBNull.Value)
        command.Parameters.AddWithValue("@Prov", DBNull.Value)
        command.Parameters.AddWithValue("@TerrName", DBNull.Value)
        command.Parameters.AddWithValue("@ControlType", DBNull.Value)
        command.Parameters.AddWithValue("@BannerName", DBNull.Value)
        command.Parameters.AddWithValue("@Age", DBNull.Value)
        command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value)
        command.Parameters.AddWithValue("@Sex", DBNull.Value)
        command.Parameters.AddWithValue("@Segment", DBNull.Value)
        command.Parameters.AddWithValue("@Market", DBNull.Value)
        command.Parameters.AddWithValue("@CustomerVat", If(customCustomerVat, DBNull.Value))
        command.Parameters.AddWithValue("@Currency", If(customCustomerCurrency, DBNull.Value))
        command.Parameters.AddWithValue("@CustomerTin", If(customCustomerTin, DBNull.Value))
    End Sub

End Module
