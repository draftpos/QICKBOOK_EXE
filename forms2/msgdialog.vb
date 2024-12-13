Public Class msgdialog


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click, GelButton1.Click
        msgresponse = False
        btntext = Nothing
        Me.Dispose()
    End Sub

    Private Sub GelButton2_Click(sender As Object, e As EventArgs) Handles GelButton2.Click
        msgresponse = True
        btntext = Nothing
        Me.Dispose()
    End Sub

    Private Sub msgdialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If msgicon Is Nothing Or msgicon = "info" Then
            msgicon = "Information_icon"
        ElseIf msgicon = "ex" Then
            msgicon = "Exclamation_icon"
        ElseIf msgicon = "er" Then
            msgicon = "Error_icon_dialog"
        ElseIf msgicon = "de" Then
            msgicon = "Delete_iconDialog"
        ElseIf msgicon = "up" Then
            msgicon = "updateiconDialog"
        Else
            msgicon = "Information_icon"
        End If
        If msgcontent.Length > 0 Then
            lblmsgcontent.Text = ToTitleCase(msgcontent.ToLower())
        End If
        If msgtitle.Length > 0 Then
            lblmsgtitle.Text = ToTitleCase(msgtitle.ToLower())
        End If
        Try
            If btntext Is Nothing OrElse btntext.Length < 2 Then
                GelButton2.Text = "Ok"
                GelButton1.Text = "Cancel"
            Else
                GelButton1.Text = btntext(0).ToString()
                GelButton2.Text = btntext(1).ToString()
            End If
        Catch ex As Exception
        End Try
        Dim imageResource As Image = CType(My.Resources.ResourceManager.GetObject(msgicon), Image)
        Me.PictureBox1.Image = imageResource
        btntext = Nothing
    End Sub

    Private Sub lblmsgcontent_Click(sender As Object, e As EventArgs) Handles lblmsgcontent.Click

    End Sub
End Class

