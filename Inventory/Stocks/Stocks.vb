Imports System.Windows.Controls

Public Class Stocks
    Private Sub Stocks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.ReadInventory(DataGridView1)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        AddUpdateStocks.Label1.Text = "Add Stocks"
        AddUpdateStocks.Label2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Product Name").Value.ToString
        AddUpdateStocks.ShowDialog()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        AddUpdateStocks.Label1.Text = "Update Stocks"
        AddUpdateStocks.Label2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Product Name").Value.ToString
        AddUpdateStocks.ShowDialog()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        AddUpdateStocks.Label1.Text = "Edit Threshold"
        AddUpdateStocks.Label2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Product Name").Value.ToString
        AddUpdateStocks.ShowDialog()
    End Sub
End Class