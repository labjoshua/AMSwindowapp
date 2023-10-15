Imports System.ComponentModel
Imports System.Diagnostics.Eventing.Reader
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common

Public Class AddUpdateOrder
    Private initialcount As Integer


    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Me.Close()
    End Sub

    Private Sub AddUpdateOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox2.Text = GuestOrders.Label8.Text
        If Label1.Text = "Add Order" Then
            LoadThis.ReadFoods(DataGridView1)
        ElseIf Label1.Text = "Update Order" Then
            DataGridView2.Columns.Clear()
            DataGridView2.Rows.Clear()
            LoadThis.ReadOrders(DataGridView2)
            LoadThis.ReadFoods(DataGridView1)
            initialcount = DataGridView2.RowCount
        End If

        If TextBox2.Text = "Guest Name" Then
            MessageBox.Show("Select Guest First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End If

        AddHandler DataGridView1.CellDoubleClick, AddressOf DataGridView1_DoubleClick

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CloneByInserting(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim existingRow As DataGridViewRow = Nothing
            Dim Qty As Integer = 1 ' Set default value to 1
            Dim Total As Decimal

            ' Show an input dialog to enter the quantity
            Dim inputQty As String = InputBox("Enter the quantity:", "Quantity", "1")

            ' Validate and update the quantity value
            If Integer.TryParse(inputQty, Qty) Then
                ' Check if the selected row already exists in DataGridView2
                For Each row As DataGridViewRow In DataGridView2.Rows
                    If row.Cells(0).Value = selectedRow.Cells(1).Value AndAlso row.Cells(1).Value = selectedRow.Cells(2).Value Then
                        existingRow = row
                        Exit For
                    End If
                Next

                If existingRow IsNot Nothing Then
                    ' Increment the Qty of the existing row
                    Qty = Convert.ToInt32(existingRow.Cells(2).Value) + Qty

                    ' Calculate the updated Total based on Qty and column2 value
                    Dim column2Value As String = existingRow.Cells(1).Value.ToString()
                    Dim column2DecimalValue As Decimal

                    If Decimal.TryParse(column2Value, column2DecimalValue) Then
                        Total = column2DecimalValue * Qty

                        ' Update the Qty and Total in the existing row
                        existingRow.Cells(2).Value = Qty
                        existingRow.Cells(3).Value = Total.ToString("N2") ' Add comma separator to Total with 2 decimal places
                    End If
                Else
                    ' Create a new row
                    Dim newRow As DataGridViewRow = New DataGridViewRow()
                    newRow.CreateCells(DataGridView2)

                    ' Clone column2 and column3 to column1 and column2
                    newRow.Cells(0).Value = selectedRow.Cells(1).Value
                    newRow.Cells(1).Value = selectedRow.Cells(2).Value

                    ' Set the initial Qty and Total
                    newRow.Cells(2).Value = Qty
                    newRow.Cells(3).Value = (Convert.ToDecimal(selectedRow.Cells(2).Value) * Qty).ToString("N2") ' Calculate and add comma separator to Total with 2 decimal places

                    DataGridView2.Rows.Add(newRow)
                End If
            Else
                MessageBox.Show("Invalid quantity value. Please enter a numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub



    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If Label1.Text = "Add Order" Then
            CloneByInserting(sender, e)
        End If
    End Sub

    Private Sub AddUpdateOrder_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim rest As DialogResult = MessageBox.Show("Close Windows??", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If rest = DialogResult.No Then
            e.Cancel = True
        Else
            CheckAddOrder()
        End If

        TextBox1.Text = ""
    End Sub

    'This is for closing as well
    Private Sub CheckAddOrder()
        If DataGridView2.RowCount > 0 Then
            Dim q As DialogResult = MessageBox.Show("Do you want to save", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If q = DialogResult.Yes Then
                Insert.InsertGuestOrderProduct()
                Me.Close()
                DataGridView2.Rows.Clear()
                DataGridView2.Columns.Clear()
            ElseIf q = DialogResult.No Then
                DataGridView2.Rows.Clear()
                DataGridView2.Columns.Clear()
            End If
        End If
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Insert.InsertGuestOrderProduct()
        LoadThis.Threshold()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Look.SearchProductsTextBox(TextBox1, DataGridView1)
    End Sub

    Private Sub AddUpdateOrder_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub
End Class