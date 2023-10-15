Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class ResetPassword
    Private Sub ResetPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox2.Text = TextBox1.Text Then
            Label3.Text = "Password matched"
            Label3.ForeColor = Color.FromArgb(0, 128, 0)
        Else
            Label3.Text = "Password didn't matched"
            Label3.ForeColor = Color.FromArgb(255, 0, 0)
        End If
    End Sub

    Public Sub Forgot()
        Dim Query As String
        Dim SHA256 As New SHA256Managed()

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE amsusers 
            set userPassword=@password
            WHERE userID=@ID"
            Command = New MySqlCommand(Query, MysqlConn)

            Dim hashBytes() As Byte = SHA256.ComputeHash(Encoding.UTF8.GetBytes(TextBox2.Text))
            Dim hashedValue As String = BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToUpper()

            Command.Parameters.AddWithValue("@password", hashedValue)
            Command.Parameters.AddWithValue("@ID", usersID)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("User Data Update", "Update Users", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Forgot()
        Me.Dispose()
        LoginForm.Show()
    End Sub
End Class