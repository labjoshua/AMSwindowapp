Public Class GuestOrders

    Private Shared Dgvt As String
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        AddUpdateOrder.Label1.Text = "Add Order"
        AddUpdateOrder.TextBox2.Enabled = False
        AddUpdateOrder.ShowDialog()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        AddOrderEquipment.ShowDialog()
    End Sub

    Private Sub DataGridView4_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView4.DoubleClick
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        CheckInList.ShowDialog()
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub GuestOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub DataGridView2_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView2.DoubleClick
        If AccountType = "Admin" Then
            UpdateQtyOrder.Label1.Text = "Update Qty: "
            UpdateQtyOrder.Label2.Text = DataGridView2.Rows(DataGridView2.CurrentCell.RowIndex).Cells("Order").Value.ToString
            UpdateQtyOrder.TextBox1.Text = DataGridView2.Rows(DataGridView2.CurrentCell.RowIndex).Cells("Qty").Value.ToString
            UpdateQtyOrder.Show()
        Else
            MessageBox.Show("You are prohibited to make updates!", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
    Private Sub ClearSelectionExcept(selectedDataGridView As DataGridView)
        For Each dgv As DataGridView In {DataGridView1, DataGridView2, DataGridView3, DataGridView4}
            If dgv IsNot selectedDataGridView Then
                dgv.ClearSelection()
            End If
        Next
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        ClearSelectionExcept(DataGridView1)
        Dgvt = "DataGridView1"
    End Sub

    Private Sub DataGridView2_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView2.SelectionChanged
        ClearSelectionExcept(DataGridView2)
        Dgvt = "DataGridView2"
    End Sub

    Private Sub DataGridView3_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView3.SelectionChanged
        ClearSelectionExcept(DataGridView3)
        Dgvt = "DataGridView3"
    End Sub

    Private Sub DataGridView4_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView4.SelectionChanged
        ClearSelectionExcept(DataGridView4)
        Dgvt = "DataGridView4"
    End Sub

    Private Sub IconButton2_Click_1(sender As Object, e As EventArgs) Handles IconButton2.Click
        If Dgvt = "DataGridView2" Then
            Dim dialog As DialogResult = MessageBox.Show("Do you want to delete this Order?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dialog = DialogResult.Yes Then
                Eraase.DeleteFoodsOrder()
            End If
        ElseIf Dgvt = "DataGridView1" Then
            Dim dialogg As DialogResult = MessageBox.Show("Do you want to delete this Equipment", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dialogg = DialogResult.Yes Then
                Eraase.DeleteEquipment()
            End If
        ElseIf Dgvt = "DataGridView4" Then
            Dim dialoogg As DialogResult = MessageBox.Show("Do you want to delete this Package", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dialoogg = DialogResult.Yes Then
                Eraase.DeletePackage()
            End If
        End If

    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        If AccountType = "Admin" Then
            UpdateEquipQty.Label1.Text = "Update Order: "
            UpdateEquipQty.Label2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Order").Value.ToString
            UpdateEquipQty.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Qty").Value.ToString
            UpdateEquipQty.ShowDialog()
        End If
    End Sub

    Private Sub IconButton4_Click_1(sender As Object, e As EventArgs) Handles IconButton4.Click
        AddPackage.Refresh()
        AddPackage.ShowDialog()
    End Sub
End Class