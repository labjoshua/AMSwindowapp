Imports FontAwesome.Sharp

Public Class Inventory
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
        DashboardForm.Label6.Text = "Inventory - Stocks"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(SubparentStocks, Panel2)
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Inventory - Consumed"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(Consumed, Panel2)
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs)
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Inventory - Expenses"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(Expenses, Panel2)
    End Sub

    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Inventory - Sales"
        Panel2.Controls.Clear()
        Gen.ChildInsideChildForm(Receipt, Panel2)
        Receipt.IconButton2.Hide()
    End Sub
End Class