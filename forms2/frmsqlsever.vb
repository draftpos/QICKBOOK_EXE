Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports MetroFramework
Imports MetroFramework.Forms
Imports System.Data.Sql
Imports System.Threading.Tasks

Public Class frmSqlsever
    Dim st As String
    Dim SqlConnStr As String


    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        If lblSet.Text = "Main Form" Then
            Me.Close()
        Else
            If ConfirmCloseApplication() Then
                End
            End If
        End If
    End Sub


    Private Sub cmbAuthentication_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAuthentication.SelectedIndexChanged
        If cmbAuthentication.SelectedIndex = 0 Then
            txtUserName.ReadOnly = True
            txtPassword.ReadOnly = True
            txtUserName.Text = ""
            txtPassword.Text = ""
        ElseIf cmbAuthentication.SelectedIndex = 1 Then
            txtUserName.ReadOnly = False
            txtPassword.ReadOnly = False
        End If
    End Sub

    Private Sub cmbServerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbServerName.SelectedIndexChanged
        cmbAuthentication.Enabled = True
    End Sub

    Public Sub Reset()
        txtPassword.Text = ""
        txtUserName.Text = ""
        cmbServerName.Text = ""
        cmbAuthentication.SelectedIndex = 0
        cmbInstallationType.SelectedIndex = 0
        pnl_load.Visible = True
        lblposting.Visible = True
    End Sub

    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        ValidateServerDetails()

        Dim SqlConn As New SqlConnection
        ConfigureSqlConnection()

        If SqlConn.State = ConnectionState.Closed Then
            SqlConn.ConnectionString = SqlConnStr
            Try
                SqlConn.Open()
                ShowSuccessMessage("Successful DB Connection")
            Catch ex As Exception
                ShowErrorMessage("Invalid DB Connection: " & ex.Message)
            End Try
        End If
    End Sub


    Private Sub CreateBlankDB()
        Try
            Using con As New SqlConnection("Data Source=" & cmbServerName.Text & ";Initial Catalog=master;Integrated Security=True;MultipleActiveResultSets=True")
                con.Open()
                DropExistingDatabase(con, "RetailPOS_DB")
                CreateNewDatabase(con, "RetailPOS_DB")
                ExecuteDatabaseScript(con, Application.StartupPath & "\BlankDBscript.sql")
            End Using
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Async Sub frmSqlServerSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnl_load.BringToFront()
        Reset()
        ' Await ServerEnumerationAsync()
        load_form()
        ' Timer6.Enabled = True
    End Sub


    Private Async Sub load_form()
        Reset()

        If File.Exists(filePathsqlsetting) Then
            '     Try
            sql = "SELECT 1"
            dt = Crud(sql, Nothing)
            If dt.Rows.Count = 0 Then
                msgtitle = ("Connection isn't Valid.")
                msgcontent = $"Invalid Connection String {vbCrLf} Click Yes to Proceed and No to Connect the DB..."
                msgdialog.ShowDialog()
                If msgresponse Then
                    'pnlsqlsettings.Visible = True
                    'pnl_login.Visible = False
                    Await ServerEnumerationAsync()
                    Return
                End If
            End If
            ' msgresponse = False
            ' Catch ex As Exception
            '    msgresponse = False
            '   msgcontent = ($"Failed to check connection: {vbCrLf}  {ex.Message.ToString()}  {vbCrLf} Click Yes to Proceed and No to Connect the DB...")
            '  msgcontent = "Invalid Connection String"
            ' msgdialog.ShowDialog()

            ' Return
            ' End Try
            ''''''''''''''''outer because of the try
            ' If msgresponse Then
            ' Await ServerEnumerationAsync()
            ' Return
            ' End If




        Else
11:         Await ServerEnumerationAsync()
            Return
        End If
        frm_Login.Dispose()
        frm_Login.Show()
        Me.Dispose()
    End Sub
    Async Function ServerEnumerationAsync() As Task
        pnl_load.Visible = True
        pnl_load.BringToFront()
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
        pnl_load.Visible = False
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
    Private Sub cmbInstallationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbInstallationType.SelectedIndexChanged
        If cmbInstallationType.SelectedIndex = 1 Then
            cmbAuthentication.SelectedIndex = 1
            cmbAuthentication.Enabled = False
            btnTestConnection.Visible = True
            btnDemoDB.Text = "Save Setting"
        ElseIf cmbInstallationType.SelectedIndex = 0 Then
            cmbAuthentication.SelectedIndex = 0
            cmbAuthentication.Enabled = True
            btnTestConnection.Visible = True
            btnDemoDB.Text = "Create DB and Proceed"
        End If
    End Sub

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


    Private Sub ExecuteDatabaseScriptWithGO(con As SqlConnection, scriptPath As String)
        Dim script As String = File.ReadAllText(scriptPath)
        Dim server As New Server(New ServerConnection(con))
        server.ConnectionContext.ExecuteNonQuery(script)
    End Sub

    Private Sub ShowSuccessMessage(message As String)
        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ShowErrorMessage(message As String)
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub
    Private dotCounter As Integer = 0
    Private Sub UpdateLABELCOUNTCount()
        If pnl_load.Visible = False Then
            Timer6.Enabled = False
            'load_form()
        End If
        lblposting.Visible = True
        dotCounter = (dotCounter + 1) Mod 4 ' Cycle through 0, 1, 2, 3
        Dim dots As String = New String("."c, dotCounter)
        lblposting.Text = $"Loading Server {dots}"
    End Sub
    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        UpdateLABELCOUNTCount()
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

    Private Sub btnDemoDB_Click(sender As Object, e As EventArgs) Handles btnDemoDB.Click
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
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub pnl_load_Paint(sender As Object, e As PaintEventArgs) Handles pnl_load.Paint

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
