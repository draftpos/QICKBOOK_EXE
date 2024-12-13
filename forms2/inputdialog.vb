Imports System.Data.SqlClient
Imports System.Windows.Input

Public Class inputdialog
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles GelButton1.Click
        msgresponse = False
        dialog_input_response_data = ""
        Me.Dispose()
    End Sub

    Private Sub inputdialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DateTimePicker1.Value = Now.Date
        DateTimePicker2.Value = Now.Date
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
        lblmsgtitle.Text = msgtitle
        TextBox1.Text = msgcontent

        'Try
        If btntext Is Nothing OrElse btntext.Length < 2 Then
            GelButton2.Text = "Ok"
            GelButton1.Text = "Cancel"
        Else
            GelButton1.Text = btntext(0).ToString()
            GelButton2.Text = btntext(1).ToString()
        End If
        'Catch ex As Exception
        'End Try
        btntext = Nothing
        TextBox1.TabIndex = 0
        TextBox1.Focus()
    End Sub

    Private Sub GelButton2_Click(sender As Object, e As EventArgs) Handles GelButton2.Click
        If TextBox1.Text.Length = 0 And pnp1.Visible = False And pnldate.Visible = False Then
            msgtitle = "Invalid data supplied"
            msgcontent = "No Valid Input Supplied"
            msgicon = "er"
            msgdialog.ShowDialog()
            Return
        End If
        If pnp1.Visible And ComboBox1.SelectedIndex = -1 Then
            msgtitle = "Invalid currency selected"
            msgcontent = "No Valid Input Supplied"
            msgicon = "er"
            msgdialog.ShowDialog()
            Return
        End If
        If pnp1.Visible Then
            If ComboBox1.SelectedItem IsNot Nothing Then
                Dim selectedCurrency = CType(ComboBox1.SelectedItem, Object)
                Dim currencyCode As String = selectedCurrency.Id
                Dim currencyName As String = selectedCurrency.CurrencyName
                dialog_input_response_data = currencyCode.Trim
            Else
                msgtitle = "Invalid currency selected"
                msgcontent = "No Valid Input Supplied"
                msgicon = "er"
                msgdialog.ShowDialog()
                Return
            End If

        Else
            dialog_input_response_data = TextBox1.Text.Trim()
            globalfromdate = CDate(DateTimePicker1.Value)
            globaltodate = CDate(DateTimePicker2.Value)
        End If
        msgresponse = True
        btntext = Nothing
        Me.Dispose()
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            GelButton2_Click(sender, e)
            e.Handled = True
        End If
    End Sub

End Class