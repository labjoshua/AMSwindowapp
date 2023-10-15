Imports System.ComponentModel
Imports FontAwesome.Sharp
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports Google.Protobuf
Imports Org.BouncyCastle.Crypto.Tls

Public Class DashboardForm
    Private currentBtn As IconButton
    Private AutoInTime As TimeSpan
    Private WithEvents triggerTimer As New System.Windows.Forms.Timer()

    Private Sub ActivateButton(senderBtn As Object)
        If senderBtn IsNot Nothing Then
            Dim newBtn As IconButton = DirectCast(senderBtn, IconButton)

            If currentBtn IsNot Nothing AndAlso currentBtn IsNot newBtn Then
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText
                currentBtn.TextAlign = ContentAlignment.MiddleLeft
                currentBtn.BackColor = Color.FromArgb(0, 51, 102)
                currentBtn.ForeColor = Color.White
                currentBtn.IconColor = Color.White
            End If
            currentBtn = newBtn
            currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage
            currentBtn.TextAlign = ContentAlignment.MiddleCenter
            currentBtn.BackColor = Color.White
            currentBtn.ForeColor = Color.FromArgb(0, 51, 102)
            currentBtn.IconColor = Color.FromArgb(0, 51, 102)
        End If
    End Sub
    Private Sub IconButton10_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub IconButton8_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Start()

        Panel4.Controls.Clear()
        If AccountType = "User" Then
            IconButton3.Hide()
            IconButton4.Hide()
            IconButton6.Hide()
            IconButton7.Hide()
            Certificates.IconButton3.Hide()
            SubDashboard.IconButton1.Hide()
            SubDashboard.IconButton2.Hide()
            SubDashboard.IconButton4.Hide()
            SubDashboard.IconButton6.Hide()
            GuestOrders.IconButton2.Hide()
        End If
        'This is for the trigger of time
        '======== triggerTime is the holder of the time you set for trigger
        Dim triggerTime As TimeSpan = TimeSpan.Parse("22:01:00")

        Dim currentTime As TimeSpan = DateTime.Now.TimeOfDay
        Dim timeUntilTrigger As TimeSpan = triggerTime - currentTime

        If timeUntilTrigger.TotalMilliseconds < 0 Then
            timeUntilTrigger = TimeSpan.FromDays(1) - currentTime + triggerTime
        End If

        Timer2.Interval = CInt(timeUntilTrigger.TotalMilliseconds)
        Timer2.Start()
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Insert.CheckForCheckIn()
        Timer2.Stop()
    End Sub


    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        ActivateButton(sender)
        Panel4.Controls.Clear()
        Gen.ShowFormToParentContainer(SubDashboard, Panel4)
        Label6.Text = "Dashboard"
        GuestForm.IconButton5.Hide()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        ActivateButton(sender)
        Label6.Text = "Inventory"
        Panel4.Controls.Clear()
        Gen.ShowFormToParentContainer(Inventory, Panel4)
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        ActivateButton(sender)
        Label6.Text = "Products"
        Panel4.Controls.Clear()
        Gen.ShowFormToParentContainer(Products, Panel4)
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        ActivateButton(sender)
        Label6.Text = "Certificates"
        Panel4.Controls.Clear()
        Gen.ShowFormToParentContainer(Certificates, Panel4)
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        ActivateButton(sender)
        Label6.Text = "Users"
        Panel4.Controls.Clear()
        Gen.ShowFormToParentContainer(Users, Panel4)
    End Sub

    Private Sub IconButton7_Click(sender As Object, e As EventArgs)
        SubDashboard.Panel2.Controls.Clear()
        Panel4.Controls.Clear()
        Label6.Text = ""
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim currentTime As DateTime = DateTime.Now
        Dim currentDate As DateTime = Date.Now
        Dim TimeFormat As String = currentTime.ToString("hh:mm:ss tt")
        Dim FormattedDate As String = currentDate.ToString("yyyy-MM-dd")

        Label3.Text = FormattedDate
        Label4.Text = TimeFormat
    End Sub

    Private Sub IconButton7_Click_1(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub DashboardForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to close?", "Close App", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Me.Dispose()
            Loading.Dispose()
        End If
    End Sub

    Private Sub IconButton7_Click_2(sender As Object, e As EventArgs)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub IconButton7_Click_3(sender As Object, e As EventArgs) Handles IconButton7.Click
        Settings.Show()
    End Sub

    Private Sub IconButton8_Click_1(sender As Object, e As EventArgs)
    End Sub

    Private Sub IconButton8_Click_2(sender As Object, e As EventArgs)
    End Sub

    Private Sub IconButton8_Click_3(sender As Object, e As EventArgs)
    End Sub

    Private Sub IconButton8_Click_4(sender As Object, e As EventArgs) 
    End Sub
End Class