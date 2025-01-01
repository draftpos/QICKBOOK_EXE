<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class msgdialog
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
        Me.lblmsgtitle = New System.Windows.Forms.Label()
        Me.lblmsgcontent = New System.Windows.Forms.Label()
        Me.CustomLineControl1 = New Havano_Fiscal.CustomLineControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GelButton2 = New GelButtons.GelButton()
        Me.GelButton1 = New GelButtons.GelButton()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblmsgtitle
        '
        Me.lblmsgtitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblmsgtitle.Font = New System.Drawing.Font("Arial Rounded MT Bold", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmsgtitle.Location = New System.Drawing.Point(0, 0)
        Me.lblmsgtitle.Name = "lblmsgtitle"
        Me.lblmsgtitle.Size = New System.Drawing.Size(360, 79)
        Me.lblmsgtitle.TabIndex = 0
        Me.lblmsgtitle.Text = "Label1"
        Me.lblmsgtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblmsgcontent
        '
        Me.lblmsgcontent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblmsgcontent.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmsgcontent.Location = New System.Drawing.Point(0, 80)
        Me.lblmsgcontent.Name = "lblmsgcontent"
        Me.lblmsgcontent.Size = New System.Drawing.Size(361, 124)
        Me.lblmsgcontent.TabIndex = 395
        Me.lblmsgcontent.Text = "Label2"
        Me.lblmsgcontent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CustomLineControl1
        '
        Me.CustomLineControl1.BackColor = System.Drawing.Color.White
        Me.CustomLineControl1.ForeColor = System.Drawing.Color.Cyan
        Me.CustomLineControl1.LineColor = System.Drawing.Color.WhiteSmoke
        Me.CustomLineControl1.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.CustomLineControl1.LineThickness = 2
        Me.CustomLineControl1.Location = New System.Drawing.Point(4, 71)
        Me.CustomLineControl1.Name = "CustomLineControl1"
        Me.CustomLineControl1.Size = New System.Drawing.Size(352, 10)
        Me.CustomLineControl1.TabIndex = 1
        Me.CustomLineControl1.Text = "CustomLineControl1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Uighur", 6.0!, System.Drawing.FontStyle.Italic)
        Me.Label1.Location = New System.Drawing.Point(31, 253)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 399
        Me.Label1.Text = "Havano Pos_Cutomized msg"
        '
        'GelButton2
        '
        Me.GelButton2.BackColor = System.Drawing.Color.SteelBlue
        Me.GelButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GelButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GelButton2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GelButton2.ForeColor = System.Drawing.Color.White
        Me.GelButton2.GradientBottom = System.Drawing.Color.Purple
        Me.GelButton2.GradientTop = System.Drawing.Color.Purple
        Me.GelButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GelButton2.Location = New System.Drawing.Point(179, 208)
        Me.GelButton2.Name = "GelButton2"
        Me.GelButton2.Size = New System.Drawing.Size(94, 52)
        Me.GelButton2.TabIndex = 397
        Me.GelButton2.Text = "Ok"
        Me.GelButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.GelButton2.UseVisualStyleBackColor = False
        '
        'GelButton1
        '
        Me.GelButton1.BackColor = System.Drawing.Color.SteelBlue
        Me.GelButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GelButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GelButton1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GelButton1.ForeColor = System.Drawing.Color.White
        Me.GelButton1.GradientBottom = System.Drawing.Color.Red
        Me.GelButton1.GradientTop = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GelButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GelButton1.Location = New System.Drawing.Point(80, 208)
        Me.GelButton1.Name = "GelButton1"
        Me.GelButton1.Size = New System.Drawing.Size(94, 52)
        Me.GelButton1.TabIndex = 396
        Me.GelButton1.Text = "Can&cel"
        Me.GelButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.GelButton1.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(338, 18)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(30, 27)
        Me.btnCancel.TabIndex = 394
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = False
        Me.btnCancel.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(3, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 35)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 398
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblmsgtitle)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 81)
        Me.Panel1.TabIndex = 400
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.CustomLineControl1)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.GelButton2)
        Me.Panel2.Controls.Add(Me.GelButton1)
        Me.Panel2.Controls.Add(Me.lblmsgcontent)
        Me.Panel2.Location = New System.Drawing.Point(8, 8)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(362, 265)
        Me.Panel2.TabIndex = 0
        '
        'msgdialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(379, 279)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "msgdialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Havano Fiscal"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblmsgtitle As Label
    Friend WithEvents CustomLineControl1 As CustomLineControl
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblmsgcontent As Label
    Friend WithEvents GelButton1 As GelButtons.GelButton
    Friend WithEvents GelButton2 As GelButtons.GelButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
End Class
