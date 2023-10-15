Public Class Certificates
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If AddUpdateCertificates.Label1.Text = "Update Certificate" Then
            AddUpdateCertificates.Close()
        End If
        AddUpdateCertificates.Label1.Text = "Add Certificate"
        AddUpdateCertificates.IconButton1.Hide()
        AddUpdateCertificates.ShowDialog()

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If AddUpdateCertificates.Label1.Text = "Add Certificate" Then
            AddUpdateCertificates.Close()
        End If
        AddUpdateCertificates.Label1.Text = "Update Certificate"
        AddUpdateCertificates.IconButton2.Hide()
        AddUpdateCertificates.ShowDialog()

        With AddUpdateCertificates
            .TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Name of Diver").Value.ToString
            .TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("License No.").Value.ToString
        End With
    End Sub

    Private Sub Certificates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.DivingCertificate(DataGridView1)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to delete " & DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Name of Diver").Value.ToString & "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialog = DialogResult.Yes Then
            Eraase.DeleteCertificate()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Look.SearchCertificate(DataGridView1)
    End Sub
End Class