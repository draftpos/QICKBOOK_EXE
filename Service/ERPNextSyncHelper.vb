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
End Module
