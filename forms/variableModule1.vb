Imports System.IO

Module variableModule1
    Public filePathfirst_time As String = Path.Combine("havanquickbook_first_time_Reindexing.dll")
    Public filePathreadcredential As String = Path.Combine(Application.StartupPath, "onlined.dll")
    Public filePathreadfiscal As String = Path.Combine(Application.StartupPath, "Havanoconfig.ini")
    Public filePathsqlsetting As String = Path.Combine(Application.StartupPath, "havanquickbook_sql_settings.ini")
    Public filePathanimation As String = Path.Combine(Application.StartupPath, "havanquickbooksanimation.ini")
    Public sql, mysql, query As String
    Public autorization_migration As Integer = 0
End Module
