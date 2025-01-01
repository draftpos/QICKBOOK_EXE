Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class frm_company
    Private Sub GelButton1_Click(sender As Object, e As EventArgs) Handles GelButton1.Click
        upadtecompnydata()
    End Sub

    Private Sub upadtecompnydata()
        Try
            ' Initialize the connection if it's not already done
            If con Is Nothing Then
                con = New SqlConnection(cs) ' Ensure `cs` is your valid connection string
            End If

            ' Open the connection if it's closed
            If con.State = ConnectionState.Closed Then con.Open()

            ' Check if data exists in the Company table
            Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM Company", con)
            Dim rowCount As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            ' Perform update or insert based on the row count
            If rowCount > 0 Then
                UpdateCompanyData()
            Else
                InsertCompanyData()
            End If

            MessageBox.Show("Operation completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure the connection is properly closed
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
    Private Sub UpdateCompanyData()
        Try
            Dim cb As New StringBuilder("UPDATE Company SET ")
            Dim isFirstField As Boolean = True
            cmd = New SqlCommand()
            cmd.Connection = con

            ' Add fields dynamically
            If Not String.IsNullOrEmpty(txtCompanyName.Text) Then
                cb.Append(If(isFirstField, "", ", ") & "CompanyName=@d1")
                cmd.Parameters.AddWithValue("@d1", txtCompanyName.Text)
                isFirstField = False
            End If

            If Not String.IsNullOrEmpty(cmbCountry.Text) Then
                cb.Append(If(isFirstField, "", ", ") & "Country=@d3")
                cmd.Parameters.AddWithValue("@d3", cmbCountry.Text)
                isFirstField = False
            End If

            ' Add other fields similarly...
            If Not String.IsNullOrEmpty(txtAddressLine1.Text) Then
                cb.Append(If(isFirstField, "", ", ") & "Address=@d4")
                cmd.Parameters.AddWithValue("@d4", txtAddressLine1.Text)
                isFirstField = False
            End If

            If PictureBox1.Image IsNot Nothing Then
                cb.Append(If(isFirstField, "", ", ") & "Logo=@d19")
                Dim ms As New MemoryStream()
                Dim bmpImage As New Bitmap(PictureBox1.Image)
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim data As Byte() = ms.ToArray()
                Dim p As New SqlParameter("@d19", SqlDbType.Image)
                p.Value = data
                cmd.Parameters.Add(p)
            End If
            cb.Append($", ServiceTaxNo='{txtServiceTaxNo.Text}',  QTC='{txtterm.Text}',VatNo='{txtVatNo.Text}',City='{txtAddressLine2.Text}',  Tin='{txtTIN.Text.Trim()}' ")
            ' Target the first row (assuming smallest ID represents the first row)
            cb.Append(" WHERE ID = (SELECT MIN(ID) FROM Company)")

            ' Set the command text and execute
            cmd.CommandText = cb.ToString()
            cmd.ExecuteNonQuery()

            'MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub InsertCompanyData()
        Dim cb As New StringBuilder("INSERT INTO Company (CompanyName, Country, Address, City, State, PinCode, ContactNo, Fax, Email, Logo) VALUES ")
        cb.Append("(@d1, @d3, @d4, @d5, @d6, @d7, @d8, @d9, @d10, @d19)")
        cmd = New SqlCommand(cb.ToString(), con)
        cmd.Parameters.AddWithValue("@d1", txtCompanyName.Text)
        cmd.Parameters.AddWithValue("@d3", cmbCountry.Text)
        cmd.Parameters.AddWithValue("@d4", txtAddressLine1.Text)
        cmd.Parameters.AddWithValue("@d5", txtAddressLine2.Text)
        cmd.Parameters.AddWithValue("@d6", txtAddressLine3.Text)
        cmd.Parameters.AddWithValue("@d7", txtPinCode.Text)
        cmd.Parameters.AddWithValue("@d8", txtContactNo.Text)
        cmd.Parameters.AddWithValue("@d9", txtfax.Text)
        cmd.Parameters.AddWithValue("@d10", txtEmail.Text)
        If PictureBox1.Image IsNot Nothing Then
            Dim ms As New MemoryStream()
            Dim bmpImage As New Bitmap(PictureBox1.Image)
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim data As Byte() = ms.ToArray()
            Dim p As New SqlParameter("@d19", SqlDbType.Image)
            p.Value = data
            cmd.Parameters.Add(p)
        End If
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub GelButton2_Click(sender As Object, e As EventArgs) Handles GelButton2.Click
        If exitmsg() Then
            Me.Dispose()
        End If
    End Sub
    Private Sub frm_company_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        companyInfo = getOrgName()

        If companyInfo IsNot Nothing Then
            txtCompanyName.Text = companyInfo.CompanyName
            cmbCountry.Text = companyInfo.Country
            txtAddressLine1.Text = companyInfo.Address
            txtAddressLine2.Text = companyInfo.City
            txtAddressLine3.Text = companyInfo.State
            txtPinCode.Text = companyInfo.PinCode
            txtContactNo.Text = companyInfo.ContactNo
            txtfax.Text = companyInfo.Fax
            txtEmail.Text = companyInfo.Email
            With companyInfo
                txtVatNo.Text = .VatNo
                txtterm.Text = .QTC
                txtAddressLine2.Text = .City
                txtServiceTaxNo.Text = .ServiceTaxNo
                txtTIN.Text = .TIN
            End With
            Try

                If companyInfo.Logo IsNot Nothing Then
                    Using ms As New MemoryStream(companyInfo.Logo)
                        PictureBox1.Image = Image.FromStream(ms)
                    End Using
                Else
                    PictureBox1.Image = Nothing
                End If
            Catch ex As Exception

            End Try
        Else
            MessageBox.Show("No company information found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim op As New OpenFileDialog With {
            .Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
            .Title = "Select an Image"
        }
        If op.ShowDialog() = DialogResult.OK Then
            Try
                PictureBox1.Image = Image.FromFile(op.FileName)
            Catch ex As Exception
                MessageBox.Show("Invalid image file selected. Please choose a valid image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub frm_company_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.Show()
    End Sub
End Class