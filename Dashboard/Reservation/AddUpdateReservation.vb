Imports System.IO
Imports FontAwesome.Sharp
Imports Google.Apis.Auth.OAuth2
Imports System.Threading
Imports Google.Apis.Gmail.v1
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports Google.Apis.Gmail.v1.Data
Imports Newtonsoft.Json.Linq
Imports System.Windows.Controls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class AddUpdateReservation
    Private service As GmailService
    Private clickedButton As IconButton

    Sub New()
        InitializeComponent()
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
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Me.Close()
        TextBox2.Text = ""
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        With GuestForm
            .IconButton1.Hide()
            .IconButton2.Hide()
            .IconButton3.Hide()
            .IconButton5.Show()
            .ShowDialog()
        End With
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        With Rooms
            .IconButton1.Hide()
            .IconButton2.Hide()
            .IconButton3.Hide()
            .ShowDialog()
        End With
    End Sub

    Public Sub SendEmail(ByVal senderEmail As String, ByVal senderName As String, ByVal recipient As String, ByVal subject As String, ByVal body As String)
        Try
            Dim emailContent As String = CreateEmail(senderEmail, senderName, recipient, subject, body)
            Dim emailMessage As Message = New Message With {
                .Raw = Base64UrlEncode(emailContent)
            }

            ' Use the service to send the email
            service.Users.Messages.Send(emailMessage, "me").Execute()

            MessageBox.Show($"Email sent successfully from {senderName}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If clickedButton Is IconButton2 Then
                Insert.CreateReservation()
            ElseIf clickedButton Is IconButton1 Then
                UpdateFunction.UpdateReservation()
            ElseIf clickedButton Is IconButton5 Then
                UpdateOnlineRes()
            End If


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

    Private Function LoadEmailTemplate() As Object

        Using streamReader As New StreamReader(Rsvv)
            Dim templateContent As String = streamReader.ReadToEnd()
            Return JObject.Parse(templateContent)
        End Using
    End Function

    Private Function LoadUpdateEmailTemp() As Object
        Using streamReader As New StreamReader(Updtt)
            Dim template As String = streamReader.ReadToEnd()
            Return JObject.Parse(template)
        End Using
    End Function

    Private Function PopulateEmailBody(ByVal template As JObject) As String
        Dim subject As String = template("subject").ToString()
        Dim body As String = template("body").ToString()
        Dim customerName As String = TextBox2.Text
        body = body.Replace("[Client's Name]", customerName)
        body = body.Replace("[Room Name]", TextBox1.Text)
        body = body.Replace("[Check-In Date]", Format(CDate(DateTimePicker1.Text), "MMMM-dd-yyyy"))
        body = body.Replace("[Check-Out Date]", Format(CDate(DateTimePicker2.Text), "MMMM-dd-yyyy"))
        body = body.Replace("[Resort Contact Information]", "(+63)917-777-9341 / (+63)919-312-3938 / (+63)939-913-5718")
        body = body.Replace("[Resort Name]", "Arthur's Place Dive Resort")
        body = body.Replace("[Your Name]", Enconder)
        body = body.Replace("[Your Title]", "Resort Manager")
        body = body.Replace("[Resort Name]", "Arthur's Place Dive Resort")
        body = body.Replace("[Contact Information]", "(+63)917-777-9341, (+63)919-312-3938, (+63)939-913-5718")
        body = body.Replace("[Email]", AdminEmail)
        Return body
    End Function

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Try
            clickedButton = IconButton2
            Dim template As JObject = LoadEmailTemplate()
            Dim customerName As String = TextBox1.Text
            Dim emailbody As String = PopulateEmailBody(template)
            SendEmail(AdminEmail, "Arthur's Place", guestEmail, template("subject").ToString(), emailbody)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AddUpdateReservation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Authenticate()
        TextBox2.Enabled = True
        TextBox1.Enabled = True
        DateTimePicker1.Enabled = True
        DateTimePicker2.Enabled = True
        If Label1.Text = "Proceed to Check-In" Then
            TextBox2.Enabled = False
            TextBox1.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            IconButton1.Hide()
            IconButton2.Hide()
        ElseIf Label1.Text = "Update Reservation" Then
            TextBox2.Enabled = False
        ElseIf Label1.Text = "Proceed to Reservation" Then
            TextBox2.Enabled = False
            TextBox1.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            IconButton1.Hide()
            IconButton4.Hide()
        End If
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Try
            clickedButton = IconButton1
            Dim template As JObject = LoadUpdateEmailTemp()
            Dim customerName As String = TextBox1.Text
            Dim emailbody As String = PopulateEmailBody(template)
            SendEmail(AdminEmail, "Arthur's Place", guestEmail, template("subject").ToString(), emailbody)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Insert.CheckinReservation(Me)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        LoadThis.FetchClientEmail(TextBox2.Text)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Try
            clickedButton = IconButton5
            Dim template As JObject = LoadEmailTemplate()
            Dim customerName As String = TextBox1.Text
            Dim emailbody As String = PopulateEmailBody(template)
            SendEmail(AdminEmail, "Arthur's Place", guestEmail, template("subject").ToString(), emailbody)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub UpdateOnlineRes()
        Dim Query As String
        Dim Status As String = "Reserved"

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE amsreservation
                    SET reservationStatus = @stat,
                    EncodedDate = @date,
                    userID = @IDs
                    WHERE reservationID = @ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@stat", Status)
            Command.Parameters.AddWithValue("@date", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@IDs", AdminID)
            Command.Parameters.AddWithValue("@ID", OnlineTab.DataGridView1.Rows(OnlineTab.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Reservation Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            loadThisShiiit()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            loadThisShiiit()
        Finally
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub loadThisShiiit()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsreservation.reservationID'ID', amsguests.guestName'Guest Name', amsguests.guestContactInfo'Contact No.', roominformation.roomName'Room', amsreservation.reservationDateFrom'Date of Stay', amsreservation.reservationDateTo'Up To'
            FROM amsreservation
            LEFT JOIN amsguests ON amsguests.guestID = amsreservation.guestID
            LEFT JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            LEFT JOIN amsusers ON amsusers.userID = amsreservation.userID
            WHERE amsreservation.reservationStatus LIKE @stat"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@stat", "Online")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            OnlineTab.DataGridView1.DataSource = BS

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

End Class