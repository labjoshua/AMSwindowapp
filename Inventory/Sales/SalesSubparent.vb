Imports System.Windows.Controls
Imports FontAwesome.Sharp

Public Class SalesSubparent
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
        Choose = "From Sales"
        ActivateButton(sender)
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(SalesReport, Panel2)
        DashboardForm.Label6.Text = "Inventory - Receipt"
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        ActivateButton(sender)
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(Refund, Panel2)
        DashboardForm.Label6.Text = "Inventory - Refund"
    End Sub

    Private Sub SalesSubparent_Load(sender As Object, e As EventArgs) Handles Me.Load
        IconButton2.Hide()
    End Sub
End Class