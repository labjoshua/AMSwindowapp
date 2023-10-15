Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class AddUpdateUsers

    Dim choice As String
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Me.Close()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Insert.CreateNewUser()
        LoadThis.ReadUsers(Users.DataGridView1)
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        UpdateFunction.UpdateUser()
        LoadThis.ReadUsers(Users.DataGridView1)
    End Sub
End Class