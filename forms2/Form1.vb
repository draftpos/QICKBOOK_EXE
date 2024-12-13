Imports System.Data.SqlClient
Imports System.IO


Imports Windows.Win32.System
Public Class Form1
    Dim SqlConnStr As String
    Private Sub cmbInstallationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbInstallationType.SelectedIndexChanged
        If cmbInstallationType.SelectedIndex = 1 Then
            cmbAuthentication.SelectedIndex = 1
            cmbAuthentication.Enabled = False
            btnDemoDB.Text = "Save Client Setting"
        ElseIf cmbInstallationType.SelectedIndex = 0 Then
            cmbAuthentication.SelectedIndex = 0
            cmbAuthentication.Enabled = True
            btnDemoDB.Text = "Create DB and Proceed"
        End If
    End Sub
    Private Sub ValidateServerDetails()
        If String.IsNullOrEmpty(cmbServerName.Text) Then
            MsgBox("Please select/enter Server Name", MsgBoxStyle.Information)
            cmbServerName.Focus()
            Exit Sub
        End If
        If cmbAuthentication.SelectedIndex = 1 Then
            If String.IsNullOrEmpty(txtUserName.Text) Then
                MsgBox("Please enter user name", MsgBoxStyle.Information)
                txtUserName.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtPassword.Text) Then
                MsgBox("Please enter password", MsgBoxStyle.Information)
                txtPassword.Focus()
                Exit Sub
            End If
        End If
    End Sub
    Private Async Sub load_form()
        Reset()

        If File.Exists(filePathsqlsetting) Then
            Try
                sql = "SELECT 1"
                dt = Crud(sql, Nothing)
                If dt.Rows.Count = 0 Then
                    msgtitle = ("Connection isn't Valid.")
                    msgcontent = $"Invalid Connection String {vbCrLf} Click Yes to Proceed and No to Connect the DB..."
                    msgdialog.ShowDialog()
                    If msgresponse Then
                        pnlsqlsettings.Visible = True
                        pnl_login.Visible = False
                        ServerEnumerationAsync().Wait()
                        Return
                    End If
                End If
            Catch ex As Exception
                msgcontent = ($"Failed to check connection: {vbCrLf}  {ex.Message.ToString()}  {vbCrLf} Click Yes to Proceed and No to Connect the DB...")
                msgcontent = "Invalid Connection String"
                msgdialog.ShowDialog()
                If msgresponse Then
                    pnlsqlsettings.Visible = True
                    pnl_login.Visible = False
                    ServerEnumerationAsync().Wait()
                    Return
                Else
                    End
                End If
                ' Return
            End Try
        Else
            pnlsqlsettings.Visible = True
            pnl_login.Visible = False
            Await ServerEnumerationAsync()
            Return
        End If
        pnlsqlsettings.Visible = False
        pnl_login.Visible = True
    End Sub
    Public Sub Reset()
        txtPassword.Text = ""
        txtUserName.Text = ""
        cmbServerName.Text = ""
        cmbAuthentication.SelectedIndex = 0
        cmbInstallationType.SelectedIndex = 0
    End Sub

    Async Function ServerEnumerationAsync() As Task
        Await Task.Run(Sub() EnsureSqlBrowserServiceEnabled())
        Dim fallbackNeeded As Boolean = False
        Try
            Cursor = Cursors.WaitCursor
            Timer5.Enabled = True
            dt = New DataTable
            ' Try
            '                dt = Await Task.Run(Function() Microsoft.SqlServer.Management.Smo.SmoApplication.EnumAvailableSqlServers(True))
            '   dt = Await Task.Run(Function() Microsoft.SqlServer.Management.Smo.SmoApplication.EnumAvailableSqlServers(True)).ConfigureAwait(False)

            'Catch ex As Exception
            fallbackNeeded = True
            '   End Try
            ' If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'cmbServerName.ValueMember = "Name"
            'cmbServerName.DataSource = dt
            'Else
            'fallbackNeeded = True ' No servers found, initiate fallback
            'End If
        Catch ex As Exception
            fallbackNeeded = True ' Exception occurred, initiate fallback
        Finally
            Cursor = Cursors.Default
        End Try
        If fallbackNeeded Then
            Await FallbackToManualServerListAsync()
        End If
    End Function


    Private Async Function FallbackToManualServerListAsync() As Task
        Try
            Dim serverList As DataTable = Await Task.Run(Function() Microsoft.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources())
            Dim servers As New List(Of String)
            For Each row As DataRow In serverList.Rows
                Dim serverName = row("ServerName").ToString()
                Dim instanceName = row("InstanceName").ToString()
                If String.IsNullOrEmpty(instanceName) Then
                    servers.Add(serverName)
                Else
                    servers.Add($"{serverName}\{instanceName}")
                End If
            Next
            cmbServerName.DataSource = servers
        Catch ex As Exception
            msgcontent = $"Unable to find SQL Server instance.{vbCrLf}" &
                             $"Please enter the name of the SQL Server instance manually.{vbCrLf}{ex}"
            msgtitle = "Sql Server Failed On Both Manual and SMO Libary [Info] "
            msgdialog.ShowDialog()
        End Try
    End Function

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        Cursor = Cursors.Default
        Timer5.Enabled = False
    End Sub

    Private Function ConfirmDatabaseCreation() As Boolean
        Return MsgBox("It will create the DB and configure the SQL Server. Do you want to proceed?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes
    End Function

    Private Function ConfirmSqlServerConfiguration() As Boolean
        Return MsgBox("It will configure the SQL Server. Do you want to proceed?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes
    End Function

    Private Function ConfirmCloseApplication() As Boolean
        Return MsgBox("Do you want to close the application?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes
    End Function
    Private Sub SaveSqlSettings()
        Using sw As New StreamWriter(filePathsqlsetting)
            If cmbAuthentication.SelectedIndex = 0 Then
                sw.WriteLine("Data Source=" & cmbServerName.Text.Trim & ";Initial Catalog=FetchInv;Integrated Security=True;MultipleActiveResultSets=True")
            ElseIf cmbAuthentication.SelectedIndex = 1 Then
                sw.WriteLine("Data Source=" & cmbServerName.Text.Trim & ";Initial Catalog=FetchInv;User ID=" & txtUserName.Text.Trim & ";Password=" & txtPassword.Text & ";MultipleActiveResultSets=True")
            End If
        End Using
    End Sub
    Private Sub ConfigureSqlConnection()
        If cmbAuthentication.SelectedIndex = 0 Then
            SqlConnStr = "Data Source=" & cmbServerName.Text.Trim & ";Initial Catalog=FetchInv;Integrated Security=True;MultipleActiveResultSets=True"
        ElseIf cmbAuthentication.SelectedIndex = 1 Then
            SqlConnStr = "Data Source=" & cmbServerName.Text.Trim & ";Initial Catalog=FetchInv;User ID=" & txtUserName.Text.Trim & ";Password=" & txtPassword.Text & ";MultipleActiveResultSets=True"
        End If
    End Sub
    Private Function check_connectionstate() As Boolean
        ValidateServerDetails()
        Dim SqlConn As New SqlConnection
        ConfigureSqlConnection()

        If SqlConn.State = ConnectionState.Closed Then
            SqlConn.ConnectionString = SqlConnStr
            Try
                SqlConn.Open()
                ' ShowSuccessMessage("Successful DB Connection")
                Return True
            Catch ex As Exception
                ShowErrorMessage("Invalid DB Connection: " & ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function

    Private Sub ShowErrorMessage(message As String)
        MessageBox.Show(message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        If Not check_connectionstate() Then
            ShowErrorMessage("Invalid DB Connection:")
            Return
        End If
        '  If File.Exists(filePathreadcredential) Then
        ' File.Delete(filePathreadcredential)
        ' End If
        ' If File.Exists(filePathfirst_time) Then
        ' File.Delete(filePathfirst_time)
        ' End If
        '  MsgBox("Set")
        Using sw As StreamWriter = New StreamWriter(filePathsqlsetting)
            If cmbAuthentication.SelectedIndex = 0 Then
                sw.WriteLine("Data Source=" & cmbServerName.Text.Trim & ";Initial Catalog=FetchInv;Integrated Security=True;MultipleActiveResultSets=True")
                sw.Close()
            End If
            If cmbAuthentication.SelectedIndex = 1 Then
                sw.WriteLine("Data Source=" & cmbServerName.Text.Trim & ";Initial Catalog=FetchInv;User ID=" & txtUserName.Text.Trim & ";Password=" & txtPassword.Text & ";MultipleActiveResultSets=True")
                sw.Close()
            End If
        End Using
        msgcontent = "SQL Server setting has been connected" & vbCrLf & "Application will be closed,Please start it again"
        msgtitle = "Information"
        msgdialog.ShowDialog()
        ' End
        Application.Exit()
    End Sub



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnDemoDB.Click
        '        If Not check_connectionstate() Then
        '      ShowErrorMessage("Invalid DB Connection:")
        '    Return
        '   End If

        If File.Exists(filePathreadcredential) Then
            File.Delete(filePathreadcredential)
        End If

        If File.Exists(filePathfirst_time) Then
            File.Delete(filePathfirst_time)
        End If
        If cmbInstallationType.SelectedIndex = 0 Then
            ValidateServerDetails()
            ConfigureSqlConnection()
            If ConfirmDatabaseCreation() Then
                SaveSqlSettings()
                ' If Not CheckBox1.Checked Then
                Try
                    CreateDB()
                Catch ex As Exception
                    Try
                        '  MsgBox("ff")
                        CreateDBnew()
                    Catch ex1 As Exception
                    End Try
                End Try
                ShowSuccessMessage("DB has been created and SQL Server setting has been saved successfully. Application will be closed. Please start it again.")
                Application.Exit()
            End If
        ElseIf cmbInstallationType.SelectedIndex = 1 Then
            ValidateServerDetails()
            ConfigureSqlConnection()
            If ConfirmSqlServerConfiguration() Then
                SaveSqlSettings()
                ShowSuccessMessage("SQL Server setting has been saved successfully. Application will be closed. Please start it again.")
                Application.Exit()
            End If
        End If
    End Sub

    Private Sub CreateDB()
        'Try
        Using con As New SqlConnection("Data Source=" & cmbServerName.Text & ";Initial Catalog=master;Integrated Security=True;MultipleActiveResultSets=True")
            con.Open()
            DropExistingDatabase(con, "FetchInv")
            CreateNewDatabase(con, "FetchInv")
            ExecuteDatabaseScript(con, Application.StartupPath & "FetchInv.sql")
        End Using
        '  Catch ex As Exception
        '     ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Sub CreateDBnew()
        Dim backupFolderPath As String = "C:\db_backupquickbook"
        Dim queryCheckDb As String = "If exists (select name from master.dbo.sysdatabases where name = @dbName) select 1 else select 0"
        Try
            Dim dbname As String = "FetchInv"
            Using cmdCheck As New SqlCommand(queryCheckDb, con)
                cmdCheck.Parameters.AddWithValue("@dbName", dbname)
                If Convert.ToInt32(cmdCheck.ExecuteScalar()) = 1 Then
                    If Not Directory.Exists(backupFolderPath) Then
                        Directory.CreateDirectory(backupFolderPath)
                    End If
                    Dim backupFilePath As String = Path.Combine(backupFolderPath, dbname & "_backup_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".bak")
                    Dim queryBackup As String = "BACKUP DATABASE [" & dbname & "] TO DISK = @backupFilePath"
                    Using cmdBackup As New SqlCommand(queryBackup, con)
                        cmdBackup.Parameters.AddWithValue("@backupFilePath", backupFilePath)
                        cmdBackup.ExecuteNonQuery()
                    End Using
                End If
            End Using
        Catch EX As Exception
        End Try
        Dim conString As String = "Data Source=" & cmbServerName.Text & ";Initial Catalog=FetchInv;Integrated Security=True;MultipleActiveResultSets=True"
        Using con As New SqlConnection(conString)
            con.Open()
            Dim cb2 As String = "SELECT * FROM sys.databases WHERE name='FetchInv'"
            Using cmd As New SqlCommand(cb2, con)
                Using rdr As SqlDataReader = cmd.ExecuteReader()
                    If rdr.Read() Then
                        Dim cb1 As String = "USE Master ALTER DATABASE FetchInv SET Single_User WITH Rollback Immediate"
                        Using cmdDrop As New SqlCommand(cb1, con)
                            cmdDrop.ExecuteNonQuery()
                        End Using

                        Dim cbDrop As String = "DROP DATABASE FetchInv"
                        Using cmdDropDB As New SqlCommand(cbDrop, con)
                            cmdDropDB.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using
            Dim cb3 As String = "CREATE DATABASE FetchInv"
            Using cmdCreateDB As New SqlCommand(cb3, con)
                cmdCreateDB.ExecuteNonQuery()
            End Using

            Using sr As New StreamReader(Application.StartupPath & "\FetchInv.sql")
                Dim script As String = sr.ReadToEnd()
                Dim commands As String() = script.Split({"GO"}, StringSplitOptions.RemoveEmptyEntries)
                Using con1 As New SqlConnection(conString)
                    con1.Open()
                    For Each cmdText As String In commands
                        Using cmdExecuteScript As New SqlCommand(cmdText, con1)
                            cmdExecuteScript.ExecuteNonQuery()
                        End Using
                    Next
                End Using
            End Using
        End Using
    End Sub
    Private Sub ShowSuccessMessage(message As String)
        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub DropExistingDatabase(con As SqlConnection, dbName As String)
        Dim backupFolderPath As String = "C:\db_backup"
        Dim queryCheckDb As String = "If exists (select name from master.dbo.sysdatabases where name = @dbName) select 1 else select 0"

        Try
            Using cmdCheck As New SqlCommand(queryCheckDb, con)
                cmdCheck.Parameters.AddWithValue("@dbName", dbName)
                If Convert.ToInt32(cmdCheck.ExecuteScalar()) = 1 Then
                    If Not Directory.Exists(backupFolderPath) Then
                        Directory.CreateDirectory(backupFolderPath)
                    End If
                    Dim backupFilePath As String = Path.Combine(backupFolderPath, dbName & "_backup_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".bak")
                    Dim queryBackup As String = "BACKUP DATABASE [" & dbName & "] TO DISK = @backupFilePath"
                    Using cmdBackup As New SqlCommand(queryBackup, con)
                        cmdBackup.Parameters.AddWithValue("@backupFilePath", backupFilePath)
                        cmdBackup.ExecuteNonQuery()
                    End Using

                    ' Set the database to single-user mode to disconnect users
                    Dim querySingleUser As String = "ALTER DATABASE [" & dbName & "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE"
                    Using cmdSingleUser As New SqlCommand(querySingleUser, con)
                        cmdSingleUser.ExecuteNonQuery()
                    End Using

                    ' Drop the database
                    Dim queryDrop As String = "DROP DATABASE [" & dbName & "]"
                    Using cmdDrop As New SqlCommand(queryDrop, con)
                        cmdDrop.ExecuteNonQuery()
                    End Using
                End If
            End Using
        Catch ex As Exception
            ShowErrorMessage("Error in dropping existing database: " & ex.Message)
        End Try
    End Sub

    Private Sub CreateNewDatabase(con As SqlConnection, dbName As String)
        Dim query As String = "CREATE DATABASE [" & dbName & "]"
        Using cmd As New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub ExecuteDatabaseScript(con As SqlConnection, scriptPath As String)
        Dim script As String = File.ReadAllText(scriptPath)
        Try
            Dim conString As String = "Data Source=" & cmbServerName.Text & ";Initial Catalog=master;Integrated Security=True;MultipleActiveResultSets=True"
            Using con1 As New SqlConnection(conString)
                con1.Open()
                Dim cb2 As String = "SELECT * FROM sys.databases WHERE name='QUICK_BOOK_DB'"
                Using cmd As New SqlCommand(cb2, con1)
                    Using rdr As SqlDataReader = cmd.ExecuteReader()
                        If rdr.Read() Then
                            Dim cb1 As String = "USE Master ALTER DATABASE QUICK_BOOK_DB SET Single_User WITH Rollback Immediate DROP DATABASE QUICK_BOOK_DB"
                            Using cmdDrop As New SqlCommand(cb1, con)
                                cmdDrop.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                End Using
                Dim cb3 As String = "CREATE DATABASE QUICK_BOOK_DB"
                Using cmdCreateDB As New SqlCommand(cb3, con)
                    cmdCreateDB.ExecuteNonQuery()
                End Using
                Using sr As New StreamReader(Application.StartupPath & "\QUICK_BOOK_DB.sql")
                    Dim st As String = sr.ReadToEnd()
                    Dim server As New Microsoft.SqlServer.Management.Smo.Server(New Microsoft.SqlServer.Management.Common.ServerConnection(con))
                    server.ConnectionContext.ExecuteNonQuery(st)
                End Using
            End Using
        Catch ex As Exception
            'MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlsqlsettings.Visible = False
        pnl_login.Visible = False
        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value <= 95 Then
            ProgressBar1.Value += 5
        Else
            Timer1.Enabled = False
            ProgressBar1.Value = 0
            ProgressBar1.Visible = False
            load_form()
        End If

    End Sub

    Private Sub PrintInvoicePdfToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintInvoicePdfToolStripMenuItem.Click
        msgtitle = "ENTER INVOICE NO"
        msgcontent = ""
        inputdialog.ShowDialog()
        If msgresponse Then
            PrintA4(dialog_input_response_data)
        End If
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

        pnlmenu.Enabled = True
        pnlsqlsettings.Visible = False
        pnl_login.Visible = False
        Await companytables_and_data()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button5.Click, btnClose.Click
        If exitapp() Then
            Application.Exit()
        End If
    End Sub

    Private Sub CompanyInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanyInfoToolStripMenuItem.Click
        frm_company.Dispose()
        frm_company.ShowDialog()
    End Sub

    Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        pnlsqlsettings.Visible = True
        pnl_login.Visible = False
        Await ServerEnumerationAsync()
    End Sub

    Private Sub ShowInvoicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowInvoicesToolStripMenuItem.Click
        frmdgwrecords.Dispose()
        frmdgwrecords.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub

    Private Sub SubmissionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubmissionToolStripMenuItem.Click
        msgtitle = "ENTER CREDITNOTE NO"
        msgcontent = ""
        inputdialog.ShowDialog()
        If msgresponse Then
            creditnotePrintA4(dialog_input_response_data)
        End If
    End Sub

    Private Sub UsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsersToolStripMenuItem.Click
        frm_credinot_lst.Dispose()
        frm_credinot_lst.ShowDialog()
    End Sub


End Class
