Imports MySql.Data.MySqlClient

Public Class OnlineTab
    Private Sub OnlineTab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReadReservationOnline()
    End Sub

    Public Sub ReadReservationOnline()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsreservation.reservationID'ID', amsguests.guestName'Guest Name', amsguests.guestContactInfo'Contact No.', roominformation.roomName'Room', amsreservation.reservationDateFrom'Date of Stay', amsreservation.reservationDateTo'Up To'
            FROM amsreservation
            LEFT JOIN amsguests ON amsguests.guestID = amsreservation.guestID
            LEFT JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            LEFT JOIN amsusers ON amsusers.userID = amsreservation.userID
            WHERE amsreservation.reservationStatus LIKE @stat"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@stat", "Online")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            DataGridView1.DataSource = BS

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM amsreservation WHERE reservationID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Reservation Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ReadReservationOnline()
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
                Command.Dispose()
                MessageBox.Show(ex.Message)
            End If
        End Try
        MysqlConn.Close()
        Command.Dispose()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        With AddUpdateReservation
            .TextBox2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString
            .TextBox1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Room").Value.ToString
            .DateTimePicker1.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Date of Stay").Value.ToString
            .DateTimePicker2.Text = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells("Up To").Value.ToString
            .Label1.Text = "Proceed to Reservation"
            .IconButton2.Hide()
            .IconButton1.Hide()
            .IconButton4.Hide()
            .ShowDialog()
        End With
    End Sub
End Class