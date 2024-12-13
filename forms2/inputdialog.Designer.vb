<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class inputdialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GelButton2 = New GelButtons.GelButton()
        Me.GelButton1 = New GelButtons.GelButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblmsgtitle = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.pnp1 = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnldate = New System.Windows.Forms.Panel()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CustomLineControl1 = New WindowsApp1.CustomLineControl()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnp1.SuspendLayout()
        Me.pnldate.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GelButton2
        '
        Me.GelButton2.BackColor = System.Drawing.Color.SteelBlue
        Me.GelButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GelButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GelButton2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GelButton2.ForeColor = System.Drawing.Color.White
        Me.GelButton2.GradientBottom = System.Drawing.Color.Purple
        Me.GelButton2.GradientTop = System.Drawing.Color.Purple
        Me.GelButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GelButton2.Location = New System.Drawing.Point(172, 122)
        Me.GelButton2.Name = "GelButton2"
        Me.GelButton2.Size = New System.Drawing.Size(89, 46)
        Me.GelButton2.TabIndex = 405
        Me.GelButton2.Text = "Ok"
        Me.GelButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.GelButton2.UseVisualStyleBackColor = False
        '
        'GelButton1
        '
        Me.GelButton1.BackColor = System.Drawing.Color.SteelBlue
        Me.GelButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GelButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GelButton1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GelButton1.ForeColor = System.Drawing.Color.White
        Me.GelButton1.GradientBottom = System.Drawing.Color.Purple
        Me.GelButton1.GradientTop = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GelButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GelButton1.Location = New System.Drawing.Point(77, 122)
        Me.GelButton1.Name = "GelButton1"
        Me.GelButton1.Size = New System.Drawing.Size(89, 46)
        Me.GelButton1.TabIndex = 404
        Me.GelButton1.Text = "Can&cel"
        Me.GelButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.GelButton1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Uighur", 10.0!, System.Drawing.FontStyle.Italic)
        Me.Label1.Location = New System.Drawing.Point(106, 166)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 21)
        Me.Label1.TabIndex = 407
        Me.Label1.Text = "Havano Pos_Cutomized msg"
        '
        'lblmsgtitle
        '
        Me.lblmsgtitle.Font = New System.Drawing.Font("Arial Rounded MT Bold", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmsgtitle.Location = New System.Drawing.Point(1, -1)
        Me.lblmsgtitle.Name = "lblmsgtitle"
        Me.lblmsgtitle.Size = New System.Drawing.Size(361, 62)
        Me.lblmsgtitle.TabIndex = 400
        Me.lblmsgtitle.Text = "Label1"
        Me.lblmsgtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Maiandra GD", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(5, 72)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(357, 40)
        Me.TextBox1.TabIndex = 408
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pnp1
        '
        Me.pnp1.Controls.Add(Me.ComboBox1)
        Me.pnp1.Location = New System.Drawing.Point(-2, 67)
        Me.pnp1.Name = "pnp1"
        Me.pnp1.Size = New System.Drawing.Size(346, 51)
        Me.pnp1.TabIndex = 409
        Me.pnp1.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(11, 6)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(318, 39)
        Me.ComboBox1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Lucida Console", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(2, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 19)
        Me.Label2.TabIndex = 410
        Me.Label2.Text = "From"
        '
        'pnldate
        '
        Me.pnldate.Controls.Add(Me.DateTimePicker2)
        Me.pnldate.Controls.Add(Me.DateTimePicker1)
        Me.pnldate.Controls.Add(Me.Label3)
        Me.pnldate.Controls.Add(Me.Label2)
        Me.pnldate.Location = New System.Drawing.Point(2, 67)
        Me.pnldate.Name = "pnldate"
        Me.pnldate.Size = New System.Drawing.Size(360, 50)
        Me.pnldate.TabIndex = 411
        Me.pnldate.Visible = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker2.Font = New System.Drawing.Font("Lucida Console", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(219, 14)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(135, 26)
        Me.DateTimePicker2.TabIndex = 413
        Me.DateTimePicker2.Value = New Date(2024, 10, 23, 0, 0, 0, 0)
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Lucida Console", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(55, 14)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(134, 26)
        Me.DateTimePicker1.TabIndex = 412
        Me.DateTimePicker1.Value = New Date(2024, 10, 23, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Lucida Console", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(190, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 19)
        Me.Label3.TabIndex = 411
        Me.Label3.Text = "To"
        '
        'CustomLineControl1
        '
        Me.CustomLineControl1.BackColor = System.Drawing.Color.White
        Me.CustomLineControl1.ForeColor = System.Drawing.Color.Cyan
        Me.CustomLineControl1.LineColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.CustomLineControl1.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.CustomLineControl1.LineThickness = 2
        Me.CustomLineControl1.Location = New System.Drawing.Point(-3, 53)
        Me.CustomLineControl1.Name = "CustomLineControl1"
        Me.CustomLineControl1.Size = New System.Drawing.Size(365, 13)
        Me.CustomLineControl1.TabIndex = 401
        Me.CustomLineControl1.Text = "CustomLineControl1"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(1, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(22, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 406
        Me.PictureBox1.TabStop = False
        '
        'inputdialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(364, 180)
        Me.Controls.Add(Me.GelButton2)
        Me.Controls.Add(Me.GelButton1)
        Me.Controls.Add(Me.CustomLineControl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblmsgtitle)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.pnldate)
        Me.Controls.Add(Me.pnp1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "inputdialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.pnp1.ResumeLayout(False)
        Me.pnldate.ResumeLayout(False)
        Me.pnldate.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GelButton2 As GelButtons.GelButton
    Friend WithEvents GelButton1 As GelButtons.GelButton
    Friend WithEvents CustomLineControl1 As CustomLineControl
    Friend WithEvents Label1 As Label
    Friend WithEvents lblmsgtitle As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents pnp1 As Panel
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents pnldate As Panel
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents PictureBox1 As PictureBox
End Class
