﻿Imports System.Data.SqlClient
Imports System.IO

Module Glob_var
    Public da As SqlDataAdapter = Nothing
    Public dt As New DataTable()
    Public con As SqlConnection = Nothing
    Public cmd0, cmd, cmd1, cmd2, cmd3, cmd4 As SqlCommand
    Public rdr As SqlDataReader = Nothing
    Public rdr2 As SqlDataReader = Nothing
    Public ds As DataSet
    Public adp, adp1, adp2, adp3, adp4 As SqlDataAdapter
    Public dtable, dtable1, dtable2, dtable3, dtable4 As DataTable
    Public TempFileNames2, TempFileNames3 As String
    Public rdr1 As SqlDataReader = Nothing
    Function getOrgName() As Company
        Try
            Using con As New SqlConnection(cs)
                con.Open()
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandText = "SELECT * FROM Company"
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim showlog As Boolean
                            If LCase(reader("ShowLogo").ToString().Trim()) = "yes" Then
                                showlog = True
                            Else
                                showlog = False
                            End If
                            Dim company As New Company() With {
                                .CompanyName = If(IsDBNull(reader("CompanyName")), "", reader("CompanyName").ToString()),
                                .MailingName = If(IsDBNull(reader("MailingName")), "", reader("MailingName").ToString()),
                                .Country = If(IsDBNull(reader("Country")), "", reader("Country").ToString()),
                                .Address = If(IsDBNull(reader("Address")), "", reader("Address").ToString()),
                                .City = If(IsDBNull(reader("City")), "", reader("City").ToString()),
                                .State = If(IsDBNull(reader("State")), "", reader("State").ToString()),
                                .PinCode = If(IsDBNull(reader("PinCode")), "", reader("PinCode").ToString()),
                                .ContactNo = If(IsDBNull(reader("ContactNo")), "", reader("ContactNo").ToString()),
                                .Fax = If(IsDBNull(reader("Fax")), "", reader("Fax").ToString()),
                                .Email = If(IsDBNull(reader("Email")), "", reader("Email").ToString()),
                                .Website = If(IsDBNull(reader("Website")), "", reader("Website").ToString()),
                                .TIN = If(IsDBNull(reader("TIN")), "", reader("TIN").ToString()),
                                .LicenseNo = If(IsDBNull(reader("LicenseNo")), "", reader("LicenseNo").ToString()),
                                .ServiceTaxNo = If(IsDBNull(reader("ServiceTaxNo")), "", reader("ServiceTaxNo").ToString()),
                                .CST = If(IsDBNull(reader("CST")), "", reader("CST").ToString()),
                                .PAN = If(IsDBNull(reader("PAN")), "", reader("PAN").ToString()),
                                .CurrencyCode = If(IsDBNull(reader("CurrencyCode")), "", reader("CurrencyCode").ToString()),
                                .Currency = If(IsDBNull(reader("Currency")), "", reader("Currency").ToString()),
                                .Logo = If(IsDBNull(reader("Logo")), Nothing, DirectCast(reader("Logo"), Byte())),
                                .ShowLogo = showlog, ' SafeConvertToBoolean(reader("ShowLogo")),
                                .CapitalAccount = If(IsDBNull(reader("CapitalAccount")), "", reader("CapitalAccount").ToString()),
                                .NP = If(IsDBNull(reader("NP")), "", reader("NP").ToString()),
                                .QCode = If(IsDBNull(reader("QCode")), "", reader("QCode").ToString()),
                                .BCode = If(IsDBNull(reader("BCode")), "", reader("BCode").ToString()),
                                .InvoiceHeader = If(IsDBNull(reader("InvoiceHeader")), "", reader("InvoiceHeader").ToString()),
                                .ItemWiseVAT = SafeConvertToBoolean(reader("ItemWiseVAT")),
                                .QTC = If(IsDBNull(reader("QTC")), "", reader("QTC").ToString()),
                                .ZeroPrice = SafeConvertToBoolean(reader("ZeroPrice")),
                                .BelowCost = SafeConvertToBoolean(reader("BelowCost")),
                                .ActiveBelow = SafeConvertToBoolean(reader("ActiveBelow")),
                                .wscalable = SafeConvertToBoolean(reader("wscalable")),
                                .pscalable = SafeConvertToBoolean(reader("pscalable")),
                                .MultiCurrencyReceipt = SafeConvertToBoolean(reader("MultiCurrencyReceipt")),
                                .ShowMultiCurrency = SafeConvertToBoolean(reader("ShowMultiCurrency")),
                                .VatNo = If(IsDBNull(reader("VatNo")), "", reader("VatNo").ToString()),
                                .RevMaxKey = If(IsDBNull(reader("RevMaxKey")), "", reader("RevMaxKey").ToString()),
                                .ShowDiscount = SafeConvertToBoolean(reader("ShowDiscount")),
                                .EnableRevMax = SafeConvertToBoolean(reader("EnableRevMax")),
                                .selnegative = SafeConvertToBoolean(reader("selnegative")),
                                .patchinter = SafeConvertToBoolean(reader("patchinter")),
                                .autoprint = SafeConvertToBoolean(reader("autoprint")),
                                .autoprintshift = SafeConvertToBoolean(reader("autoprintshift")),
                                .textprinting = SafeConvertToBoolean(reader("textprinting")),
                                .vat_display = SafeConvertToBoolean(reader("vat_display")),
                                .resturantui = SafeConvertToBoolean(reader("resturantui")),
                                .cash = SafeConvertToBoolean(reader("cash"))
                            }
                            Return company
                        Else
                            Return Nothing
                        End If

                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle the exception as needed, possibly logging it
            Return Nothing
        End Try
    End Function

    Private Function SafeConvertToBoolean(value As Object) As Boolean
        If value Is Nothing OrElse IsDBNull(value) Then
            Return False
        End If

        Dim boolValue As Boolean
        If Boolean.TryParse(value.ToString(), boolValue) Then
            Return boolValue
        End If

        ' Additional checks for numeric representation of booleans
        If IsNumeric(value) Then
            Dim intValue As Integer
            If Integer.TryParse(value.ToString(), intValue) Then
                Return intValue <> 0
            End If
        End If

        ' Default to false if conversion fails
        Return False
    End Function

    Public Function getadvance_data() As Advance_settings
        Dim settings As New Advance_settings()

        ' Default values
        Dim defaultHeaderSize As Integer = 12
        Dim defaultHeaderStyle As String = "Bold"
        Dim defaultContentSize As Integer = 11
        Dim defaultSubheaderSize As Integer = 10
        Dim orderDefaultContentSize As Integer = 11
        Dim orderDefaultContentStyle As String = "Regular"

        ' Initialize settings with default values
        settings.ContentFontSize = defaultContentSize
        settings.ContentFontStyle = "Regular"
        settings.ContentHeaderSize = defaultHeaderSize
        settings.ContentHeaderStyle = defaultHeaderStyle
        settings.SubheaderSize = defaultSubheaderSize
        settings.SubheaderStyle = "Regular"
        settings.orderContentFontSize = orderDefaultContentSize
        settings.orderContentFontStyle = orderDefaultContentStyle

        Using con As New SqlConnection(cs)
            con.Open()
            Using cmd As SqlCommand = con.CreateCommand()
                cmd.CommandText = "SELECT dmarkup, phar, dispatch, CUSCHARSTAT, ContentFontSize, ContentFontStyle, ContentHeaderSize, ContentHeaderStyle, SubheaderSize, SubheaderStyle, orderContentFontSize, orderContentFontStyle, batches_additional_column FROM Advance_settings"
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        settings.DMarkup = If(IsDBNull(reader("dmarkup")), 0D, Convert.ToDecimal(reader("dmarkup")))
                        settings.Phar = If(IsDBNull(reader("phar")), False, Convert.ToBoolean(reader("phar")))
                        settings.Dispatch = If(IsDBNull(reader("dispatch")), False, Convert.ToBoolean(reader("dispatch")))
                        settings.CusCharStat = If(IsDBNull(reader("CUSCHARSTAT")), False, Convert.ToBoolean(reader("CUSCHARSTAT")))
                        settings.batches_additional_column = If(IsDBNull(reader("batches_additional_column")), False, Convert.ToBoolean(reader("batches_additional_column"))) ' 

                        settings.ContentFontSize = If(IsDBNull(reader("ContentFontSize")) OrElse Convert.ToInt32(reader("ContentFontSize")) <= 0, defaultContentSize, Convert.ToInt32(reader("ContentFontSize")))
                        settings.ContentFontStyle = If(IsDBNull(reader("ContentFontStyle")), "Regular", reader("ContentFontStyle").ToString())

                        settings.ContentHeaderSize = If(IsDBNull(reader("ContentHeaderSize")) OrElse Convert.ToInt32(reader("ContentHeaderSize")) <= 0, defaultHeaderSize, Convert.ToInt32(reader("ContentHeaderSize")))
                        settings.ContentHeaderStyle = If(IsDBNull(reader("ContentHeaderStyle")), defaultHeaderStyle, reader("ContentHeaderStyle").ToString())

                        settings.SubheaderSize = If(IsDBNull(reader("SubheaderSize")) OrElse Convert.ToInt32(reader("SubheaderSize")) <= 0, defaultSubheaderSize, Convert.ToInt32(reader("SubheaderSize")))
                        settings.SubheaderStyle = If(IsDBNull(reader("SubheaderStyle")), "Regular", reader("SubheaderStyle").ToString())

                        settings.orderContentFontSize = If(IsDBNull(reader("orderContentFontSize")) OrElse Convert.ToInt32(reader("orderContentFontSize")) <= 0, orderDefaultContentSize, Convert.ToInt32(reader("orderContentFontSize")))
                        settings.orderContentFontStyle = If(IsDBNull(reader("orderContentFontStyle")), orderDefaultContentStyle, reader("orderContentFontStyle").ToString())
                    End If
                End Using
            End Using
        End Using

        Return settings
    End Function



    Public Sub HideFilesInAppPathExceptExe()
        Try
            Dim appPath As String = Application.StartupPath
            Dim files As String() = Directory.GetFiles(appPath)

            For Each filed As String In files

                Dim fileName As String = Path.GetFileName(filed)
                If Not fileName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) Then
                    File.SetAttributes(filed, FileAttributes.Hidden)
                End If
            Next
            MsgBox("All files except the EXE file are now hidden.", MsgBoxStyle.Information, "Success")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Operation Failed")
        End Try
    End Sub


End Module

Public Class Advance_settings
    Public Property DMarkup As Decimal
    Public Property Phar As Boolean
    Public Property Dispatch As Boolean
    Public Property CusCharStat As Boolean
    Public Property batches_additional_column As Boolean
    Public Property ContentFontSize As Integer
    Public Property ContentFontStyle As String
    Public Property ContentHeaderSize As Integer
    Public Property ContentHeaderStyle As String
    Public Property SubheaderSize As Integer
    Public Property SubheaderStyle As String
    Public Property orderContentFontSize As Integer
    Public Property orderContentFontStyle As String
End Class
