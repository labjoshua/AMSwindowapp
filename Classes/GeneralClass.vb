Imports FontAwesome.Sharp
Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows

Public Class GeneralClass
    Private Shared currentChildForm As Form
    Public Sub Login()
        Dim Query As String
        Dim Count As Integer
        Dim UsrAcc, UsrPass As String
        Dim SHA256 As New SHA256Managed

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT * FROM amsusers WHERE userName=@account AND userPassword=@authentication"
            Command = New MySqlCommand(Query, MysqlConn)

            Dim hashBytes() As Byte = SHA256.ComputeHash(Encoding.UTF8.GetBytes(LoginForm.TextBox2.Text))
            Dim hashedValue As String = BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToUpper()

            Command.Parameters.AddWithValue("@account", LoginForm.TextBox1.Text)
            Command.Parameters.AddWithValue("@authentication", hashedValue)
            CmdRead = Command.ExecuteReader

            Count = 0
            UsrAcc = ""
            UsrPass = ""
            While CmdRead.Read
                Count = Count + 1
                UsrAcc = CmdRead.GetString("userName")
                AdminID = CmdRead.GetInt32("userID")
                UsrPass = CmdRead.GetString("userPassword")
                Enconder = CmdRead.GetString("userFullName")
                AccountType = CmdRead.GetString("userAccountRole")
                AdminEmail = CmdRead.GetString("email")
            End While

            If Count = 1 AndAlso LoginForm.TextBox1.Text = UsrAcc AndAlso hashedValue = UsrPass Then
                DashboardForm.Show()
                LoginForm.Hide()
                LoginForm.TextBox1.Clear()
                LoginForm.TextBox2.Clear()
                DashboardForm.Label1.Text = Enconder
                DashboardForm.Label2.Text = AccountType
                DashboardForm.Label5.Text = host

            Else
                MessageBox.Show("Username or Password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                LoginForm.TextBox1.Clear()
                LoginForm.TextBox2.Clear()
            End If

            MysqlConn.Close()

        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If

            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub ChildInsideChildForm(From As Form, ContainPnl As Panel)
        From.TopLevel = False

        If currentChildForm IsNot Nothing AndAlso Not currentChildForm.IsDisposed Then
            currentChildForm.Close()
        End If
        If Not From.IsDisposed Then
            ContainPnl.Controls.Add(From)
            From.Dock = DockStyle.Fill
            From.BringToFront()
            From.Show()
            currentChildForm = From
        End If
    End Sub


    Public Sub ShowFormToParentContainer(Frm As Form, ContainPanel As Panel)
        Frm.TopLevel = False
        ContainPanel.Controls.Add(Frm)
        Frm.Dock = DockStyle.Fill
        Frm.BringToFront()
        Frm.Show()
    End Sub
End Class
