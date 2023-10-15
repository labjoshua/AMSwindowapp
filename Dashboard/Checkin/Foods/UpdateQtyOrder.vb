Public Class UpdateQtyOrder
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Me.Close()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        UpdateFunction.UpdateOrderFood()
    End Sub

    Private Sub UpdateQtyOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
End Class