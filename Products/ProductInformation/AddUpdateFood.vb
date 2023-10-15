Public Class AddUpdateFood
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Me.Close()
    End Sub

    Private Sub AddUpdateFood_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.ProductCategory(ComboBox1)
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Insert.CreateFoods(ComboBox1)
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        UpdateFunction.UpdateFood()
    End Sub
End Class