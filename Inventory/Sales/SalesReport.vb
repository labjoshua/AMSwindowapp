

Imports System.ComponentModel

Public Class SalesReport

    Public Sub InitializeServices()
        ReportViewer1.RefreshReport()
    End Sub
    Private Sub SalesReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ReportViewer1.RefreshReport()
        InitializeServices()
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        Receipt.ShowDialog()
    End Sub

    Private Sub ReportViewer1_ReportRefresh(sender As Object, e As CancelEventArgs) Handles ReportViewer1.ReportRefresh
        Prnt.PrintOrderFoods(Receipt.DataGridView1)
    End Sub
End Class