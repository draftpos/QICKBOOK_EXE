Imports System.Data.SqlClient
Imports System.IO

Module ConnectionString
    Public con As SqlConnection = Nothing
    Public cmd0, cmd, cmd1, cmd2, cmd3, cmd4 As SqlCommand
    Public rdr As SqlDataReader = Nothing
    Public rdr2 As SqlDataReader = Nothing
    Dim st As String
    Public Function ReadCS() As String
        Using sr As StreamReader = New StreamReader(Application.StartupPath & "\havanquickbook_sql_settings.ini")
            st = sr.ReadLine()
        End Using
        Return st
    End Function
    Public Function ReadServer() As String
        Using sr As StreamReader = New StreamReader(Application.StartupPath & "\havanquickbook_sql_settings.ini")
            st = sr.ReadLine()
        End Using
        Dim serverstr As String = st.Split(";")(0)
        Return serverstr
    End Function
    Public ReadOnly cs As String = ReadCS()
    Public ReadOnly serverstr As String = ReadServer()
End Module
