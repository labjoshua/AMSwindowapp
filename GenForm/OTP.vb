Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Controls
Imports MySql.Data.MySqlClient

Public Class OTP
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        If TextBox1.Text = OTPCode Then
            Me.Hide()
            ResetPassword.Show()
        Else
            MessageBox.Show("Code did not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub OTP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
    End Sub
End Class