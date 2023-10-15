Imports System.IO
Imports System.Runtime.CompilerServices
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Gmail.v1
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports Newtonsoft.Json.Linq
Imports System.Threading
Imports Google.Apis.Gmail.v1.Data

Public Class Reservation
    Private service As GmailService
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

            MessageBox.Show($"Email sent successfully from {senderName}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Eraase.DeleteReservation()

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

    Private Function LoadDeleteTemplate() As Object
        Using streamReader As New StreamReader(cnll)
            Dim templateContent As String = streamReader.ReadToEnd()
            Return JObject.Parse(templateContent)
        End Using
    End Function

    Private Function PopulateEmailBody(ByVal template As JObject) As String
        Dim subject As String = template("subject").ToString()
        Dim body As String = template("body").ToString()
        body = body.Replace("[Client's Name]", customerName)
        body = body.Replace("[Room Name]", roomName)
        body = body.Replace("[Check-In Date]", Date1)
        body = body.Replace("[Check-Out Date]", Date2)
        body = body.Replace("[Resort Contact Information]", "(+63)917-777-9341 / (+63)919-312-3938 / (+63)939-913-5718")
        body = body.Replace("[Resort Name]", "Arthur's Place Dive Resort")
        body = body.Replace("[Your Name]", Enconder)
        body = body.Replace("[Your Title]", "Resort Manager")
        body = body.Replace("[Resort Name]", "Arthur's Place Dive Resort")
        body = body.Replace("[Contact Information]", "(+63)917-777-9341, (+63)919-312-3938, (+63)939-913-5718")
        body = body.Replace("[Email]", AdminEmail)
        Return body
    End Function

    Private Sub IconButton1_Click(sender As Object, e As EventArgs)
        If AddUpdateGuest.Label1.Text = "Update Reservation" Then
            AddUpdateReservation.Close()
        End If
        AddUpdateReservation.Label1.Text = "Add Reservation"
        AddUpdateReservation.IconButton1.Hide()
        AddUpdateReservation.ShowDialog()

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs)
        If AddUpdateReservation.Label1.Text = "Add Reservation" Then
            AddUpdateReservation.Close()
        End If
        AddUpdateReservation.Label1.Text = "Update Reservation"
        AddUpdateReservation.IconButton2.Hide()
        AddUpdateReservation.ShowDialog()

    End Sub

    Private Sub Reservation_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadThis.ReadReservation()
        LoadThis.ReadRoomSearchComboBox()
        Authenticate()
    End Sub

    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        With AddUpdateReservation
            .Label1.Text = "Add Reservation"
            .IconButton1.Hide()
            .IconButton2.Show()
            .IconButton4.Hide()
            .ShowDialog()
        End With
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Look.ComboBoxSearchReservation()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Look.DateSearchReservation()
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        DateTimePicker1.Value = DateTime.Now.Date
        ComboBox1.SelectedIndex = -1
        LoadThis.ReadReservation()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Look.AdvanceSearchReservation()
        Else
            IconButton4.PerformClick()
        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Look.TextBoxSearchReservation()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim result As DialogResult
        result = MessageBox.Show("Do you want to delete this reservation?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Try
                Dim template As JObject = LoadDeleteTemplate()
                Dim customerName As String = customerName
                Dim emailbody As String = PopulateEmailBody(template)
                SendEmail(AdminEmail, "Arthur's Place", guestEmailAdd, template("subject").ToString(), emailbody)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub IconButton2_Click_1(sender As Object, e As EventArgs) Handles IconButton2.Click
        With AddUpdateReservation
            .Label1.Text = "Update Reservation"
            .IconButton2.Hide()
            .IconButton1.Show()
            .IconButton4.Hide()
            .TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
            .TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Room").Value.ToString
            .DateTimePicker1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Date of Stay").Value.ToString
            .DateTimePicker2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Up To").Value.ToString
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        With AddUpdateReservation
            .Label1.Text = "Proceed to Check-In"
            .TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
            .TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Room").Value.ToString
            .DateTimePicker1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Date of Stay").Value.ToString
            .DateTimePicker2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Up To").Value.ToString
            .IconButton1.Hide()
            .IconButton2.Hide()
            .IconButton4.Show()
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        LoadThis.FetchroomandDate()
        LoadThis.FetchEmailOnce()
    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs) Handles IconButton8.Click
        Insert.CheckForCheckIn()
    End Sub
End Class