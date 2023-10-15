Public Class Equipment
    Private Sub Equipment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.ReadEquipments(DataGridView1)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Look.SearchEquipment(DataGridView1, TextBox2)
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If AddUpdateEquipment.Label1.Text = "Update Equipment" Then
            AddUpdateEquipment.Close()
        End If

        AddUpdateEquipment.Label1.Text = "Add Equipment"
        AddUpdateEquipment.IconButton1.Hide()
        AddUpdateEquipment.ShowDialog()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If AddUpdateEquipment.Label1.Text = "Add Equipment" Then
            AddUpdateEquipment.Close()
        End If

        AddUpdateEquipment.Label1.Text = "Update Equipment"
        AddUpdateEquipment.IconButton2.Hide()
        AddUpdateEquipment.ShowDialog()

        With AddUpdateEquipment
            .TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Name of Equipment").Value.ToString
            .TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Price").Value.ToString
        End With
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to delete " & DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Name of Equipment").Value.ToString & "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialog = DialogResult.Yes Then
            Eraase.DeleteEquipments()
        End If
    End Sub
End Class