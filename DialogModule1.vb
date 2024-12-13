Module DialogModule1


    Public Function exitmsg()
        msgtitle = "Close Form"
        msgcontent = "Exit Form??"
        msgdialog.ShowDialog()
        Return msgresponse
    End Function
    Public Function exitapp() As Boolean
        msgtitle = "Close Application"
        msgcontent = "Exit App??"
        msgdialog.ShowDialog()
        Return msgresponse
    End Function
End Module
