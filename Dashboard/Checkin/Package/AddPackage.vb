Imports System.ComponentModel
Imports System.Diagnostics.Eventing.Reader
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common
Imports Mysqlx.XDevAPI.Relational
Public Class AddPackage

    ' Initialize a variable to store the value to increment
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Me.Close()
    End Sub

    Private Sub AddPackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = GuestOrders.Label8.Text
        LoadThis.ReadPackageandpromos(DataGridView1)
        TextBox2.Enabled = False

        If TextBox2.Text = "Guest Name" Then
            MessageBox.Show("Select Guest First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End If
        AddHandler DataGridView1.CellDoubleClick, AddressOf DataGridView1_DoubleClick
    End Sub

    Private Sub replicaterow(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim valueToIncrement As Integer = 1 ' Default value to increment
            Dim rowIndexToRemove As Integer = -1 ' Initialize with an invalid index

            ' Check if the selected cell already exists in DataGridView2
            For Each row As DataGridViewRow In DataGridView2.Rows
                If row.Cells(0).Value IsNot Nothing AndAlso row.Cells(0).Value.Equals(selectedRow.Cells(0).Value) Then
                    rowIndexToRemove = row.Index ' Store the index of the row to remove
                    valueToIncrement = CInt(row.Cells(4).Value) / CInt(selectedRow.Cells(3).Value) + 1
                    ' Calculate and set the value in DataGridView2, index 3, as the product of index 3 in DataGridView1 and the value of valueToIncrement
                    row.Cells(3).Value = valueToIncrement
                    Dim house = CInt(selectedRow.Cells(3).Value) * valueToIncrement
                    row.Cells(4).Value = house.ToString("N2")
                End If
            Next

            ' Remove the existing row if found
            If rowIndexToRemove >= 0 Then
                DataGridView2.Rows.RemoveAt(rowIndexToRemove)
            End If

            ' Add the updated row to DataGridView2
            Dim newRow As DataGridViewRow = New DataGridViewRow()
            newRow.CreateCells(DataGridView2)

            newRow.Cells(0).Value = selectedRow.Cells(0).Value
            newRow.Cells(1).Value = selectedRow.Cells(1).Value
            newRow.Cells(2).Value = selectedRow.Cells(2).Value
            newRow.Cells(3).Value = valueToIncrement
            ' Set the value in DataGridView2, index 3, as the product of index 3 in DataGridView1 and the value of valueToIncrement
            Dim total = CInt(selectedRow.Cells(3).Value) * valueToIncrement
            newRow.Cells(4).Value = total.ToString("N2")

            DataGridView2.Rows.Add(newRow)
        End If
    End Sub
    Private Sub DataGridView1_DoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        replicaterow(sender, e)
    End Sub

    Private Sub AddPackage_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim rest As DialogResult = MessageBox.Show("Close Windows??", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If rest = DialogResult.No Then
            e.Cancel = True
        End If
        DataGridView2.Rows.Clear()
    End Sub


    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Insert.OrderPackageandPromos()
    End Sub

    Private Sub AddPackage_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub
End Class