Imports System.Windows.Controls
Imports FontAwesome.Sharp

Public Class SubDashboard
    Private currentBtn As IconButton



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
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Dashboard - Guest"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(GuestForm, Panel2)
        GuestForm.IconButton1.Show()
        GuestForm.IconButton2.Show()
        GuestForm.IconButton3.Show()
        GuestForm.IconButton5.Hide()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Dashboard - Reservation"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(Reservation, Panel2)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Dashboard - Check In"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(GuestOrders, Panel2)
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Dashboard - Bill Out"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(BillOut, Panel2)
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Choose = "From Checkin"
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Dashboard - Bill Out"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(CheckInList, Panel2)
        CheckInList.IconButton5.Hide()
    End Sub

    Private Sub SubDashboard_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Dashboard - Online"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(OnlineTab, Panel2)
        CheckInList.IconButton5.Hide()
    End Sub
End Class