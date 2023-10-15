Imports System.Windows.Controls

Public Class Rooms


    Private Sub IconButton1_Click(sender As Object, e As EventArgs)
        If AddUpdateRooms.Label4.Text = "Update Rooms" Then
            AddUpdateRooms.Close()
        End If

        AddUpdateRooms.Label4.Text = "Add Rooms"
        AddUpdateRooms.IconButton1.Hide()
        AddUpdateRooms.ShowDialog()

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs)
        If AddUpdateRooms.Label4.Text = "Add Rooms" Then
            AddUpdateRooms.Close()
        End If

        AddUpdateRooms.Label4.Text = "Update Rooms"
        AddUpdateRooms.IconButton2.Hide()
        AddUpdateRooms.ShowDialog()

    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Me.Close()
    End Sub

    Private Sub Rooms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.ReadRoomInformation()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        AddUpdateReservation.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Room Name").Value.ToString
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub IconButton1_Click_1(sender As Object, e As EventArgs) Handles IconButton1.Click
        If AddUpdateRooms.Label4.Text = "Update Rooms" Then
            AddUpdateRooms.Close()
        End If

        AddUpdateRooms.Label4.Text = "Add Rooms"
        AddUpdateRooms.IconButton1.Hide()
        AddUpdateRooms.ShowDialog()
    End Sub

    Private Sub IconButton2_Click_1(sender As Object, e As EventArgs) Handles IconButton2.Click
        If AddUpdateRooms.Label4.Text = "Add Rooms" Then
            AddUpdateRooms.Close()
        End If

        AddUpdateRooms.Label4.Text = "Update Rooms"
        AddUpdateRooms.IconButton2.Hide()
        AddUpdateRooms.ShowDialog()

        With AddUpdateRooms
            .TextBox3.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Room Name").Value.ToString
            .roomPrice.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Room Price").Value.ToString
        End With
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to delete " & DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Room Name").Value.ToString & "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialog = DialogResult.Yes Then
            Eraase.DeleteRoom()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Look.SearchRoomTextBox()
    End Sub
End Class