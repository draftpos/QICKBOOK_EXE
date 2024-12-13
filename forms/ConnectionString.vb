Imports System.IO:  Imports WinFormsApp1.STANNIC_POS.Reports
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Module ConnectionString
    Dim st As String
    Public Function ReadCS() As String
        Using sr As StreamReader = New StreamReader(filePathsqlsetting)
            st = sr.ReadLine()
        End Using
        Return st
    End Function
    Public Function ReadServer() As String
        Using sr As StreamReader = New StreamReader(filePathsqlsetting)
            st = sr.ReadLine()
        End Using
        Dim serverstr As String = st.Split(";")(0)
        Return serverstr
    End Function
    Public ReadOnly cs As String = ReadCS()
    Public ReadOnly serverstr As String = ReadServer()

End Module
