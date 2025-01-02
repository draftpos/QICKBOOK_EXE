<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMonitor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMonitor))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnMini = New System.Windows.Forms.PictureBox()
        Me.btnImgExit = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Pic_logo = New System.Windows.Forms.PictureBox()
        Me.lblnotify = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.tmr_start = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MyNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.btnMini, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnImgExit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.Pic_logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnMini)
        Me.Panel1.Controls.Add(Me.btnImgExit)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(522, 29)
        Me.Panel1.TabIndex = 3
        '
        'btnMini
        '
        Me.btnMini.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMini.Image = Global.HZ_Monitor.My.Resources.Resources.minimize_2
        Me.btnMini.Location = New System.Drawing.Point(468, 0)
        Me.btnMini.Name = "btnMini"
        Me.btnMini.Size = New System.Drawing.Size(26, 27)
        Me.btnMini.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnMini.TabIndex = 3
        Me.btnMini.TabStop = False
        '
        'btnImgExit
        '
        Me.btnImgExit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnImgExit.Image = Global.HZ_Monitor.My.Resources.Resources.close
        Me.btnImgExit.Location = New System.Drawing.Point(494, 0)
        Me.btnImgExit.Name = "btnImgExit"
        Me.btnImgExit.Size = New System.Drawing.Size(26, 27)
        Me.btnImgExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnImgExit.TabIndex = 2
        Me.btnImgExit.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "HavanoZimra Monitor"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.PaleGreen
        Me.lblStatus.Location = New System.Drawing.Point(22, 115)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(481, 24)
        Me.lblStatus.TabIndex = 10
        Me.lblStatus.Text = "Fiscal Day Status Unknown"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Pic_logo)
        Me.GroupBox2.Controls.Add(Me.lblnotify)
        Me.GroupBox2.Location = New System.Drawing.Point(22, 144)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(481, 105)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        '
        'Pic_logo
        '
        Me.Pic_logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pic_logo.Image = Global.HZ_Monitor.My.Resources.Resources.send_icon
        Me.Pic_logo.Location = New System.Drawing.Point(12, 22)
        Me.Pic_logo.Name = "Pic_logo"
        Me.Pic_logo.Size = New System.Drawing.Size(62, 61)
        Me.Pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Pic_logo.TabIndex = 4
        Me.Pic_logo.TabStop = False
        '
        'lblnotify
        '
        Me.lblnotify.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!)
        Me.lblnotify.ForeColor = System.Drawing.Color.White
        Me.lblnotify.Location = New System.Drawing.Point(80, 27)
        Me.lblnotify.Name = "lblnotify"
        Me.lblnotify.Size = New System.Drawing.Size(393, 51)
        Me.lblnotify.TabIndex = 5
        Me.lblnotify.Text = "Hz Monitor Stopped"
        Me.lblnotify.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnStart)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(481, 65)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Image = Global.HZ_Monitor.My.Resources.Resources.close_color_sm
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(304, 18)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(169, 33)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close Fiscal Day"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnStart.ForeColor = System.Drawing.Color.White
        Me.btnStart.Image = Global.HZ_Monitor.My.Resources.Resources.start_sm
        Me.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStart.Location = New System.Drawing.Point(6, 18)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(169, 33)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start Fiscal Day"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'tmr_start
        '
        Me.tmr_start.Interval = 5000
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(537, 58)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(179, 175)
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'MyNotifyIcon
        '
        Me.MyNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.MyNotifyIcon.BalloonTipText = "Hz Monitor is Running"
        Me.MyNotifyIcon.Icon = CType(resources.GetObject("MyNotifyIcon.Icon"), System.Drawing.Icon)
        Me.MyNotifyIcon.Text = "Hz Monitor Running"
        Me.MyNotifyIcon.Visible = True
        '
        'FrmMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Purple
        Me.ClientSize = New System.Drawing.Size(522, 271)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmMonitor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HavanoZimra Monitor"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnMini, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnImgExit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.Pic_logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnImgExit As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Pic_logo As PictureBox
    Friend WithEvents lblnotify As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnClose As Button
    Friend WithEvents btnStart As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents tmr_start As Timer
    Friend WithEvents btnMini As PictureBox
    Friend WithEvents MyNotifyIcon As NotifyIcon
End Class
