Public Class UpdateEquipQty
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Me.Close()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        UpdateFunction.UpdateOrderEquipment()
    End Sub
End Class