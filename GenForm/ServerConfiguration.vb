Public Class ServerConfiguration
    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Me.Close()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Server.TestConnection(TextBox3, TextBox1, TextBox2, TextBox4)
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Server.SaveConfiguration(Me, TextBox3, TextBox1, TextBox2, TextBox4)
        Loading.Dispose()
    End Sub
End Class