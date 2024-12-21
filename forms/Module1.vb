Imports System.Data.SqlClient

Module Module1
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
    Public saveFileDialog As New SaveFileDialog()
End Module
