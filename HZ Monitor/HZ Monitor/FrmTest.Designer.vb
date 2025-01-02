<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTest
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
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.btnAddInvoice = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtid = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnAddItem
        '
        Me.btnAddItem.Location = New System.Drawing.Point(120, 43)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(88, 33)
        Me.btnAddItem.TabIndex = 9
        Me.btnAddItem.Text = "Add Item"
        Me.btnAddItem.UseVisualStyleBackColor = True
        '
        'btnAddInvoice
        '
        Me.btnAddInvoice.Location = New System.Drawing.Point(23, 43)
        Me.btnAddInvoice.Name = "btnAddInvoice"
        Me.btnAddInvoice.Size = New System.Drawing.Size(91, 33)
        Me.btnAddInvoice.TabIndex = 8
        Me.btnAddInvoice.Text = "Add Invoice"
        Me.btnAddInvoice.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(26, 100)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 33)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Add CreditNote"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(120, 100)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(88, 33)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Add CN Item"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Transaction ID"
        '
        'txtid
        '
        Me.txtid.Location = New System.Drawing.Point(120, 13)
        Me.txtid.Name = "txtid"
        Me.txtid.Size = New System.Drawing.Size(124, 20)
        Me.txtid.TabIndex = 13
        Me.txtid.Text = "TXN12348"
        '
        'FrmTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(273, 167)
        Me.Controls.Add(Me.txtid)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnAddItem)
        Me.Controls.Add(Me.btnAddInvoice)
        Me.Name = "FrmTest"
        Me.Text = "FrmTest"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnAddItem As Button
    Friend WithEvents btnAddInvoice As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtid As TextBox
End Class
