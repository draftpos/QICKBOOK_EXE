Imports System.Data.SqlClient

Public Class Company
    Public Property CompanyName As String
    Public Property MailingName As String
    Public Property Country As String
    Public Property Address As String
    Public Property City As String
    Public Property State As String
    Public Property PinCode As String
    Public Property ContactNo As String
    Public Property Fax As String
    Public Property Email As String
    Public Property Website As String
    Public Property TIN As String
    Public Property LicenseNo As String
    Public Property ServiceTaxNo As String
    Public Property CST As String
    Public Property PAN As String
    Public Property CurrencyCode As String
    Public Property Currency As String
    Public Property Logo As Byte()
    Public Property ShowLogo As Boolean
    Public Property CapitalAccount As String
    Public Property NP As String
    Public Property QCode As String
    Public Property BCode As String
    Public Property InvoiceHeader As String
    Public Property ItemWiseVAT As Boolean
    Public Property QTC As String
    Public Property ZeroPrice As Boolean
    Public Property BelowCost As Boolean
    Public Property ActiveBelow As Boolean
    Public Property wscalable As Boolean
    Public Property pscalable As Boolean
    Public Property MultiCurrencyReceipt As Boolean
    Public Property ShowMultiCurrency As Boolean
    Public Property VatNo As String
    Public Property RevMaxKey As String
    Public Property ShowDiscount As Boolean
    Public Property EnableRevMax As Boolean
    Public Property selnegative As Boolean
    Public Property patchinter As Boolean
    Public Property cash As Boolean
    Public Property autoprint As Boolean
    Public Property autoprintshift As Boolean
    Public Property textprinting As Boolean
    Public Property vat_display As Boolean
    Public Property resturantui As Boolean





End Class

Public Class ApiResponseData
    Public Property status As String
    Public Property message As String
    Public Property data As ApiResponseDetails
End Class

Public Class ApiResponseDetails
    Public Property email As String
    Public Property store_id As String
    Public Property id As String
    Public Property mobile As String
    Public Property username As String
    Public Property role_id As String
    Public Property role_name As String
    Public Property status As String
    Public Property last_name As String
    Public Property default_warehouse_id As String
    Public Property warehouse_id As String
    Public Property store_name As String
    Public Property store_logo As String
    Public Property store_address As String
    Public Property city As String
    Public Property country As String
    Public Property timezone As String
    Public Property currency As String
    Public Property dated As String
    Public Property warehouses As List(Of WarehouseDetails)
End Class

Public Class WarehouseDetails
    Public Property warehouse_name As String
    Public Property warehouse_type As String
    Public Property id As String
End Class

Public Class Customer
    Public Property CC_ID As Integer
    Public Property CustomerID As String
    Public Property Name As String
    Public Property ContactNo As String
    Public Property Address As String
    Public Property TRN As String
    Public Property RegistrationDate As DateTime?
    Public Property Active As String
    Public Property RateType As String
    Public Property Discount As String
    Public Property TradeName As String
    Public Property Street As String
    Public Property Email As String
    Public Property VatNumber As String
    Public Property Province As String
    Public Property HouseNo As String
    Public Property City As String
    Public Property Cuspin As String
End Class
