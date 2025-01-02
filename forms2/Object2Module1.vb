Imports System.Data.SqlClient
Imports System.IO
Imports WinFormsApp1.WinFormsApp1.forms

Module Object2Module1
    Function PrintA4(ByVal InvNo As String) As Boolean
        If InvNo.Trim().Length = 0 Then
            Return False
        End If

        frmReport.Dispose()
        Dim queryproductand_info As String = $"
SELECT 
    c.sClientName AS CustomerName,
    c.sPhone AS CustomerContact,
    i.InvoiceNumber AS InvoiceNo,
    i.TxnDate AS InvoiceDate,
    i.Subtotal AS Subtotal,
    i.SalesTaxTotal AS SalesTax,
    i.BalanceRemaining AS TotalAmount,
    i.Currency AS InvoiceCurrency,
    i.TxnId AS Reference,
    i.QRcode, 
    i.DeviceID, 
    i.FiscalDay, 
    i.CustomerRef, 
    i.ReceiptNo, 
    i.Vcode, 
    i.HavanoZimraStatus,
    'Reason' AS Reason,
    it.Name AS ProductName,
    it.Qty AS Quantity,
    it.Rate AS SalesRate,
    it.Amount AS ItemTotal,
    it.Vat AS VatStatus,
    CASE 
        WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount
        ELSE 0
    END AS ItemVat,
    SUM(CASE 
        WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount
        ELSE 0
    END) OVER () AS TotalVat,
    SUM(it.Amount) OVER () AS Total,
    SUM(it.Amount) OVER () - SUM(CASE 
        WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount
        ELSE 0
    END) OVER () AS Total_Exclusive
FROM 
    [dbo].[Invoice] i
INNER JOIN 
    [dbo].[Customer] c ON i.CustomerListId = c.sClientCode
INNER JOIN 
    [dbo].[Item] it ON i.TxnId = it.TxnId
WHERE 
    i.InvoiceNumber = @InvNo
    AND it.Qty > 0;
"

        con = New SqlConnection(cs)
        con.Open()
        Dim ct As String = queryproductand_info
        cmd = New SqlCommand(ct)
        cmd.Parameters.AddWithValue("@InvNo", InvNo)
        cmd.Connection = con
        rdr = cmd.ExecuteReader()
        If Not rdr.Read() Then
            MessageBox.Show("Invoice no. does not exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If (rdr IsNot Nothing) Then
                rdr.Close()
            End If
            Return False
        End If
        Dim sql As String = $"SELECT C.[id] AS CC_ID, C.[sClientCode] AS CustomerID, C.[sClientName] AS Name, " &
                    "C.[sPhone] AS ContactNo, C.[sAddress] AS Address, C.[sCustomerTin] AS TRN, " &
                    "C.[dDateOfBirth] AS RegistrationDate, C.[Sex] AS Active, C.[sSegment] AS RateType, " &
                    "NULL AS Discount, C.[sTerrName] AS TradeName, C.[sCity] AS Street, " &
                    "C.[sEmail] AS Email, C.[sCustomerVat] AS VatNumber, C.[sProv] AS Province, " &
                    "NULL AS HouseNo, C.[sCity] AS City, C.[sClientGrpCode] AS Cuspin " &
                    "FROM [dbo].[Customer] C " &
                    "INNER JOIN [dbo].[Invoice] I ON I.CustomerListId = C.sClientCode " &
                   $"WHERE I.InvoiceNumber = '{InvNo}'"

        Dim mysqlparameter As New List(Of SqlParameter)()
        mysqlparameter.Add(New SqlParameter("@InvNo", InvNo))
        Dim dt As DataTable = Crud(sql, mysqlparameter)
        Dim rpt As New DReport1 'Therpt you created.
        Dim myConnection As SqlConnection
        Dim MyCommand, MyCommand1, mycmd3, mycmd4 As New SqlCommand()
        Dim myDA, myDA1, myda3, myda4 As New SqlDataAdapter()
        Dim myDS As New DataSet 'The DataSet you created.
        myConnection = New SqlConnection(cs)
        MyCommand.Connection = myConnection
        MyCommand1.Connection = myConnection
        mycmd3.Connection = myConnection
        mycmd4.Connection = myConnection
        '  rpt.PageFooterSection1.SectionFormat.EnableSuppress = True
        dtable2 = Crud(queryproductand_info, Nothing)
        con.Close()
        con = New SqlConnection(cs)
        con.Open()
        Dim cl4 As String = sql
        cmd = New SqlCommand(cl4)
        cmd.Connection = con
        cmd.Parameters.AddWithValue("@InvNo", InvNo)
        cmd.CommandTimeout = 0
        rdr = cmd.ExecuteReader()
        'If rdr.Read() Then
        Cursor.Current = Cursors.WaitCursor
        myConnection = New SqlConnection(cs)
        MyCommand.Connection = myConnection
        MyCommand1.Connection = myConnection
        MyCommand.CommandText = queryproductand_info
        MyCommand.Parameters.AddWithValue("@InvNo", InvNo)
        MyCommand.CommandTimeout = 0
        MyCommand1.CommandText = "SELECT * from Company"
        MyCommand.CommandType = CommandType.Text
        MyCommand1.CommandType = CommandType.Text
        mycmd4.CommandText = sql
        myDA.SelectCommand = MyCommand
        myDA1.SelectCommand = MyCommand1
        myda3.SelectCommand = mycmd3
        myda4.SelectCommand = mycmd4
        ' myDA.Fill(myDS, "InvoiceInfo")
        'myDA.Fill(myDS, "Invoice_Product")
        myDA.Fill(myDS, "Product")
        ' myDA.Fill(myDS, "Invoice_Payment")
        myDA1.Fill(myDS, "Company")
        myda4.Fill(myDS, "Customer")
        myDS.WriteXmlSchema("crys_xml.xml")
        rpt.Subreports(0).SetDataSource(myDS)
        rpt.Subreports(1).SetDataSource(myDS)
        rpt.Subreports(2).SetDataSource(myDS)
        rpt.SetDataSource(myDS)
        rpt.SetParameterValue("VatFlag", VatFlag)
        rpt.SetParameterValue("Tax5", Tax5)
        rpt.SetParameterValue("Tax15", Tax15)
        rpt.SetParameterValue("Tax0", Tax0)
        rpt.SetParameterValue("TaxE", TaxE)
        rpt.SetParameterValue("TIN", TIN)
        rpt.SetParameterValue("VAT", VAT)
        rpt.SetParameterValue("activationKey", activationKey)
        rpt.SetParameterValue("deviceSerialNo", deviceSerialNo)
        rpt.SetParameterValue("DeviceID", DeviceID)
        rpt.SetParameterValue("DeviceModelName", DeviceModelName)
        rpt.SetParameterValue("DeviceModelVersion", DeviceModelVersion)
        rpt.SetParameterValue("ZimraServer", ZimraServer)
        rpt.SetParameterValue("VerificationServer", VerificationServer)
        rpt.SetParameterValue("ReceiptCounter", ReceiptCounter)
        rpt.SetParameterValue("ReceiptGlobalNo", ReceiptGlobalNo)
        rpt.SetParameterValue("ReceiptPrintForm", ReceiptPrintForm)
        rpt.SetParameterValue("FiscalDayNo", FiscalDayNo)
        rpt.SetParameterValue("FiscalDate", FiscalDate)
        rpt.SetParameterValue("PreviousReceiptHash", PreviousReceiptHash)
        rpt.SetParameterValue("FiscalDayStatus", FiscalDayStatus)
        frmReport.CrystalReportViewer1.ReportSource = rpt
        frmReport.ShowDialog()
        Return True '   Exit Sub
        '   End If

        ' Catch ex As Exception
        'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
        Return True
    End Function



    Function creditnotePrintA4(ByVal InvNo As String) As Boolean
        If InvNo.Trim().Length = 0 Then
            Return False
        End If
        frmReport.Dispose()
        Dim queryproductand_info As String = $"
SELECT 
      c.sClientName AS CustomerName,
    c.sPhone AS CustomerContact,
    i.CreditNoteNumber AS InvoiceNo,
    i.TxnDate AS InvoiceDate,
    i.Subtotal AS Subtotal,
    i.SalesTaxTotal AS SalesTax,
    i.TotalAmount AS TotalAmount,
i.QRcode,i.DeviceID,i.FiscalDay,i.CustomerRef,i.Vcode,i.HavanoZimraStatus,
'$' AS InvoiceCurrency,
      i.TxnId AS Reference,
      i.CreditRemaining AS CreditRemaining,
i.Message As Reaon,
    it.Name AS ProductName,
    it.Qty AS Quantity,
    it.Rate AS SalesRate,
    it.Amount AS ItemTotal,
 it.Vat AS vatstat ,
CASE 
        WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount
        ELSE 0
    END AS ItemVat,
    SUM(CASE 
        WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount
        ELSE 0
    END) OVER () AS TotalVat,
    SUM(it.Amount) OVER () AS Total,
    SUM(it.Amount) OVER () - SUM(CASE 
        WHEN LOWER(it.vat) = 's'  or  rtrim(it.vat)='15' THEN (15.0 / 115.0) * it.Amount
        ELSE 0
    END) OVER () AS Total_Exclusive 
  FROM 
    [dbo].[CreditMemo] i
INNER JOIN 
    [dbo].[Customer] c ON i.CustomerListId = c.sClientCode
INNER JOIN 
    [dbo].[Item] it ON i.TxnId = it.TxnId
WHERE 
    i.CreditNoteNumber = '{InvNo}' 
    AND it.Qty > 0;
    "
        'Try
        con = New SqlConnection(cs)
        con.Open()
        Dim ct As String = queryproductand_info
        cmd = New SqlCommand(ct)
        cmd.Parameters.AddWithValue("@d1", InvNo)
        cmd.Connection = con
        rdr = cmd.ExecuteReader()
        If Not rdr.Read() Then
            MessageBox.Show("Invoice no. does not exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If (rdr IsNot Nothing) Then
                rdr.Close()
            End If
            Return False
        End If
        Dim sql As String = $"SELECT C.[id] AS CC_ID, C.[sClientCode] AS CustomerID, C.[sClientName] AS Name, " &
                    "C.[sPhone] AS ContactNo, C.[sAddress] AS Address, C.[sCustomerTin] AS TRN, " &
                    "C.[dDateOfBirth] AS RegistrationDate, C.[Sex] AS Active, C.[sSegment] AS RateType, " &
                    "NULL AS Discount, C.[sTerrName] AS TradeName, C.[sCity] AS Street, " &
                    "C.[sEmail] AS Email, C.[sCustomerVat] AS VatNumber, C.[sProv] AS Province, " &
                    "NULL AS HouseNo, C.[sCity] AS City, C.[sClientGrpCode] AS Cuspin " &
                    "FROM [dbo].[Customer] C " &
                    "INNER JOIN [dbo].[CreditMemo] I ON I.CustomerListId = C.sClientCode " &
                   $"WHERE I.CreditNoteNumber = '{InvNo}'"

        Dim mysqlparameter As New List(Of SqlParameter)()
        mysqlparameter.Add(New SqlParameter("@d1", InvNo))
        Dim dt As DataTable = Crud(sql, mysqlparameter)
        Dim rpt As New creditnote 'Therpt you created.
        Dim myConnection As SqlConnection
        Dim MyCommand, MyCommand1, mycmd3, mycmd4 As New SqlCommand()
        Dim myDA, myDA1, myda3, myda4 As New SqlDataAdapter()
        Dim myDS As New DataSet 'The DataSet you created.
        myConnection = New SqlConnection(cs)
        MyCommand.Connection = myConnection
        MyCommand1.Connection = myConnection
        mycmd3.Connection = myConnection
        mycmd4.Connection = myConnection
        '  rpt.PageFooterSection1.SectionFormat.EnableSuppress = True
        dtable2 = Crud(queryproductand_info, Nothing)
        con.Close()
        con = New SqlConnection(cs)
        con.Open()
        Dim cl4 As String = sql
        cmd = New SqlCommand(cl4)
        cmd.Connection = con
        cmd.Parameters.AddWithValue("@d1", InvNo)
        cmd.CommandTimeout = 0
        rdr = cmd.ExecuteReader()
        '    If rdr.Read() Then
        Cursor.Current = Cursors.WaitCursor
        myConnection = New SqlConnection(cs)
        MyCommand.Connection = myConnection
        MyCommand1.Connection = myConnection
        MyCommand.CommandText = queryproductand_info
        MyCommand.Parameters.AddWithValue("@d1", InvNo)
        MyCommand.CommandTimeout = 0
        MyCommand1.CommandText = "SELECT * from Company"
        MyCommand.CommandType = CommandType.Text
        MyCommand1.CommandType = CommandType.Text
        mycmd4.CommandText = sql
        myDA.SelectCommand = MyCommand
        myDA1.SelectCommand = MyCommand1
        myda3.SelectCommand = mycmd3
        myda4.SelectCommand = mycmd4
        ' myDA.Fill(myDS, "InvoiceInfo")
        'myDA.Fill(myDS, "Invoice_Product")
        myDA.Fill(myDS, "Product")
        ' myDA.Fill(myDS, "Invoice_Payment")
        myDA1.Fill(myDS, "Company")
        myda4.Fill(myDS, "Customer")
        myDS.WriteXmlSchema("creditnotel.xml")
        'myDS.WriteXmlSchema("crys_xml.xml")
        rpt.Subreports(0).SetDataSource(myDS)
        rpt.Subreports(1).SetDataSource(myDS)
        rpt.Subreports(2).SetDataSource(myDS)
        '  rpt.Subreports(3).SetDataSource(myDS)
        rpt.SetDataSource(myDS)
        ' report.SetParameterValue("FiscalDate", FiscalDate)
        rpt.SetParameterValue("FiscalDayNo", FiscalDayNo)
         rpt.SetParameterValue("deviceSerialNo", deviceSerialNo)
        rpt.SetParameterValue("DeviceID", DeviceID)
        frmReport.CrystalReportViewer1.ReportSource = rpt
        frmReport.ShowDialog()
        Return True '   Exit Sub
        ' End If

        ' Catch ex As Exception
        'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
        Return True
    End Function
    Public Async Function companytables_and_data() As Task
        ' Check if the "Company" table exists, create it if not
        Dim sql As String
        sql = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Company') " &
              "BEGIN " &
              "CREATE TABLE [dbo].[Company] (" &
              "[Id] [int] IDENTITY(1,1) NOT NULL," &
              "[CompanyName] NVARCHAR(255)," &
              "[MailingName] NVARCHAR(255)," &
              "[Country] NVARCHAR(255)," &
              "[Address] NVARCHAR(255)," &
              "[City] NVARCHAR(255)," &
              "[State] NVARCHAR(255)," &
              "[PinCode] NVARCHAR(50)," &
              "[ContactNo] NVARCHAR(50)," &
              "[Email] NVARCHAR(255)," &
              "[ShowLogo] NVARCHAR(10)," &
              "[CurrencyCode] NVARCHAR(10)," &
              "[Currency] NVARCHAR(50)," &
              "[InvoiceHeader] NVARCHAR(255)," &
              "[wscalable] BIT," &
              "[pscalable] BIT," &
              "[MultiCurrencyReceipt] BIT," &
              "[ShowMultiCurrency] BIT," &
              "[RevMaxKey] NVARCHAR(MAX)," &
              "[ShowDiscount] NVARCHAR(10)," &
              "[VatNo] NVARCHAR(50)," &
              "[EnableRevMax] NVARCHAR(10)," &
              "[selnegative] NVARCHAR(10)," &
              "[ZeroPrice] NVARCHAR(10)," &
              "[BelowCost] NVARCHAR(10)," &
              "[ActiveBelow] NVARCHAR(10)," &
              "[NP] INT," &
              "[QCode] NVARCHAR(10)," &
              "[BCode] NVARCHAR(10)," &
              "[Website] NVARCHAR(255)," &
              "[TIN] NVARCHAR(50)," &
             "[Logo] IMAGE," &
              "[ItemWiseVAT] NVARCHAR(10)," &
              "[QTC] [nvarchar](max) NULL," &
              "[ServiceTaxNo] NVARCHAR(50)" &
                            ") END"

        Dim dt As DataTable = Crud(sql, Nothing)

        ' Check if the "Company" table contains data, insert default data if empty
        sql = "IF NOT EXISTS (SELECT 1 FROM [dbo].[Company]) " &
              "BEGIN " &
              "INSERT INTO [dbo].[Company] (" &
              "[CompanyName], [MailingName], [Country], [Address], [City], [State], [PinCode], [ContactNo], " &
              "[Email], [ShowLogo], [CurrencyCode], [Currency], [InvoiceHeader], [wscalable], [pscalable], " &
              "[MultiCurrencyReceipt], [ShowMultiCurrency], [RevMaxKey], [ShowDiscount], [VatNo], [EnableRevMax], " &
              "[selnegative], [ZeroPrice], [BelowCost], [ActiveBelow], [NP], [QCode], [BCode], [Website], [TIN], " &
              "[Logo], [ItemWiseVAT], [ServiceTaxNo]" &
              ") VALUES (" &
              "'Havano Stores', 'Barcode with Price', 'Zimbabwe', 'No. 56 Rhodes Streets', 'Chivhu', 'Harare', " &
              "'263', '0771713407', 'info@havano.net', 'Yes', 'USD', 'USD', 'FISCAL TAX INVOICE', 0, 0, 1, 1, " &
              "'<Binary data>', 'false', '800001', 'false', 'false', 'No', 'No', 'No', 1, 'No', 'Yes', " &
              "'80mm RollPaper', '900001', '<Binary data>', 'Yes', 'Thanks....Visit Again!'" &
              ") END"
        dt = Crud(sql, Nothing)
        Return
    End Function

    Public Async Function Automatically_BackUp() As Task
        Dim backupDir As String = "C:\Havano_Fiscal_DB"
        Dim hiddenBackupDir As String = "C:\hidden_Havano_Fiscal"

        ' Ensure backup directory exists
        If Not Directory.Exists(backupDir) Then
            Directory.CreateDirectory(backupDir)
        End If

        ' Ensure hidden backup directory exists and is hidden
        If Not Directory.Exists(hiddenBackupDir) Then
            Directory.CreateDirectory(hiddenBackupDir)
            Dim dirInfo As New DirectoryInfo(hiddenBackupDir)
            dirInfo.Attributes = FileAttributes.Directory Or FileAttributes.Hidden
        End If

        ' Backup file name with date and time
        Dim today As String = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
        Dim backupFileName As String = "Havano_Fiscal_DB_" & today & ".bak"
        Dim backupFilePath As String = Path.Combine(backupDir, backupFileName)

        ' Check if the database is not corrupt before backup
        ' If Not Await IsDatabaseHealthy() Then
        'MsgBox("The database is corrupt. Backup aborted.", MsgBoxStyle.Critical, "Backup Error")
        'Exit Function
        'End If

        If File.Exists(backupFilePath) Then
            File.Delete(backupFilePath)
        End If

        Dim backupFiles As FileInfo() = New DirectoryInfo(backupDir).GetFiles("Havano_Fiscal_DB_*.bak").OrderBy(Function(f) f.CreationTime).ToArray()
        If backupFiles.Length > 30 Then
            For i As Integer = 0 To backupFiles.Length - 31
                Dim file1 As FileInfo = backupFiles(i)
                Dim hiddenFilePath As String = Path.Combine(hiddenBackupDir, file1.Name)
                If File.Exists(hiddenFilePath) Then
                    File.Delete(hiddenFilePath)
                End If
                file1.MoveTo(hiddenFilePath)
            Next
        End If
        SqlConnection.ClearAllPools()

        ' Perform the backup operation asynchronously
        Await Task.Run(Sub()
                           Using con As New SqlConnection(cs)
                               con.Open()
                               Dim backupQuery As String = "BACKUP DATABASE Fetchinv TO DISK='" & backupFilePath & "' WITH INIT, STATS=10"
                               Using cmd As New SqlCommand(backupQuery, con)
                                   cmd.CommandTimeout = 0
                                   cmd.ExecuteNonQuery()
                               End Using
                           End Using
                       End Sub)

    End Function


    Private Async Function IsDatabaseHealthy() As Task(Of Boolean)
        Try
            Using con As New SqlConnection(cs)
                Await con.OpenAsync()
                Dim cmd As New SqlCommand("DBCC CHECKDB('Fetchinv') WITH NO_INFOMSGS, ALL_ERRORMSGS", con)
                cmd.CommandTimeout = 0
                Await cmd.ExecuteNonQueryAsync()
            End Using
            Return True
        Catch ex As Exception
            ' Log or display the exception if needed
            Return False
        End Try
    End Function

End Module
