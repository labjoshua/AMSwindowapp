Public Class EquipStocks
    Private Sub EquipStocks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.EquipmentInventory(DataGridView1)
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        AddUpdateStocks.Label1.Text = "Add Equipment Stocks"
        AddUpdateStocks.Label2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Item").Value.ToString
        AddUpdateStocks.ShowDialog()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        AddUpdateStocks.Label1.Text = "Update Equipment Stocks"
        AddUpdateStocks.Label2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Item").Value.ToString
        AddUpdateStocks.ShowDialog()
    End Sub
End Class