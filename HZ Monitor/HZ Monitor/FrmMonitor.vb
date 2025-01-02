Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.IO
Imports System.Net.Http
Imports System.Threading
Imports HavanoZimra
Imports MessagingToolkit.QRCode.Codec
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class FrmMonitor

    Dim DeviceID, FiscalDay, receiptGlobalNo, VerificationCode As String
    Dim selCurrency As String
    Dim message As String
    Dim CustomerID As String = ""
    Dim CusID, Currency, BranchName, InvoiceNo, CusCompanyName, VatNo, CompanyAddress As String
    Dim TIN, CompanyPhoneNo, CompanyBPN As String
    Dim InvoiceFlag, Cashier, InvoiceComment As String
    Dim item_xml As String
    Dim InvoiceAmount As Decimal = 0
    Dim InvoiceTaxAmount As Decimal = 0
    Dim TxnID_Lst As List(Of String) = New List(Of String)
    Dim AddCustomer2Zimra As String = 0
    Dim TrnxType As String = 0
    Dim CustomerName, TradeName, CustomerVATNumber, CustomerAddress, CustomerTelephoneNumber, CustomerTIN As String
    Dim CustomerEmail, CustomerProvince, CustomerStreet, CustomerHouseNo, CustomerCity As String
    Dim mytable As String = ""
    Private appMutex As Mutex
    Private Sub FrmMonitor_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            MyNotifyIcon.Visible = True
            Me.Hide()
        End If
    End Sub
    Private Sub RestoreApp(sender As Object, e As EventArgs)
        ' Restore the application
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        MyNotifyIcon.Visible = False
    End Sub

    Private Sub ExitApp(sender As Object, e As EventArgs)
        ' Exit the application
        MyNotifyIcon.Visible = False
        Application.Exit()
    End Sub

    Private Sub MyNotifyIcon_DoubleClick(sender As Object, e As EventArgs) Handles MyNotifyIcon.DoubleClick
        RestoreApp(sender, e)
    End Sub

    Private Sub btnMini_Click(sender As Object, e As EventArgs) Handles btnMini.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Dim InvoiceRecieptNo As String = ""

    Private Sub lblStatus_Click(sender As Object, e As EventArgs) Handles lblStatus.Click
        'FrmTest.ShowDialog()
    End Sub

    Dim OriginalInvoiceNo As String = ""
    Dim CreditNoteNo As String
    Private Sub tmr_start_Tick(sender As Object, e As EventArgs) Handles tmr_start.Tick
        StartSendingInvoice()
        'tmr_start.Stop()
    End Sub

    Private Sub btnImgExit_Click(sender As Object, e As EventArgs) Handles btnImgExit.Click
        Application.Exit()
    End Sub

    Sub InitCulturalFormattingChanges()
        Dim cultureInfo As CultureInfo = DirectCast(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
        cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        Thread.CurrentThread.CurrentCulture = cultureInfo
    End Sub

    Private Async Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        btnStart.Text = "Starting..."
        Dim hm As New HavanoZimralib
        Dim ZiminfoRes As String = Await hm.SendPrivateRequest(HavanoZimralib.ReqType.Config, HttpMethod.Get)
        Dim msg As String = Await hm.OpenFiscalDay()
        My.Settings.HavanoZimraDevice = hm.GetDeviceSerialNumber()
        My.Settings.Save()
        MessageBox.Show(Me, msg, "HavanoZimra Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information)

        btnStart.Text = "Start Fical Day"
        lblnotify.Text = "Waiting for Havano Fiscal request..."
        tmr_start.Enabled = True
        tmr_start.Start()
    End Sub
    Function CheckFilesAndFolders() As String
        Dim startupPath As String = Application.StartupPath
        Dim certFolderPath As String = Path.Combine(startupPath, "havano_cert")
        Dim configFilePath As String = Path.Combine(startupPath, "havanoconfig.ini")

        Dim resultMessage As String = ""
        If Not Directory.Exists(certFolderPath) Then
            resultMessage &= "The 'havano_cert' folder does not exist." & Environment.NewLine
        End If

        If Not File.Exists(configFilePath) Then
            resultMessage &= "The 'havanoconfig.ini' file does not exist."
        End If
        Return resultMessage
    End Function

    Private Async Sub FrmMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim result As String = CheckFilesAndFolders()
        If result <> "" Then
            MessageBox.Show(result, "Check Files and Folders", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Application.Exit()
        End If

        InitCulturalFormattingChanges()
        MyNotifyIcon.Text = "HavanoZimra Monitor"
        MyNotifyIcon.Visible = False


        ' Add a context menu to the NotifyIcon
        Dim contextMenu As New ContextMenuStrip()
        contextMenu.Items.Add("Restore", Nothing, AddressOf RestoreApp)
        contextMenu.Items.Add("Exit", Nothing, AddressOf ExitApp)
        MyNotifyIcon.ContextMenuStrip = contextMenu
        Try
            Dim hm As New HavanoZimralib
            Dim IsInternetOkay As Boolean = Await hm.IsInternetAvailable
            If IsInternetOkay Then
                Dim res As String = Await hm.CheckGlobalNumber()
                Dim jsonObject As JObject = JObject.Parse(res)
                Dim message = jsonObject("Message").ToString()
                If message <> "Good" Then
                    MessageBox.Show(Me, message, "HavanoPOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            My.Settings.HavanoZimraDevice = hm.GetDeviceSerialNumber()
            My.Settings.VerificationServer = hm.VerificationServer()
            My.Settings.Save()

            Dim resMsg As String = Await hm.SendPrivateRequest(HavanoZimralib.ReqType.Status, HttpMethod.Get)
            Dim maxdayHrs As Double = hm.GetMaxDayHours

            If resMsg = "FiscalDayClosed" Then
                resMsg = "Fiscal Day is Closed, Please Open fiscal day"
                lblStatus.Text = "Fiscal Day is Closed"
                tmr_start.Enabled = False
                MessageBox.Show(Me, resMsg, "HavanoPOS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Show()
                Exit Sub
            Else
                lblStatus.Text = "Fiscal Day is Opened"
                lblnotify.Text = "Waiting for Havano Fiscal request..."
                tmr_start.Enabled = True
                tmr_start.Start()
            End If



            If maxdayHrs < 0 Then
                MessageBox.Show(Me, "Invalid Date found in the configuration file", "HavanoPOS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                tmr_start.Enabled = False
                Exit Sub
            End If

            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            Me.WindowState = FormWindowState.Normal
            lblStatus.Text = "Fiscal Day status unknown"
            tmr_start.Enabled = False
        End Try
        MyNotifyIcon.ShowBalloonTip(1000)
        lblnotify.Text = "Waiting for Havano Fiscal request..."
        tmr_start.Enabled = True
        tmr_start.Start()
        'FrmTest.ShowDialog()
    End Sub
    Private Async Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        btnClose.Enabled = False
        Try
            btnClose.Text = "Closing..."

            Dim hm As New HavanoZimralib

            Dim msg As String = Await hm.CloseFiscalDay()
            Thread.Sleep(7000)
            Dim resMsg As String = Await hm.SendPrivateRequest(HavanoZimralib.ReqType.Status, HttpMethod.Get)
            'If resMsg = "FiscalDayCloseFailed" Then resMsg = "Fiscal Day Close Failed"

            If resMsg = "FiscalDayOpened" Then resMsg = "Fiscal Day Close Failed"
            MessageBox.Show(Me, resMsg, "HavanoZimra Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If resMsg = "FiscalDayClosed" Then
                hm.UpdateCloseDay()
                tmr_start.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "HavanoZimra Monitor Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnClose.Text = "Close Fiscal Day"
            btnClose.Enabled = True
        End Try
        btnClose.Text = "Close Fiscal Day"
        btnClose.Enabled = True
    End Sub

    Public Async Sub CheckHavanoZimraData()
        Dim hm As New HavanoZimralib
        My.Settings.HavanoZimraDevice = hm.GetDeviceSerialNumber()
        My.Settings.VerificationServer = hm.VerificationServer()
        My.Settings.Save()

        Dim resMsg As String = Await hm.SendPrivateRequest(HavanoZimralib.ReqType.Status, HttpMethod.Get)
        Dim maxdayHrs As Double = hm.GetMaxDayHours

        If resMsg = "FiscalDayClosed" Then
            resMsg = "Fiscal Day is Closed, Please Open fiscal day"
            MessageBox.Show(Me, resMsg, "HavanoZimra Monitor Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If maxdayHrs < 0 Then
            MessageBox.Show(Me, "Invalid Date found in the configuration file", "HavanoZimra Monitor Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub
    Public Function CheckInvoice() As Boolean
        Dim isAvailable As Boolean = False
        TxnID_Lst.Clear()
        con = New SqlConnection(cs)
        con.Open()
        cmd = con.CreateCommand()
        cmd = New SqlCommand("select TxnId from Invoice WHERE HavanoZimraStatus=0", con)
        rdr = cmd.ExecuteReader()
        While rdr.Read()
            TxnID_Lst.Add(rdr(0).ToString)
            isAvailable = True
        End While
        con.Close()
        Return isAvailable
    End Function
    Public Function CheckCreditNote() As Boolean
        Dim isAvailable As Boolean = False
        TxnID_Lst.Clear()
        con = New SqlConnection(cs)
        con.Open()
        cmd = con.CreateCommand()
        cmd = New SqlCommand("select CreditNoteNumber from CreditMemo WHERE HavanoZimraStatus=0", con)
        rdr = cmd.ExecuteReader()
        While rdr.Read()
            TxnID_Lst.Add(rdr(0).ToString)
            isAvailable = True
        End While
        con.Close()
        Return isAvailable
    End Function
    Public Function GenerateRandomItemID() As String
        Dim random As New Random()
        Dim randomNumber As Integer = random.Next(10000000, 100000000) ' Generate a number between 10000000 and 99999999
        Return randomNumber.ToString()
    End Function
    Sub GetItemXML(ByVal trnxid As String)
        Dim xmlstr As String = "<ITEMS>"
        Dim itm_list As String = ""
        Dim full_item As String = ""
        Dim rcount As Integer = 0
        Dim TotaltaxAmount As Decimal = 0
        Dim TotalAmount As Decimal = 0
        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = con.CreateCommand()
            cmd = New SqlCommand("select Name,Qty,Rate,Amount,Vat from Item WHERE TxnId='" + trnxid + "'", con)
            rdr = cmd.ExecuteReader()
            While rdr.Read()
                rcount = rcount + 1
                Dim tax As Decimal = Convert.ToDecimal(rdr(4).ToString)
                Dim taxRate As Decimal = Convert.ToDecimal(tax) / 100
                Dim tax_amount As Decimal = (Convert.ToDecimal(rdr(3).ToString) * taxRate) / (1 + taxRate)

                Dim itm As String = String.Format(
                        "<ITEM>" &
                        "<HH>{0}</HH>" &
                        "<ITEMCODE>{1}</ITEMCODE>" &
                        "<ITEMNAME>{2}</ITEMNAME>" &
                        "<QTY>{3}</QTY>" &
                        "<PRICE>{4}</PRICE>" &
                        "<TOTAL>{5}</TOTAL>" &
                        "<VAT>{6}</VAT>" &
                        "<VATR>{7}</VATR>" &
                        "</ITEM>", rcount, GenerateRandomItemID, rdr(0).ToString, rdr(1).ToString, rdr(2).ToString, rdr(3).ToString, tax_amount.ToString("N2"), taxRate.ToString("N2"))

                full_item = String.Join("", itm)
                itm_list = itm_list + full_item
            End While
        Catch ex As Exception
            lblStatus.Text = "Failed: Some Item Information are not Valid"
        End Try

        con.Close()

        xmlstr = xmlstr + itm_list + "</ITEMS>"
        item_xml = xmlstr
        'Console.WriteLine(xmlstr)
    End Sub

    Public Async Function SendTax() As Tasks.Task
        Dim response As String = ""
        'Dim CurrRate As Decimal = 1.0
        If item_xml = "<ITEMS></ITEMS>" Then
            Exit Function
        End If
        item_xml = item_xml.Replace("&", "and")
        GetCompanyDetails()

        Dim hm As New HavanoZimralib
        Dim res As String
        For Each id In TxnID_Lst
            GetInvoiceDetails(id, mytable)
            If mytable = "CreditMemo" Then
                GetCreditNoteDetails(OriginalInvoiceNo)
            End If
            res = Await hm.SendInvoice(AddCustomer2Zimra, TrnxType, Currency, CusCompanyName, InvoiceNo, CustomerName, TradeName, CustomerVATNumber, CustomerAddress, CustomerTelephoneNumber, CustomerTIN, CustomerProvince, CustomerStreet, CustomerHouseNo, CustomerCity, CustomerEmail, InvoiceAmount, InvoiceTaxAmount, "Customer Return", OriginalInvoiceNo, InvoiceRecieptNo, item_xml)
            'Console.WriteLine(res)
            Dim jsonObject As JObject = JObject.Parse(res)
            message = jsonObject("Message").ToString()
            Try
                Dim qr_code As String = jsonObject("QRcode").ToString()
                DeviceID = jsonObject("DeviceID").ToString()
                FiscalDay = jsonObject("FiscalDay").ToString()
                receiptGlobalNo = jsonObject("receiptGlobalNo").ToString()
                VerificationCode = jsonObject("VerificationCode").ToString()
                UpdateQRCode(qr_code, id, mytable)
                lblStatus.Text = "HavanoZimra Status: " & message
                'MessageBox.Show(Me, "HavanoZimra Status: " & message, "HavanoPOS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                lblStatus.Text = "HavanoZimra Status: " & message
                'MessageBox.Show(Me, "HavanoZimra Status: " & message, "HavanoPOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Thread.Sleep(4000)
        Next
        lblnotify.Text = "Waiting for Havano Fiscal request..."
        lblStatus.Text = "Fiscal Day Opened"
        'tmr_start.Start()
        tmr_start.Enabled = True
    End Function
    Public Sub GetCompanyDetails()
        con = New SqlConnection(cs)
        con.Open()
        cmd = con.CreateCommand()
        cmd = New SqlCommand("select CompanyName,VatNo,Address,ContactNo,RTRIM(CurrencyCode),TIN from Company", con)
        rdr = cmd.ExecuteReader()
        If rdr.Read() Then
            CusCompanyName = rdr(0).ToString
            CompanyBPN = rdr(0).ToString
            VatNo = rdr(1).ToString
            CompanyAddress = rdr(2).ToString
            CompanyPhoneNo = rdr(3).ToString
            Currency = rdr(4).ToString
            TIN = rdr(5).ToString
        End If
        con.Close()
    End Sub

    Public Sub GetInvoiceDetails(ByVal Trnxid As String, ByVal table As String)
        con = New SqlConnection(cs)
        con.Open()
        cmd = con.CreateCommand()
        If table = "Invoice" Then
            TrnxType = "0"
            cmd = New SqlCommand("select Currency,Subtotal,InvoiceNumber from Invoice Where TxnId='" & Trnxid & "'", con)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                Currency = rdr(0).ToString
                InvoiceAmount = rdr(1).ToString
                InvoiceNo = rdr(2).ToString
            End If
        Else
            TrnxType = "1"
            cmd = New SqlCommand("select Currency,TotalAmount,CreditNoteNumber, TxnId from CreditMemo Where CreditNoteNumber='" & Trnxid & "'", con)
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                Currency = rdr(0).ToString
                InvoiceAmount = rdr(1).ToString
                InvoiceNo = rdr(2).ToString
                OriginalInvoiceNo = rdr(3).ToString
            End If
        End If

        con.Close()
    End Sub
    Public Sub GetCreditNoteDetails(ByVal Trnxid As String)
        con = New SqlConnection(cs)
        con.Open()
        cmd = con.CreateCommand()
        cmd = New SqlCommand("select ReceiptNo from Invoice Where TxnId='" & Trnxid & "'", con)
        rdr = cmd.ExecuteReader()
        If rdr.Read() Then
            InvoiceRecieptNo = rdr(0).ToString
        End If
        con.Close()
    End Sub
    Public Sub GetCustomerDetails(ByVal cusID As String)
        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = con.CreateCommand()
            cmd = New SqlCommand("SELECT RTRIM(sClientName),RTRIM(sClientGrpName),RTRIM(sCustomerVat), RTRIM(sAddress),RTRIM(sPhone), RTRIM(sClientCode),RTRIM(sEmail), RTRIM(sProv), RTRIM(sDistrictName), RTRIM(sRepCode), RTRIM(sCity) from Customer where sClientName='" & cusID & "'", con)
            rdr = cmd.ExecuteReader()
            'string BranchName, string InvoiceNumber, string CustomerName, string TradeName, string CustomerVATNumber, string CustomerAddress, string CustomerTelephoneNumber, string CustomerTIN
            'MsgBox(CustomerID)
            If rdr.Read() Then
                CustomerName = rdr(0).ToString
                TradeName = rdr(1).ToString
                CustomerVATNumber = rdr(2).ToString
                CustomerAddress = rdr(3).ToString
                CustomerTelephoneNumber = rdr(4).ToString
                CustomerTIN = rdr(5).ToString
                CustomerEmail = rdr(6).ToString
                CustomerProvince = rdr(7).ToString
                CustomerStreet = rdr(8).ToString
                CustomerHouseNo = rdr(9).ToString
                CustomerCity = rdr(10).ToString
                AddCustomer2Zimra = "1"
                If CustomerTIN.Length < 10 Then AddCustomer2Zimra = "0"
                If CustomerVATNumber.Length < 9 Then AddCustomer2Zimra = "0"

            Else
                AddCustomer2Zimra = "0"
                CustomerName = ""
                TradeName = ""
                CustomerVATNumber = ""
                CustomerAddress = ""
                CustomerTelephoneNumber = ""
                CustomerTIN = ""
                CustomerEmail = ""
                CustomerProvince = ""
                CustomerStreet = ""
                CustomerHouseNo = ""
                CustomerCity = ""
            End If
            con.Close()
        Catch ex As Exception

        End Try

    End Sub
    Sub UpdateQRCode(ByVal code_data As String, ByVal ID As String, ByVal table As String)
        'Console.WriteLine(table & " ==== " & ID)
        GetQRCode(code_data)
        Dim con2 As SqlConnection = New SqlConnection(cs)
        con2.Open()
        Dim queryb As String = ""
        If table = "Invoice" Then
            queryb = "UPDATE Invoice SET  QrCode=@d1,DeviceID=@d2,FiscalDay=@d3,ReceiptNo=@d4,Vcode=@d5,HavanoZimraStatus=@d6 WHERE TxnId='" & ID & "'"
            Dim command0 As New SqlCommand(queryb, con2)
            command0.Parameters.AddWithValue("@d2", DeviceID)
            command0.Parameters.AddWithValue("@d3", FiscalDay)
            command0.Parameters.AddWithValue("@d4", receiptGlobalNo)
            command0.Parameters.AddWithValue("@d5", VerificationCode)
            command0.Parameters.AddWithValue("@d6", True)

            Dim ms As New MemoryStream()
            Dim bmpImage As New Bitmap(PictureBox1.Image)
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New SqlParameter("@d1", SqlDbType.Image)
            p.Value = data
            command0.Parameters.Add(p)

            command0.ExecuteNonQuery()
        Else
            queryb = "UPDATE CreditMemo SET  QrCode=@d1,DeviceID=@d2,FiscalDay=@d3,Vcode=@d4,HavanoZimraStatus=@d5 WHERE CreditNoteNumber='" & ID & "'"
            Dim command0 As New SqlCommand(queryb, con2)
            command0.Parameters.AddWithValue("@d2", DeviceID)
            command0.Parameters.AddWithValue("@d3", FiscalDay)
            command0.Parameters.AddWithValue("@d4", VerificationCode)
            command0.Parameters.AddWithValue("@d5", True)

            Dim ms As New MemoryStream()
            Dim bmpImage As New Bitmap(PictureBox1.Image)
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New SqlParameter("@d1", SqlDbType.Image)
            p.Value = data
            command0.Parameters.Add(p)

            command0.ExecuteNonQuery()
        End If

        con2.Close()

    End Sub

    Public Sub GetQRCode(ByVal cusqrdata As String)
        Dim enc As QRCodeEncoder = New QRCodeEncoder()
        enc.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        enc.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
        enc.QRCodeVersion = 0
        PictureBox1.Image = enc.Encode(cusqrdata)
    End Sub

    Public Sub StartSendingInvoice()
        If CheckInvoice() Then
            mytable = "Invoice"
            For Each id In TxnID_Lst
                Me.BeginInvoke(Sub()
                                   lblnotify.Text = "Invoice Found, Processing Invoice " & id
                               End Sub)
                Console.WriteLine(id)
                GetItemXML(id)
                SendTax()
                tmr_start.Enabled = True
                Exit Sub
            Next
        End If


        If CheckCreditNote() Then
            mytable = "CreditMemo"
            For Each id In TxnID_Lst
                Me.BeginInvoke(Sub()
                                   lblnotify.Text = "CreditNote Found, Processing Invoice " & id
                               End Sub)
                Console.WriteLine(id)
                GetItemXML(id)
                SendTax()
                tmr_start.Enabled = True
                Exit Sub
            Next

        End If

    End Sub
End Class
