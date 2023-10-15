Imports MySql.Data.MySqlClient

Public Class FoodInformation
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If AddUpdateFood.Label1.Text = "Update Food" Then
            AddUpdateFood.Close()
        End If

        AddUpdateFood.Label1.Text = "Add Food"
        AddUpdateFood.IconButton1.Hide()
        AddUpdateFood.ShowDialog()

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        If AddUpdateFood.Label1.Text = "Add Food" Then
            AddUpdateFood.Close()
        End If

        AddUpdateFood.Label1.Text = "Update Food"
        AddUpdateFood.IconButton2.Hide()
        AddUpdateFood.ShowDialog()

        With AddUpdateFood
            AddUpdateFood.TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Product Name").Value.ToString()
            AddUpdateFood.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Price").Value.ToString()
            AddUpdateFood.ComboBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Product Category").Value.ToString()
        End With


    End Sub

    Private Sub FoodInformation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.ReadFoods(DataGridView1)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to delete " & DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Product Name").Value.ToString & "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialog = DialogResult.Yes Then
            Eraase.DeleteFood()
        End If


    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Look.SearchProductsTextBox(TextBox2, DataGridView1)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
    End Sub
End Class