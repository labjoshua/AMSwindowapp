Imports System.Runtime.InteropServices

Public Class Users

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If AddUpdateUsers.Label1.Text = "Update Account" Then
            AddUpdateUsers.Close()
        End If

        AddUpdateUsers.Label1.Text = "Add Account"
        AddUpdateUsers.IconButton1.Hide()
        AddUpdateUsers.ShowDialog()


    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If AddUpdateUsers.Label1.Text = "Add Account" Then
            AddUpdateUsers.Close()
        End If

        AddUpdateUsers.Label1.Text = "Update Account"
        AddUpdateUsers.IconButton2.Hide()
        AddUpdateUsers.ShowDialog()

        With AddUpdateUsers
            AddUpdateUsers.TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Name").Value.ToString
            AddUpdateUsers.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Username").Value.ToString
            AddUpdateUsers.TextBox3.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Password").Value.ToString
            AddUpdateUsers.ComboBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Account Role").Value.ToString
            AddUpdateUsers.TextBox4.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Email").Value.ToString
        End With
    End Sub

    Private Sub Users_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadThis.ReadUsers(DataGridView1)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim dialog As DialogResult = MessageBox.Show("Do you want to delete this user?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If dialog = DialogResult.Yes Then
            Eraase.DeleteUser()
            LoadThis.ReadUsers(DataGridView1)
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Look.SearchUser()
    End Sub
End Class