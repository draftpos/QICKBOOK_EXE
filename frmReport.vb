Imports CrystalDecisions
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Web.Mail
Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports System.Text
Imports System.Security.Cryptography
Imports Org.BouncyCastle.Asn1.Ocsp
Imports HavanoZimra
Public Class frmReport
    Dim pdfFile As String = My.Application.Info.DirectoryPath & "\PDF Reports\RetailPOSReport " & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".Pdf"
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Cursor = Cursors.Default
        Timer1.Enabled = False
    End Sub

    Private Sub frm_company_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.Show()
    End Sub
End Class