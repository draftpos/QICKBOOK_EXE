Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Public Class CustomLineControl
    Inherits Control

    ' Private fields for properties
    Private _lineThickness As Integer = 2
    Private _lineStyle As DashStyle = DashStyle.Solid
    Private _lineColor As Color = Color.Black

    ' Public property for Line Thickness
    Public Property LineThickness() As Integer
        Get
            Return _lineThickness
        End Get
        Set(ByVal value As Integer)
            _lineThickness = value
            Me.Invalidate() ' Redraw the control
        End Set
    End Property

    ' Public property for Line Style
    Public Property LineStyle() As DashStyle
        Get
            Return _lineStyle
        End Get
        Set(ByVal value As DashStyle)
            _lineStyle = value
            Me.Invalidate() ' Redraw the control
        End Set
    End Property

    ' Public property for Line Color
    Public Property LineColor() As Color
        Get
            Return _lineColor
        End Get
        Set(ByVal value As Color)
            _lineColor = value
            Me.Invalidate() ' Redraw the control
        End Set
    End Property

    ' Constructor
    Public Sub New()
        Me.Size = New Size(200, 200) ' Default size
        Me.DoubleBuffered = True
    End Sub

    ' Overriding OnPaint method to draw the line
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)

        ' Create a pen with the specified color, thickness, and style
        Dim pen As New Pen(_lineColor, _lineThickness)
        pen.DashStyle = _lineStyle

        ' Draw a straight line across the control
        Dim startX As Integer = 10
        Dim startY As Integer = Me.Height \ 2
        Dim endX As Integer = Me.Width - 10
        Dim endY As Integer = Me.Height \ 2

        e.Graphics.DrawLine(pen, startX, startY, endX, endY)
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)

    End Sub
End Class
