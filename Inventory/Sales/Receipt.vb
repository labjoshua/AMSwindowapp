Public Class Receipt
    Private Sub Receipt_Load(sender As Object, e As EventArgs) Handles Me.Load
        Salesss.ReadCheckOut(DataGridView1)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        SalesReport.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
        BillOut.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Salesss.SearchCheckOut(DataGridView1)
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Me.Dispose()
    End Sub
End Class