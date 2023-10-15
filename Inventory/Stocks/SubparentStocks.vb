Imports System.Windows.Controls
Imports FontAwesome.Sharp

Public Class SubparentStocks
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
        DashboardForm.Label6.Text = "Inventory - Food and Drinks Stocks"
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(Stocks, Panel2)
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Inventory - Equipment Stocks"
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(EquipStocks, Panel2)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        ActivateButton(sender)
        DashboardForm.Label6.Text = "Inventory - Logs"
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(Logs, Panel2)
        LoadThis.ReadLogs()
    End Sub
End Class