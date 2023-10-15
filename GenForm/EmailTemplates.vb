Imports System.Windows.Controls
Imports FontAwesome.Sharp
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class EmailTemplates
    Private currentBtn As IconButton
    Dim template As JObject
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
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(EditEmailTemplate, Panel2)
        BtnName = "IconButton1"
        Button = Me.IconButton1
        patthh = Rsvv
        Try
            Dim ReadPath As String = File.ReadAllText(patthh)
            template = JObject.Parse(ReadPath)

            EditEmailTemplate.TextBox1.Text = template("body").ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        ActivateButton(sender)
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(EditEmailTemplate, Panel2)
        BtnName = "IconButton2"
        Button = Me.IconButton2
        patthh = Updtt
        Try
            Dim ReadPath As String = File.ReadAllText(patthh)
            template = JObject.Parse(ReadPath)

            EditEmailTemplate.TextBox1.Text = template("body").ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        ActivateButton(sender)
        Panel2.Controls.Clear()
        Gen.ShowFormToParentContainer(EditEmailTemplate, Panel2)
        BtnName = "IconButton3"
        Button = Me.IconButton3
        patthh = cnll
        Try
            Dim ReadPath As String = File.ReadAllText(patthh)
            template = JObject.Parse(ReadPath)

            EditEmailTemplate.TextBox1.Text = template("body").ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class