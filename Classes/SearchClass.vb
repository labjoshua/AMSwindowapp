Imports System.Security.Cryptography
Imports System.Windows.Controls
Imports MySql.Data.MySqlClient

Public Class SearchClass
    Public Sub SearchUser()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT userID'ID', userFullName'Name', userName'Username', 
            REPLACE(userPassword, SUBSTRING(userPassword, 1), REPEAT('•', LENGTH(userPassword))) AS 'Password', userAccountRole'Account Role' 
            FROM amsusers
            WHERE userFullName LIKE @search OR userName Like @search"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@search", "%" & Users.TextBox2.Text & "%")

            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Users.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Users.TextBox2.Clear()
                If Users.TextBox2.Text = "" Then
                    MDA.SelectCommand = Command
                    MDA.Fill(DT)
                End If
            Else
                BS.DataSource = DT
                Users.DataGridView1.DataSource = BS
            End If

            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Public Sub SearchGuest()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsguests.guestID'ID', amsguests.guestName'Guest Name', amsguests.guestContactInfo'Contact Info', guestEmail'Email', amsusers.userFullName'Encoded By', amsguests.EncodedDate'Encoded Date'
            FROM amsguests
            LEFT JOIN amsusers ON amsusers.userID = amsguests.userID
            WHERE amsguests.guestName LIKE @search"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@search", "%" & GuestForm.TextBox2.Text & "%")

            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                GuestForm.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If GuestForm.TextBox2.Text = "" Then
                    MDA.SelectCommand = Command
                    MDA.Fill(DT)
                End If
            Else
                BS.DataSource = DT
                GuestForm.DataGridView1.DataSource = BS
            End If

            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Public Sub AdvanceSearchReservation()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsreservation.reservationID AS ID, amsguests.guestName AS 'Guest Name', roominformation.roomName AS 'Room', amsusers.userFullName AS 'Encoded By', amsreservation.EncodedDate, amsreservation.reservationDateFrom AS 'Date of Stay', amsreservation.reservationDateTo AS 'Up To', amsreservation.reservationStatus AS 'Status'
        FROM amsreservation
        LEFT JOIN amsguests ON amsguests.guestID = amsreservation.guestID
        LEFT JOIN roominformation ON roominformation.roomID = amsreservation.roomID
        LEFT JOIN amsusers ON amsusers.userID = amsreservation.userID
        WHERE amsreservation.reservationStatus = 'Reserved' AND 
              roomName LIKE @roomname AND 
              reservationDateFrom <= @Date AND 
              reservationDateTo >= @Date"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@roomname", "%" & Reservation.ComboBox1.SelectedIndex.ToString() & "%")
            Command.Parameters.AddWithValue("@Date", Format(CDate(Reservation.DateTimePicker1.Text), "yyyy-MM-dd"))

            MDA.SelectCommand = Command
            MDA.Fill(DT)
            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Reservation.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                BS.DataSource = DT
                Reservation.DataGridView1.DataSource = BS
            End If

            MysqlConn.Close()

        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
        MysqlConn.Dispose()
        Command.Dispose()
    End Sub

    Public Sub DateSearchReservation()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsreservation.reservationID AS ID, amsguests.guestName AS 'Guest Name', roominformation.roomName AS 'Room', amsusers.userFullName AS 'Encoded By', amsreservation.EncodedDate, amsreservation.reservationDateFrom AS 'Date of Stay', amsreservation.reservationDateTo AS 'Up To', amsreservation.reservationStatus AS 'Status'
            FROM amsreservation
            LEFT JOIN amsguests ON amsguests.guestID = amsreservation.guestID
            LEFT JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            LEFT JOIN amsusers ON amsusers.userID = amsreservation.userID
            WHERE amsreservation.reservationStatus = 'Reserved' AND (@Date BETWEEN reservationDateFrom AND reservationDateTo)"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@Date", Format(CDate(Reservation.DateTimePicker1.Text), "yyyy-MM-dd"))

            MDA.SelectCommand = Command
            MDA.Fill(DT)
            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Reservation.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Reservation.DateTimePicker1.Text = "" Then
                    MDA.SelectCommand = Command
                    MDA.Fill(DT)
                End If
            Else
                BS.DataSource = DT
                Reservation.DataGridView1.DataSource = BS
            End If

            MysqlConn.Close()

        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
        MysqlConn.Dispose()
        Command.Dispose()

    End Sub

    Public Sub ComboBoxSearchReservation()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsreservation.reservationID AS ID, amsguests.guestName AS 'Guest Name', roominformation.roomName AS 'Room', amsusers.userFullName AS 'Encoded By', amsreservation.EncodedDate, amsreservation.reservationDateFrom AS 'Date of Stay', amsreservation.reservationDateTo AS 'Up To', amsreservation.reservationStatus AS 'Status'
            FROM amsreservation
            LEFT JOIN amsguests ON amsguests.guestID = amsreservation.guestID
            LEFT JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            LEFT JOIN amsusers ON amsusers.userID = amsreservation.userID
            WHERE amsreservation.reservationStatus = 'Reserved' AND roominformation.roomName LIKE @Combs"
            Command = New MySqlCommand(Query, MysqlConn)

            If Reservation.ComboBox1.SelectedItem = Nothing Then
                LoadThis.ReadReservation()
            Else
                Command.Parameters.AddWithValue("@Combs", "%" & Reservation.ComboBox1.SelectedItem.ToString() & "%")
                MDA.SelectCommand = Command
                MDA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    MysqlConn.Close()
                    Command.Dispose()
                    MysqlConn.Dispose()
                    BS.DataSource = DT
                    Reservation.DataGridView1.DataSource = BS
                    MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MDA.SelectCommand = Command
                    MDA.Fill(DT)
                Else
                    BS.DataSource = DT
                    Reservation.DataGridView1.DataSource = BS
                End If
            End If

            MysqlConn.Close()

        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
        MysqlConn.Dispose()
        Command.Dispose()
    End Sub

    Public Sub TextBoxSearchReservation()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsreservation.reservationID AS ID, amsguests.guestName AS 'Guest Name', roominformation.roomName AS 'Room', amsusers.userFullName AS 'Encoded By', amsreservation.EncodedDate, amsreservation.reservationDateFrom AS 'Date of Stay', amsreservation.reservationDateTo AS 'Up To', amsreservation.reservationStatus AS 'Status'
            FROM amsreservation
            LEFT JOIN amsguests ON amsguests.guestID = amsreservation.guestID
            LEFT JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            LEFT JOIN amsusers ON amsusers.userID = amsreservation.userID
            WHERE amsreservation.reservationStatus = 'Reserved' AND guestName LIKE @guest"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@guest", "%" & Reservation.TextBox2.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)
            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Reservation.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Reservation.TextBox2.Text = ""
                MDA.SelectCommand = Command
                MDA.Fill(DT)
            Else
                BS.DataSource = DT
                Reservation.DataGridView1.DataSource = BS
            End If

            MysqlConn.Close()

        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
        MysqlConn.Dispose()
        Command.Dispose()
    End Sub

    Public Sub FilterProductsComboBox(Combs As System.Windows.Forms.ComboBox)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Close()
            Query = "SELECT productID'ID', productName'Product Name', productPrice'Price', productcategory.productCategory
            FROM productsinformation
            JOIN productcategory On productcategory.productcategoryID = productsinformation.productcategoryID
            WHERE productcategory.productCategory LIKE @search"
            Command = New MySqlCommand(Query, MysqlConn)

            If String.IsNullOrEmpty(Combs.SelectedItem) Then
                LoadThis.ReadFoods(FoodInformation.DataGridView1)

            Else
                Command.Parameters.AddWithValue("@search", "%" & Combs.SelectedItem.ToString() & "%")

                MDA.SelectCommand = Command
                MDA.Fill(DT)
                BS.DataSource = DT
                FoodInformation.DataGridView1.DataSource = BS

                MysqlConn.Close()
            End If
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Public Sub SearchProductsTextBox(Tbx As System.Windows.Forms.TextBox, Tbl As System.Windows.Forms.DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Close()
            Query = "SELECT productID'ID', productName'Product Name', productPrice'Price'
            FROM productsinformation
            JOIN productcategory On productcategory.productcategoryID = productsinformation.productcategoryID
            WHERE productName LIKE @search"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@search", "%" & Tbx.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Tbl.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                AddUpdateOrder.TextBox1.Clear()
                MDA.SelectCommand = Command
                MDA.Fill(DT)
            Else
                BS.DataSource = DT
                Tbl.DataSource = BS
            End If

            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

            Else
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Public Sub SearchRoomTextBox()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable


        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT roomID'ID', roomName'Room Name', FORMAT(roomPrice, 2)'Room Price' FROM roominformation
            WHERE roomName LIKE @search"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@search", "%" & Rooms.TextBox1.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Rooms.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Rooms.TextBox1.Text = ""
                MDA.SelectCommand = Command
                MDA.Fill(DT)

            Else
                BS.DataSource = DT
                Rooms.DataGridView1.DataSource = BS
            End If
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub
    Public Sub Searchpackageandpromos()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT packageID'ID', packageName'Package Name', packageDescription'Package Description', 
            FORMAT(packagePrice, 2)'Price'
            FROM packageandpromos
            WHERE packageName LIKE @search"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@search", "%" & PackageandPromos.TextBox1.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                PackageandPromos.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                PackageandPromos.TextBox1.Text = ""
                MDA.SelectCommand = Command
                MDA.Fill(DT)


            Else
                BS.DataSource = DT
                PackageandPromos.DataGridView1.DataSource = BS
            End If
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub SearchEquipment(Grid As DataGridView, txtbox As System.Windows.Forms.TextBox)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT equipID'ID', equipName'Name of Equipment', FORMAT(equipPrice, 2)'Price'
            FROM equipments
            WHERE equipName LIKE @search"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@search", "%" & txtbox.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Grid.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtbox.Text = ""
                MDA.SelectCommand = Command
                MDA.Fill(DT)
            Else
                BS.DataSource = DT
                Grid.DataSource = BS
            End If
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub SearchCertificate(Grid As DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT certificateID'ID', certificateName'Name of Diver', certificateLicense'License No.', amsusers.userName'Encoded By'
            FROM divercertificate
            LEFT JOIN amsusers ON amsusers.userID = divercertificate.userID
            WHERE certificateName LIKE @search"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@search", "%" & Certificates.TextBox1.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Grid.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Certificates.TextBox1.Clear()
                MDA.SelectCommand = Command
                MDA.Fill(DT)
            Else
                BS.DataSource = DT
                Grid.DataSource = BS
            End If

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub SearchLogs()
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT productsinformation.productName'Product', logsss.dateRestock'Date', logsss.stockAdded'Qty', logsss.actionPerform'Action'
            FROM logsss
            JOIN inventory ON logsss.inventoryID = inventory.inventoryID
            JOIN productsinformation ON inventory.productID = productsinformation.productID
            WHERE productsinformation.productName LIKE @look"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@look", "%" & Logs.TextBox2.Text & "%")
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT

                Logs.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Logs.TextBox2.Clear()
                LoadThis.ReadLogs()
                MDA.SelectCommand = Command
                MDA.Fill(DT)
            Else
                BS.DataSource = DT
                Logs.DataGridView1.DataSource = BS
            End If

            BS.DataSource = DT
            Logs.DataGridView1.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub SearchLogsDTpicker()
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT productsinformation.productName'Product', logsss.dateRestock'Date', logsss.stockAdded'Qty', logsss.actionPerform'Action'
            FROM logsss
            JOIN inventory ON logsss.inventoryID = inventory.inventoryID
            JOIN productsinformation ON inventory.productID = productsinformation.productID
            WHERE dateRestock >= @fromdate and dateRestock <= @toDate"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@fromdate", "%" & Logs.TextBox2.Text & "%")
            Command.Parameters.AddWithValue("@formDate", Format(CDate(Logs.DateTimePicker1.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@toDate", Format(CDate(Logs.DateTimePicker2.Text), "yyyy-MM-dd"))
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Logs.DataGridView1.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Logs.TextBox2.Clear()
                Logs.DateTimePicker1.Value = DateTime.Now.Date
                Logs.DateTimePicker2.Value = DateTime.Now.Date
                LoadThis.ReadLogs()
                MDA.SelectCommand = Command
                MDA.Fill(DT)
            Else
                BS.DataSource = DT
                Logs.DataGridView1.DataSource = BS
            End If

            BS.DataSource = DT
            Logs.DataGridView1.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

End Class


