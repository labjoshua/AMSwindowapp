Imports System.ComponentModel

Public Class GuestForm


    Private Sub IconButton1_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        If AddUpdateGuest.Label1.Text = "Update Guest" Then
            AddUpdateGuest.Close()
        End If

        AddUpdateGuest.Label1.Text = "Add Guest"
        AddUpdateGuest.IconButton1.Hide()
        AddUpdateGuest.ShowDialog()
    End Sub

    Private Sub IconButton2_Click_1(sender As Object, e As EventArgs) Handles IconButton2.Click
        With AddUpdateGuest
            AddUpdateGuest.TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
            AddUpdateGuest.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Contact Info").Value.ToString
            AddUpdateGuest.TextBox3.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Email").Value.ToString
        End With

        If AddUpdateGuest.Label1.Text = "Add Guest" Then
            AddUpdateGuest.Close()
        End If

        AddUpdateGuest.Label1.Text = "Update Guest"
        AddUpdateGuest.IconButton2.Hide()
        AddUpdateGuest.ShowDialog()
    End Sub

    Private Sub GuestForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadThis.ReadGuest()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to delete " & DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString & "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialog = DialogResult.Yes Then
            Eraase.DeleteGuest()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Look.SearchGuest()
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If AddUpdateReservation.Label1.Text = "Add Reservation" Then
            AddUpdateReservation.TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs)

    End Sub

End Class