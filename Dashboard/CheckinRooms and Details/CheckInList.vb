Public Class CheckInList
    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        Me.Close()
    End Sub

    Private Sub CheckInList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadThis.ReadCheckIN(DataGridView1)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        GuestOrders.Label8.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
        BillOut.TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
        Me.DialogResult = Windows.Forms.DialogResult.OK

        LoadThis.ReadOrders(GuestOrders.DataGridView2)
        LoadThis.ReadRoomCheckIn()
        LoadThis.ReadRoomInformation()
        LoadThis.CheckInPackage()
        LoadThis.ReadOrdersEquipments()

        With GuestOrders
            .DataGridView1.ClearSelection()
            .DataGridView2.ClearSelection()
            .DataGridView3.ClearSelection()
            .DataGridView4.ClearSelection()
        End With
    End Sub
End Class