Imports System.IO
Imports System.Windows.Forms

Module Interval_refresh_Library
    Private refreshTimer As Timer
    Public Sub InitializeRefreshTimer()
        Dim filePath As String = Path.Combine(Application.StartupPath, "refreshinterval.dll")
        If Not File.Exists(filePath) Then
            File.WriteAllText(filePath, "3") ' Default interval is 3 seconds
        End If
        Dim interval As Integer
        Dim fileContent As String = File.ReadAllText(filePath)
        If Integer.TryParse(fileContent, interval) Then
            interval *= 1000
        Else
            interval = 3000
        End If
        If refreshTimer Is Nothing Then
            refreshTimer = New Timer()
            AddHandler refreshTimer.Tick, AddressOf RefreshCode
        End If
        refreshTimer.Interval = interval
        refreshTimer.Start()
        Console.WriteLine($"Timer Refresh is now active with an interval of {interval / 1000} seconds.")
    End Sub
    Private Sub RefreshCode(sender As Object, e As EventArgs)
        Try
            Console.WriteLine("Refreshing data...")
            frmdgwrecords.TextBox1_TextChanged(Nothing, EventArgs.Empty)
            frm_credinot_lst.TextBox1_TextChanged(Nothing, EventArgs.Empty)
        Catch ex As Exception
            Console.WriteLine($"Error during refresh: {ex.Message}")
        End Try
    End Sub
End Module
