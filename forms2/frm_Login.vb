Public Class frm_Login


    Private Sub frm_Login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txt_username.Text.Trim.Length = 0 Then
            msgtitle = "Blank UserName"
            msgcontent = "Invalid UserName"
            msgdialog.ShowDialog()
            Return
        End If
        If txt_password.Text.Trim.Length = 0 Then
            msgtitle = "Blank Password"
            msgcontent = "Invalid Password"
            msgdialog.ShowDialog()
            Return
        End If
        If txt_username.Text.Trim() <> "Admin" Or txt_password.Text.Trim <> "12345" Then
            msgtitle = "Login Failed"
            msgcontent = "Invalid UserName/Password"
            msgdialog.ShowDialog()
            Return
        End If
        Await companytables_and_data()
        Form1.Show()
        Me.Dispose()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmSqlServerSetting.Dispose()
        frmSqlServerSetting.Show()
        Me.Dispose()
    End Sub

    Private Sub frm_Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class