Public Class AddUpdateStocks
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Me.Close()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If Label1.Text = "Add Stocks" Then
            UpdateFunction.AddStocks()
            Me.Close()
            TextBox1.Text = ""
        ElseIf Label1.Text = "Update Stocks" Then
            UpdateFunction.MinusStocks()
            Me.Close()
            TextBox1.Text = ""
        ElseIf Label1.Text = "Add Equipment Stocks" Then
            UpdateFunction.AddEquipmentStocks()
            Me.Close()
            TextBox1.Text = ""
        ElseIf Label1.Text = "Update Equipment Stocks" Then
            UpdateFunction.UpdateEquipmentStock()
            Me.Close()
            TextBox1.Text = ""
        ElseIf Label1.Text = "Edit Threshold" Then
            UpdateFunction.Updatethreshold()
            Me.Close()
            TextBox1.Text = ""
        End If

    End Sub
End Class