Imports FontAwesome.Sharp

Public Class Products
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
        DashboardForm.Label6.Text = "Products - Foods"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(FoodInformation, Panel2)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Products - Rooms"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(Rooms, Panel2)
        Rooms.IconButton5.Hide()

    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Products - Package and Promos"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(PackageandPromos, Panel2)
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Products - Equipments"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(Equipment, Panel2)
    End Sub
End Class