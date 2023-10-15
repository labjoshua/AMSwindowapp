Imports MySql.Data.MySqlClient

Public Class Sales
    Public Sub ReadCheckOut(GRD As DataGridView)
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT checkin.checkinID'ID', amsguests.guestName'Guest Name',roominformation.roomName'Room Name', 
            amsreservation.reservationDateFrom'From', amsreservation.reservationDateTo'Up-To', FORMAT(overTotal, 2)'Total'
            FROM checkin
            JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            JOIN roominformation ON amsreservation.roomID = roominformation.roomID
            WHERE checkinStat= @stat"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@stat", "Check-Out")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            GRD.DataSource = BS

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub SearchCheckOut(GRD As DataGridView)
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT checkin.checkinID'ID', amsguests.guestName'Guest Name',roominformation.roomName'Room Name', 
            amsreservation.reservationDateFrom'From', amsreservation.reservationDateTo'Up-To', FORMAT(overTotal, 2)'Total'
            FROM checkin
            JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            JOIN roominformation ON amsreservation.roomID = roominformation.roomID
            WHERE checkinStat= @stat AND amsguests.guestName LIKE @sta"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@stat", "Check-Out")
            Command.Parameters.AddWithValue("@sta", "%" & Receipt.TextBox2.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                GRD.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Receipt.TextBox2.Clear()
                If Receipt.TextBox2.Text = "" Then
                    MDA.SelectCommand = Command
                    MDA.Fill(DT)
                End If
            Else
                BS.DataSource = DT
                GRD.DataSource = BS
            End If

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub
End Class
