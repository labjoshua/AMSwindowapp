Imports System.ComponentModel
Imports Google.Apis.Auth.OAuth2
Imports System.IO
Imports System.Threading
Imports Google.Apis.Drive.v3
Imports Google.Apis.Gmail.v1
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Gmail.v1.Data
Imports System.Windows.Controls

Public Class BillOut

    Private service As GmailService
    Private _driveService As DriveService

    Public Sub InitializeServices()
        ReportViewer1.RefreshReport()
        AuthenticateGmail()
        _driveService = AuthenticateGDrive()
    End Sub

    Public Sub AuthenticateGmail()
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

    Private Sub SendEmailWithDriveLink(recipient As String, subject As String, body As String, pdfFileContent As Byte())
        Try
            Dim driveFileId As String = UploadFileToDrive(pdfFileContent, "Sales Invoice.pdf")
            Dim driveLink As String = GetPublicDriveLink(driveFileId)

            ' Construct the email body with the Drive link
            Dim emailBodyWithLink As String = $"{body}{driveLink}"

            ' Send the email using Gmail API
            SendEmailWithAttachment("lab.joshacc@gmail.com", "Arthur's Place", recipient, subject, emailBodyWithLink, "Sales Invoice.pdf", pdfFileContent)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function UploadFileToDrive(content As Byte(), fileName As String) As String
        Dim fileMetadata As New Google.Apis.Drive.v3.Data.File() With {
            .Name = fileName
        }

        Using stream As New MemoryStream(content)
            Dim request As FilesResource.CreateMediaUpload = _driveService.Files.Create(fileMetadata, stream, "application/pdf")
            request.Fields = "id"
            request.Upload()
            Return request.ResponseBody.Id
        End Using
    End Function

    Private Function GetPublicDriveLink(fileId As String) As String
        ' Update the permission of the file to make it publicly accessible
        Dim permission As New Permission() With {
            .Type = "anyone",
            .Role = "reader"
        }

        _driveService.Permissions.Create(permission, fileId).Execute()

        ' Construct the publicly accessible Drive link
        Return $"https://drive.google.com/uc?id={fileId}"
    End Function

    Public Sub SendEmailWithAttachment(ByVal senderEmail As String, ByVal senderName As String, ByVal recipient As String, ByVal subject As String, ByVal body As String, ByVal attachmentFileName As String, ByVal attachmentContent As Byte())
        Try
            Dim emailContent As String = CreateEmail(senderEmail, senderName, recipient, subject, body)
            Dim emailMessage As Message = New Message With {
                .Raw = Base64UrlEncode(emailContent)
            }

            ' Use the service to send the email
            Dim message = service.Users.Messages.Send(emailMessage, "me").Execute()

            MessageBox.Show($"Email sent successfully from {senderName}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub RenderAndSendReport()
        ' Render the report in the Report Viewer on the UI thread
        Me.Invoke(Sub()
                      Prnt.PrintOrderFoods(CheckInList.DataGridView1)
                  End Sub)

        ' After rendering is done, continue with email sending process
        Try
            Dim reportContent As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Dim recipient As String = guestEmail
            Dim subject As String = "Receipt Invoice Attached"
            Dim emailBody As String = "The Sales Invoice of the services rendered is attached below."

            SendEmailWithDriveLink(recipient, subject, emailBody, reportContent)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function AuthenticateGDrive() As DriveService

        Using stream As New FileStream(Credd, FileMode.Open, FileAccess.Read)
            Dim initializer As New BaseClientService.Initializer() With {
                .HttpClientInitializer = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    {DriveService.Scope.DriveFile},
                    "user",
                    CancellationToken.None,
                    New FileDataStore("Drive.Auth.Store")
                ).Result,
                .ApplicationName = "AMS Gmail"
            }

            Return New DriveService(initializer)
        End Using
    End Function
    Private Sub BillOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ReportViewer1.RefreshReport()
        InitializeServices()
    End Sub

    Private Sub ReportViewer1_ReportRefresh(sender As Object, e As CancelEventArgs) Handles ReportViewer1.ReportRefresh
        Prnt.PrintOrderFoods(CheckInList.DataGridView1)
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim rest As DialogResult = MessageBox.Show("Do you want to email the receipt?", "Email", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If rest = DialogResult.Yes Then
            RenderAndSendReport()
            UpdateFunction.UpdateToCheckOut()
            Insert.OverallTotal()
        ElseIf rest = DialogResult.No Then
            UpdateFunction.UpdateToCheckOut()
            Insert.OverallTotal()
        End If

        TextBox1.Text = ""
        ReportViewer1.Refresh()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        LoadThis.FetchClientEmail(TextBox1.Text)
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        CheckInList.ShowDialog()
    End Sub
End Class