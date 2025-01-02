Module Module3

    ' Declare public variables with default values
    Public VatFlag As Integer = 0
    Public Tax5 As Decimal = 0
    Public Tax15 As Decimal = 0
    Public Tax0 As Decimal = 0
    Public TaxE As Decimal = 0
    Public TIN As String = Nothing
    Public VAT As String = Nothing
    Public activationKey As String = Nothing
    Public deviceSerialNo As String = Nothing
    Public DeviceID As Integer = 0
    Public DeviceModelName As String = Nothing
    Public DeviceModelVersion As String = Nothing
    Public ZimraServer As String = Nothing
    Public VerificationServer As String = Nothing
    Public ReceiptCounter As Integer = 0
    Public ReceiptGlobalNo As Integer = 0
    Public ReceiptPrintForm As String = Nothing
    Public FiscalDayNo As Integer = 0
    Public FiscalDate As String = Nothing
    Public PreviousReceiptHash As String = Nothing
    Public FiscalDayStatus As String = Nothing

    Public Sub ReadConfig()
        Dim configFilePath As String = System.IO.Path.Combine(Application.StartupPath, "havanoconfig.ini")
        If System.IO.File.Exists(configFilePath) Then
            Dim lines() As String = System.IO.File.ReadAllLines(configFilePath)
            For Each line As String In lines
                If line.Contains(":") Then
                    Dim parts() As String = line.Split(":"c)
                    Dim key As String = parts(0).Trim()
                    Dim value As String = parts(1).Trim()
                    Select Case key
                        Case "VatFlag"
                            VatFlag = Integer.Parse(value)
                        Case "Tax5"
                            Tax5 = Decimal.Parse(value)
                        Case "Tax15"
                            Tax15 = Decimal.Parse(value)
                        Case "Tax0"
                            Tax0 = Decimal.Parse(value)
                        Case "TaxE"
                            TaxE = Decimal.Parse(value)
                        Case "TIN"
                            TIN = value
                        Case "VAT"
                            VAT = value
                        Case "activationKey"
                            activationKey = value
                        Case "deviceSerialNo"
                            deviceSerialNo = value
                        Case "DeviceID"
                            DeviceID = Integer.Parse(value)
                        Case "DeviceModelName"
                            DeviceModelName = value
                        Case "DeviceModelVersion"
                            DeviceModelVersion = value
                        Case "ZimraServer"
                            ZimraServer = value
                        Case "VerificationServer"
                            VerificationServer = value
                        Case "ReceiptCounter"
                            ReceiptCounter = Integer.Parse(value)
                        Case "ReceiptGlobalNo"
                            ReceiptGlobalNo = Integer.Parse(value)
                        Case "ReceiptPrintForm"
                            ReceiptPrintForm = value
                        Case "FiscalDayNo"
                            FiscalDayNo = Integer.Parse(value)
                        Case "FiscalDate"
                            FiscalDate = (value)
                        Case "PreviousReceiptHash"
                            PreviousReceiptHash = value
                        Case "FiscalDayStatus"
                            FiscalDayStatus = value
                    End Select
                End If
            Next
        Else
            Debug.WriteLine("Configuration file not found at: " & configFilePath)
        End If
    End Sub


End Module
