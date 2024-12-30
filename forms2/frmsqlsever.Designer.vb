<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSqlsever
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSqlsever))
        Me.Timer6 = New System.Windows.Forms.Timer(Me.components)
        Me.pnl_load = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblposting = New System.Windows.Forms.Label()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbInstallationType = New System.Windows.Forms.ComboBox()
        Me.btnTestConnection = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbAuthentication = New System.Windows.Forms.ComboBox()
        Me.cmbServerName = New System.Windows.Forms.ComboBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Timer5 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSet = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnDemoDB = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnl_load.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer6
        '
        Me.Timer6.Enabled = True
        Me.Timer6.Interval = 500
        '
        'pnl_load
        '
        Me.pnl_load.BackColor = System.Drawing.Color.White
        Me.pnl_load.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_load.Controls.Add(Me.PictureBox1)
        Me.pnl_load.Controls.Add(Me.lblposting)
        Me.pnl_load.Location = New System.Drawing.Point(-8, -11)
        Me.pnl_load.Name = "pnl_load"
        Me.pnl_load.Size = New System.Drawing.Size(449, 292)
        Me.pnl_load.TabIndex = 5
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = Global.Havano_Fiscal.My.Resources.Resources._1497__2_
        Me.PictureBox1.Location = New System.Drawing.Point(87, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(260, 240)
        Me.PictureBox1.TabIndex = 29
        Me.PictureBox1.TabStop = False
        '
        'lblposting
        '
        Me.lblposting.AutoSize = True
        Me.lblposting.Font = New System.Drawing.Font("Arial Rounded MT Bold", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblposting.Location = New System.Drawing.Point(126, 257)
        Me.lblposting.Name = "lblposting"
        Me.lblposting.Size = New System.Drawing.Size(165, 24)
        Me.lblposting.TabIndex = 28
        Me.lblposting.Text = "Loading Server"
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.Purple
        Me.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConnect.FlatAppearance.BorderSize = 0
        Me.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConnect.Location = New System.Drawing.Point(129, 157)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(144, 40)
        Me.btnConnect.TabIndex = 23
        Me.btnConnect.Text = "Connect to DB"
        Me.btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 48)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 15)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Installation Type :"
        '
        'cmbInstallationType
        '
        Me.cmbInstallationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInstallationType.FormattingEnabled = True
        Me.cmbInstallationType.Items.AddRange(New Object() {"Server Installation", "Client Installation"})
        Me.cmbInstallationType.Location = New System.Drawing.Point(154, 48)
        Me.cmbInstallationType.Name = "cmbInstallationType"
        Me.cmbInstallationType.Size = New System.Drawing.Size(259, 21)
        Me.cmbInstallationType.TabIndex = 1
        '
        'btnTestConnection
        '
        Me.btnTestConnection.BackColor = System.Drawing.Color.Purple
        Me.btnTestConnection.FlatAppearance.BorderSize = 0
        Me.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTestConnection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btnTestConnection.ForeColor = System.Drawing.Color.White
        Me.btnTestConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTestConnection.Location = New System.Drawing.Point(12, 157)
        Me.btnTestConnection.Name = "btnTestConnection"
        Me.btnTestConnection.Size = New System.Drawing.Size(111, 40)
        Me.btnTestConnection.TabIndex = 6
        Me.btnTestConnection.Text = "Test DB Connection"
        Me.btnTestConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTestConnection.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 73)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 15)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Authentication :"
        '
        'cmbAuthentication
        '
        Me.cmbAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAuthentication.FormattingEnabled = True
        Me.cmbAuthentication.Items.AddRange(New Object() {"Windows Authentication", "SQL Server Authentication"})
        Me.cmbAuthentication.Location = New System.Drawing.Point(154, 73)
        Me.cmbAuthentication.Name = "cmbAuthentication"
        Me.cmbAuthentication.Size = New System.Drawing.Size(259, 21)
        Me.cmbAuthentication.TabIndex = 2
        '
        'cmbServerName
        '
        Me.cmbServerName.FormattingEnabled = True
        Me.cmbServerName.Location = New System.Drawing.Point(154, 22)
        Me.cmbServerName.Name = "cmbServerName"
        Me.cmbServerName.Size = New System.Drawing.Size(259, 21)
        Me.cmbServerName.TabIndex = 0
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(154, 126)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(259, 20)
        Me.txtPassword.TabIndex = 4
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(154, 100)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(259, 20)
        Me.txtUserName.TabIndex = 3
        '
        'Timer5
        '
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Lucida Sans", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(7, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(193, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SQL Server Setting"
        '
        'lblSet
        '
        Me.lblSet.AutoSize = True
        Me.lblSet.Location = New System.Drawing.Point(61, 7)
        Me.lblSet.Name = "lblSet"
        Me.lblSet.Size = New System.Drawing.Size(23, 13)
        Me.lblSet.TabIndex = 21
        Me.lblSet.Text = "Set"
        Me.lblSet.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 100)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 15)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "SQL Server User Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 126)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "SQL User Password :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 22)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 15)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "SQL Server Name :"
        '
        'btnDemoDB
        '
        Me.btnDemoDB.BackColor = System.Drawing.Color.Purple
        Me.btnDemoDB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDemoDB.FlatAppearance.BorderSize = 0
        Me.btnDemoDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemoDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDemoDB.ForeColor = System.Drawing.Color.White
        Me.btnDemoDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemoDB.Location = New System.Drawing.Point(276, 156)
        Me.btnDemoDB.Name = "btnDemoDB"
        Me.btnDemoDB.Size = New System.Drawing.Size(137, 40)
        Me.btnDemoDB.TabIndex = 8
        Me.btnDemoDB.Text = "Create DB  and Proceed"
        Me.btnDemoDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDemoDB.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnConnect)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbInstallationType)
        Me.GroupBox1.Controls.Add(Me.btnTestConnection)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cmbAuthentication)
        Me.GroupBox1.Controls.Add(Me.cmbServerName)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.btnDemoDB)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 219)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SQL Server Configuration"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Purple
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.lblSet)
        Me.Panel2.Location = New System.Drawing.Point(-2, -1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(441, 41)
        Me.Panel2.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(1, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(439, 277)
        Me.Panel1.TabIndex = 4
        '
        'frmSqlsever
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(440, 278)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_load)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSqlsever"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Havano Fiscal"
        Me.pnl_load.ResumeLayout(False)
        Me.pnl_load.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer6 As Timer
    Friend WithEvents pnl_load As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblposting As Label
    Friend WithEvents btnConnect As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbInstallationType As ComboBox
    Friend WithEvents btnTestConnection As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbAuthentication As ComboBox
    Friend WithEvents cmbServerName As ComboBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents Timer5 As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents lblSet As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnDemoDB As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
End Class
