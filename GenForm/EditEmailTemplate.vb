Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class EditEmailTemplate
    Dim template As JObject
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If BtnName = "IconButton1" Then
            Try
                Dim ReadPath As String = File.ReadAllText(patthh)
                template = JObject.Parse(ReadPath)
                Dim content As String = TextBox1.Text
                template("body") = content
                File.WriteAllText(patthh, template.ToString())
                MessageBox.Show("Reservation Template update successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf BtnName = "IconButton2" Then
            Try
                Dim ReadPath As String = File.ReadAllText(patthh)
                template = JObject.Parse(ReadPath)
                Dim content As String = TextBox1.Text
                template("body") = content
                File.WriteAllText(patthh, template.ToString())
                MessageBox.Show("Update Template update successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf BtnName = "IconButton3" Then
            Try
                Dim ReadPath As String = File.ReadAllText(patthh)
                template = JObject.Parse(ReadPath)
                Dim content As String = TextBox1.Text
                template("body") = content
                File.WriteAllText(patthh, template.ToString())
                MessageBox.Show("Cancellation Template update successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
End Class