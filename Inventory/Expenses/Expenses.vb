Public Class Expenses
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        'Add
        If AddUpdateExpenses.Label5.Text = "Update Expenses" Then
            AddUpdateExpenses.Close()
        End If

        AddUpdateExpenses.Label5.Text = "Add Expenses"
        AddUpdateExpenses.IconButton1.Hide()
        AddUpdateExpenses.ShowDialog()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        'Update
        If AddUpdateExpenses.Label5.Text = "Add Expenses" Then
            AddUpdateExpenses.Close()
        End If

        AddUpdateExpenses.Label5.Text = "Update Expenses"
        AddUpdateExpenses.IconButton1.Hide()
        AddUpdateExpenses.ShowDialog()
    End Sub
End Class