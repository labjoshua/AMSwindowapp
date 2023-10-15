Public Class Pathconfig
    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        OpenFileDialog1.Filter = "JSON | *.json"
        OpenFileDialog1.FileName = ""

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        OpenFileDialog1.Filter = "JSON | *.json"
        OpenFileDialog1.FileName = ""

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox2.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        OpenFileDialog1.Filter = "JSON | *.json"
        OpenFileDialog1.FileName = ""

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox3.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        OpenFileDialog1.Filter = "JSON | *.json"
        OpenFileDialog1.FileName = ""

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox4.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        OpenFileDialog1.Filter = "Report | *.rdlc"
        OpenFileDialog1.FileName = ""

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox5.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Server.SavePathConfig(Me, TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6)
    End Sub
End Class