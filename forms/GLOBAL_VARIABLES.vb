Imports System.Data.SqlClient
Imports System.IO

Module GLOBAL_VARIABLES
    Public process_online_invoice As Boolean = True
    Public HOLDTAB_ID As Integer
    Public usertypeonpc As Boolean
    Public first_time_runner As Boolean = True
    Public useronpc As String
    Public parameters As New List(Of SqlParameter)()
    Public waiters_status As Boolean = False
    Public tables_set As Boolean = False
    Public msgtitle As String = "", msgcontent As String = "", msgicon As String = ""
    Public msgresponse As Boolean = False
    Public tables_status_differ As Boolean = False
    Public companyInfo As Company = getOrgName()
    ' Public advancedata As Advance_settings = getadvance_data()
    '   Public advancedata As Advance_settings = getadvance_data()
    Public apiSecret As String '= "6110aa808c9ddd3"
    Public apikey As String ' = "4c88387ea9131dd"
    Public baseUrl As String '= baseurl
    Public erpstatus As Boolean = False
    Public dialogbtn_type As Integer = 2
    Public btntext() As String
    Public msg_datatype As String = "Default"
    Public dialog_input_response_data As String = ""
    Public warehouseid_api, storeid_api As Integer
    Public apiResponsed As ApiResponseData
    Public barcode_productcode_Generation As Tuple(Of String, String, Integer)
    Public globaltodate, globalfromdate As Date
    '    Public fiscaldata = ReadHavanoConfig()
    ' Public customer_invoice As Customer = GetCustomerByInvoiceNo(Nothing)

End Module
