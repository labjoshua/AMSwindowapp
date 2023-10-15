Imports MySql.Data.MySqlClient
Imports System.Windows
Imports System.Threading
Imports System.Windows.Controls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Loading
    Private WithEvents backgroundWorker As New System.ComponentModel.BackgroundWorker()

    Private Sub Loading_Load(sender As Object, e As EventArgs) Handles Me.Load
        backgroundWorker.WorkerReportsProgress = True
        backgroundWorker.RunWorkerAsync()

        With ServerConfiguration
            Server.LoadConfiguration(ServerConfiguration.TextBox3, ServerConfiguration.TextBox1, ServerConfiguration.TextBox2, ServerConfiguration.TextBox4)
        End With
        With Pathconfig
            Server.LoadPathConfig(Pathconfig.TextBox1, Pathconfig.TextBox2, Pathconfig.TextBox3, Pathconfig.TextBox4, Pathconfig.TextBox5, Pathconfig.TextBox6)
        End With
    End Sub
    Private Sub backgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles backgroundWorker.DoWork
        With ServerConfiguration
            Server.LoadConfiguration(ServerConfiguration.TextBox3, ServerConfiguration.TextBox1, ServerConfiguration.TextBox2, ServerConfiguration.TextBox4)
        End With

        With Pathconfig
            Server.LoadPathConfig(Pathconfig.TextBox1, Pathconfig.TextBox2, Pathconfig.TextBox3, Pathconfig.TextBox4, Pathconfig.TextBox5, Pathconfig.TextBox6)
        End With
        Thread.Sleep(3000)

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            ' Attempt to open the database connection
            MysqlConn.Open()

            ' If the connection is established, no need to wait
            If MysqlConn.State = ConnectionState.Open Then
                backgroundWorker.ReportProgress(100)
            End If
        Catch ex As MySqlException
            Me.Invoke(Sub() Me.Hide())
            MessageBox.Show(ex.Message)
            Me.Invoke(Sub() Settings.IconButton3.Hide())
            Me.Invoke(Sub() Settings.Show())
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub backgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles backgroundWorker.ProgressChanged
        ProgressBar2.Value = e.ProgressPercentage
    End Sub

    Private Sub backgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted
        If ProgressBar2.Value = 100 Then
            Me.Invoke(Sub() Me.Hide())
            Me.Invoke(Sub() LoginForm.Show())
        End If
    End Sub
End Class
