Imports FontAwesome.Sharp
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Gmail.v1
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json.Linq
Imports System
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Windows
Imports Google.Apis.Gmail.v1.Data
Public Class VerifyEmail
    Private service As GmailService
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Close()
        LoginForm.Show()
    End Sub


    Function GenerateOTP(lengthh As Integer) As String
        Dim random As New Random
        Dim otpBuilder As New StringBuilder

        For i As Integer = 1 To lengthh
            otpBuilder.Append(random.Next(0, 10))
        Next

        Return otpBuilder.ToString
    End Function

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim Query As String
        Dim Count As Integer

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT * FROM amsusers WHERE BINARY userName = @account AND BINARY email = @email"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@account", TextBox2.Text)
            Command.Parameters.AddWithValue("@email", TextBox1.Text)
            CmdRead = Command.ExecuteReader

            Count = 0

            While CmdRead.Read
                Count = Count + 1
                AdminForgot = TextBox1.Text
                usr = CmdRead.GetString("userName")
                AdminForgot = CmdRead.GetString("email")
                usersID = CmdRead.GetInt32("userID")
            End While

            If Count = 1 Then
                OTPCode = GenerateOTP(6)
                SendOTP()
                Me.Hide()
                OTP.Show()
            Else
                MessageBox.Show("Username does not match email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub Authenticate()
        Dim scopes As String() = {GmailService.Scope.MailGoogleCom}

        Using stream As New FileStream(Credd, FileMode.Open, FileAccess.Read)
            Dim initializer As New BaseClientService.Initializer() With {
        .HttpClientInitializer = GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.FromStream(stream).Secrets,
            scopes,
            "user",
            CancellationToken.None,
            New FileDataStore("Gmail.API.Auth.Store")
        ).Result,
        .ApplicationName = "AMS Gmail"
    }

            service = New GmailService(initializer)
        End Using
    End Sub

    Public Sub SendEmail(ByVal senderEmail As String, ByVal senderName As String, ByVal recipient As String, ByVal subject As String, ByVal body As String)
        Try
            Dim emailContent As String = CreateEmail(senderEmail, senderName, recipient, subject, body)
            Dim emailMessage As Message = New Message With {
            .Raw = Base64UrlEncode(emailContent)
        }

            ' Use the service to send the email
            service.Users.Messages.Send(emailMessage, "me").Execute()

            MessageBox.Show($"Code sent successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error sending email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CreateEmail(ByVal senderEmail As String, ByVal senderName As String, ByVal recipient As String, ByVal subject As String, ByVal body As String) As String
        Dim emailTemplate As String = "From: {0} <{1}>" & vbCrLf & "To: {2}" & vbCrLf & "Subject: {3}" & vbCrLf & vbCrLf & "{4}"
        Return String.Format(emailTemplate, senderName, senderEmail, recipient, subject, body)
    End Function

    Private Function Base64UrlEncode(ByVal input As String) As String
        Dim inputBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(input)
        Return Convert.ToBase64String(inputBytes).Replace("+", "-").Replace("/", "_").Replace("=", "")
    End Function

    Public Sub SendOTP()
        Dim senderEmail As String = "lab.joshacc@gmail.com"
        Dim senderName As String = "Arthur's Place"
        Dim Subject As String = "Forgot Password OTP"
        Dim bods As String = "Your reset code is " & OTPCode & ""
        SendEmail(senderEmail, senderName, AdminForgot, Subject, bods)
    End Sub

    Private Sub VerifyEmail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Authenticate()
    End Sub
End Class