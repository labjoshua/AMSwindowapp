Public Class Logs
    Private Sub Logs_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadThis.ReadLogs()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Look.SearchLogs()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Look.SearchLogsDTpicker()
    End Sub
End Class