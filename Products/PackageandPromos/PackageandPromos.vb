Public Class PackageandPromos
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If AddUpdatePackageandPromos.Label1.Text = "Update Package" Then
            AddUpdatePackageandPromos.Close()
        End If
        AddUpdatePackageandPromos.Label1.Text = "Add Package"
        AddUpdatePackageandPromos.IconButton1.Hide()
        AddUpdatePackageandPromos.ShowDialog()

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If AddUpdatePackageandPromos.Label1.Text = "Add Package" Then
            AddUpdatePackageandPromos.Close()
        End If
        AddUpdatePackageandPromos.Label1.Text = "Update Package"
        AddUpdatePackageandPromos.IconButton2.Hide()
        AddUpdatePackageandPromos.ShowDialog()

        With AddUpdatePackageandPromos
            AddUpdatePackageandPromos.TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Package Name").Value.ToString
            AddUpdatePackageandPromos.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Price").Value.ToString
            AddUpdatePackageandPromos.TextBox3.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Package Description").Value.ToString
        End With
    End Sub

    Private Sub PackageandPromos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.ReadPackageandpromos(DataGridView1)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Look.Searchpackageandpromos()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to delete " & DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Package Name").Value.ToString & "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialog = DialogResult.Yes Then
            Eraase.DeletePackageandPromos()
        End If

    End Sub
End Class