Public Class Consumed
    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        LoadThis.ReadConsumedProduct(DataGridView1, DateTimePicker1, DateTimePicker2)
    End Sub

    Private Sub Consumed_Load(sender As Object, e As EventArgs) Handles Me.Load
        IconButton1.Hide()
    End Sub
End Class