Imports System.IO
Imports System.Runtime.CompilerServices
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Gmail.v1
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports Newtonsoft.Json.Linq
Imports System.Threading
Imports Google.Apis.Gmail.v1.Data
Imports System.ComponentModel

Public Class LoginForm
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With ServerConfiguration
            Server.LoadConfiguration(ServerConfiguration.TextBox3, ServerConfiguration.TextBox1, ServerConfiguration.TextBox2, ServerConfiguration.TextBox4)
        End With

        With Pathconfig
            Server.LoadPathConfig(Pathconfig.TextBox1, Pathconfig.TextBox2, Pathconfig.TextBox3, Pathconfig.TextBox4, Pathconfig.TextBox5, Pathconfig.TextBox6)
        End With

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Dispose()
        Loading.Dispose()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Gen.Login()
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then : IconButton1_Click(sender, e)
        ElseIf e.KeyCode = Keys.F12 Then
            ServerConfiguration.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            IconButton1_Click(sender, e)
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.Close()
        VerifyEmail.Show()
    End Sub
End Class
