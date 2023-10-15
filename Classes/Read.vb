Imports System.Globalization
Imports System.IO.Packaging
Imports System.Security.Cryptography
Imports System.Windows.Controls
Imports MySql.Data.MySqlClient

Public Class Read
    Public Sub ReadUsers(Grid As DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT userID'ID', userFullName'Name', userName'Username', 
            REPLACE(userPassword, SUBSTRING(userPassword, 1), REPEAT('•', LENGTH(userPassword))) AS 'Password', 
            userAccountRole'Account Role', email'Email'
            FROM amsusers"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Grid.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub ReadGuest()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsguests.guestID'ID',amsguests.guestName'Guest Name',amsguests.guestContactInfo'Contact Info', guestEmail'Email', amsusers.userFullName'Encoded By', amsguests.EncodedDate'Encoded Date'
                FROM amsguests
                LEFT JOIN amsusers ON amsusers.userID = amsguests.userID"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            GuestForm.DataGridView1.DataSource = BS

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub ReadReservation()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT amsreservation.reservationID AS 'ID', 
       amsguests.guestName AS 'Guest Name', 
       roominformation.roomName AS 'Room', 
       amsusers.userFullName AS 'Encoded By', 
       amsreservation.EncodedDate, 
       amsreservation.reservationDateFrom AS 'Date of Stay', 
       amsreservation.reservationDateTo AS 'Up To', 
       amsreservation.reservationStatus AS 'Status'
        FROM amsreservation
        LEFT JOIN amsguests ON amsguests.guestID = amsreservation.guestID
        LEFT JOIN roominformation ON roominformation.roomID = amsreservation.roomID
        LEFT JOIN amsusers ON amsusers.userID = amsreservation.userID
        WHERE amsreservation.reservationStatus LIKE 'Reserved'
        "

            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Reservation.DataGridView1.DataSource = BS

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub ReadRoomSearchComboBox()
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT roomName 
            FROM roominformation"
            Command = New MySqlCommand(Query, MysqlConn)
            CmdRead = Command.ExecuteReader
            While CmdRead.Read
                Dim sName = CmdRead.GetString("roomName")
                Reservation.ComboBox1.Items.Add(sName)
            End While

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        MysqlConn.Dispose()
        Command.Dispose()
    End Sub

    Public Sub ReadCheckIN(GRD As DataGridView)
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT checkin.checkinID'ID', amsguests.guestName'Guest Name',roominformation.roomName'Room Name', amsreservation.reservationDateFrom'From', amsreservation.reservationDateTo'Up-To'
            FROM checkin
            JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            JOIN roominformation ON amsreservation.roomID = roominformation.roomID
            WHERE checkinStat= @stat"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@stat", "Check-in")
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

    Public Sub ReadOrders(Grid As DataGridView)
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT orderID'ID', productsinformation.productName AS 'Order',
            FORMAT(productsinformation.productPrice, 2) AS 'Price',
            SUM(guestorderfoods.Qty) AS 'Qty',
            FORMAT(SUM(guestorderfoods.Total), 2) AS 'Total'
            FROM guestorderfoods
            LEFT JOIN checkin ON guestorderfoods.checkinID = checkin.checkinID
            LEFT JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            LEFT JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            LEFT JOIN productsinformation ON guestorderfoods.productID = productsinformation.productID
            WHERE checkin.checkinID LIKE @ID
            GROUP BY productsinformation.productName, productsinformation.productPrice"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", CheckInList.DataGridView1.Rows(CheckInList.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            MDA.SelectCommand = Command
            MDA.Fill(DT)
            BS.DataSource = DT
            Grid.DataSource = BS


            Dim totalSum As Double = 0.0
            For Each row As DataRow In DT.Rows
                Dim total As Double = CDbl(row("Total"))
                totalSum += total
            Next

            GuestOrders.TextBox1.Text = Format(totalSum, "n")
            TotalOrders = totalSum


            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub ReadOrdersEquipments()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT guestorderequipment.orderID'ID', equipments.equipName'Order', 
            FORMAT(equipments.equipPrice, 2)'Price', 
            SUM(guestorderequipment.Qty)'Qty', 
            FORMAT(SUM(guestorderequipment.Total), 2)'Total'
            FROM guestorderequipment
            LEFT JOIN checkin ON guestorderequipment.checkinID = checkin.checkinID
            LEFT JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            LEFT JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            LEFT JOIN equipments ON equipments.equipID = guestorderequipment.equipID
            WHERE checkin.checkinID	LIKE @ID
            GROUP BY equipments.equipName, equipments.equipPrice"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", CheckInList.DataGridView1.Rows(CheckInList.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            MDA.SelectCommand = Command
            MDA.Fill(DT)
            BS.DataSource = DT
            GuestOrders.DataGridView1.DataSource = BS

            Dim totalSum As Double = 0.0
            For Each row As DataRow In DT.Rows
                Dim total As Double = CDbl(row("Total"))
                totalSum += total
            Next

            GuestOrders.TextBox5.Text = Format(totalSum, "n")
            TotalOrdersEquipments = totalSum


            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub


    Public Sub ReadRoomCheckIn()
        Dim Query As String
        Dim Query2 As String
        Dim CmdRead2 As MySqlDataReader
        Dim Command2 As MySqlCommand
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT roominformation.roomId'ID', roomName'Room Name', FORMAT(roominformation.roomPrice, 2)'Price', amsreservation.reservationDateFrom'Date of Stay', amsreservation.reservationDateTo'Up to'
            FROM checkin
            JOIN amsreservation ON amsreservation.reservationID = checkin.reservationID
            JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            WHERE checkin.checkinID LIKE @ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", CheckInList.DataGridView1.Rows(CheckInList.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            GuestOrders.DataGridView3.DataSource = BS

            Dim totalSum As Double = 0.0
            For Each row As DataRow In DT.Rows
                Dim roomPrice As Double = CDbl(row("Price"))
                Dim dateFrom As DateTime = CDate(row("Date of Stay"))
                Dim dateTo As DateTime = CDate(row("Up to"))
                Dim lengthOfStay As Integer = CInt((dateTo - dateFrom).TotalDays)
                Dim totalRoomPrice As Double = roomPrice * lengthOfStay
                totalSum += totalRoomPrice
            Next

            GuestOrders.TextBox3.Text = Format(totalSum, "n")
            TotalCheckIN = totalSum

            MysqlConn.Close()

            Try
                Dim formattedTotalsum As Decimal = totalSum.ToString("n", CultureInfo.InvariantCulture)
                MysqlConn.Open()
                Query2 = "UPDATE checkin
                            SET Total = @Total
                            WHERE checkinID = @ID"
                Command2 = New MySqlCommand(Query2, MysqlConn)
                Command2.Parameters.AddWithValue("@Total", formattedTotalsum)
                Command2.Parameters.AddWithValue("@ID", CheckInList.DataGridView1.Rows(CheckInList.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
                CmdRead2 = Command2.ExecuteReader
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub ReadRoomInformation()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable


        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT roomID'ID', roomName'Room Name', FORMAT(roomPrice, 2)'Room Price' FROM roominformation"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Rooms.DataGridView1.DataSource = BS

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub CheckInPackage()
        Dim Query As String
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT orderID'ID', packageandpromos.packageName'Package', FORMAT(SUM(packageandpromos.packagePrice), 2)'Price'
            FROM guestavailedpackage
            LEFT JOIN packageandpromos ON packageandpromos.packageID = guestavailedpackage.packageID
            LEFT JOIN checkin ON checkin.checkinID = guestavailedpackage.checkinID
            WHERE checkin.checkinID LIKE @ID
            GROUP BY packageandpromos.packageName, packageandpromos.packagePrice"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", CheckInList.DataGridView1.Rows(CheckInList.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            GuestOrders.DataGridView4.DataSource = BS

            Dim totalSum As Double = 0.0
            For Each row As DataRow In DT.Rows
                Dim total As Double = CDbl(row("Price"))
                totalSum += total
            Next


            GuestOrders.TextBox4.Text = Format(totalSum, "n")
            TotalCheckInPackage = totalSum

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub ReadFoods(Grid As DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT productID'ID', productName'Product Name', FORMAT(productPrice, 2) AS'Price'
            FROM productsinformation
            JOIN productcategory ON productcategory.productcategoryID = productsinformation.productcategoryID"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Grid.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub ProductCategory(Comb As System.Windows.Forms.ComboBox)
        If Comb.Items.Count = 0 Then
            Dim MDA As New MySqlDataAdapter
            Dim BS As New BindingSource
            Dim DT As New DataTable
            Dim Query As String

            MysqlConn = New MySqlConnection
            MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

            Try
                MysqlConn.Open()
                Query = "SELECT productCategory FROM productcategory"
                Command = New MySqlCommand(Query, MysqlConn)
                CmdRead = Command.ExecuteReader
                While CmdRead.Read
                    Dim sName = CmdRead.GetString("productCategory")
                    Comb.Items.Add(sName)
                End While

                MysqlConn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            MysqlConn.Dispose()
            Command.Dispose()
        End If
    End Sub

    Public Sub ReadPackageandpromos(Grid As DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT packageID'ID', packageName'Package Name', packageDescription'Package Description', 
            FORMAT(packagePrice, 2)'Price'
            FROM packageandpromos"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Grid.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub ReadEquipments(Grid As DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT equipID'ID', equipName'Name of Equipment', FORMAT(equipPrice,2)'Price'
            FROM equipments"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Grid.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub DivingCertificate(Grid As DataGridView)
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
            LEFT JOIN amsusers ON amsusers.userID = divercertificate.userID"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Grid.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub ReadInventory(Grid As DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT inventoryID'ID', productName'Product Name', productStocks'Stocks', threshold'Threshold'
            FROM inventory
            JOIN productsinformation ON productsinformation.productID = inventory.productID
            ORDER BY productStocks ASC"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            BS.DataSource = DT
            Grid.DataSource = BS

            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()

        End Try
    End Sub

    Public Sub ReadLogs()
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
            JOIN productsinformation ON inventory.productID = productsinformation.productID;"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)
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

    Public Sub ReadConsumedProduct(Grid As DataGridView, DTPOne As DateTimePicker, DTPTwo As DateTimePicker)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT dateConsumed'Date of Purchase', amsguests.guestName'Guest Name', 
            productsinformation.productName AS 'Order',
            guestorderfoods.Qty'Qty'
            FROM guestorderfoods
            LEFT JOIN checkin ON guestorderfoods.checkinID = checkin.checkinID
            LEFT JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            LEFT JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            LEFT JOIN productsinformation ON guestorderfoods.productID = productsinformation.productID
            WHERE dateConsumed >= @DateOne AND dateConsumed <= @DateTwo
            GROUP BY amsguests.guestName, productsinformation.productName, guestorderfoods.Qty"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@DateOne", Format(CDate(DTPOne.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@DateTwo", Format(CDate(DTPTwo.Text), "yyyy-MM-dd"))
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Grid.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Public Sub EquipmentInventory(Grid As DataGridView)
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT invequipID'ID', equipments.equipName'Item', equipinventory.equipStocks'Stocks'
            FROM equipinventory
            LEFT JOIN equipments ON equipments.equipID = equipinventory.equipID"
            Command = New MySqlCommand(Query, MysqlConn)
            MDA.SelectCommand = Command
            MDA.Fill(DT)

            If DT.Rows.Count = 0 Then
                MysqlConn.Close()
                Command.Dispose()
                MysqlConn.Dispose()
                BS.DataSource = DT
                Grid.DataSource = BS
                MessageBox.Show("No Record Found!", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Public Sub FetchClientEmail(Tbx As String)
        Dim FetchEmailQuery As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            FetchEmailQuery = "SELECT guestEmail FROM amsguests WHERE guestName LIKE @guestName"
            Command = New MySqlCommand(FetchEmailQuery, MysqlConn)
            Command.Parameters.AddWithValue("@guestName", Tbx)
            guestEmail = Convert.ToString(Command.ExecuteScalar())
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub FetchroomandDate()
        Dim FetchRmandDate As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            FetchRmandDate = "SELECT roominformation.roomName, reservationDateFrom,
            reservationDateTo, guestEmail, guestName
            FROM amsreservation
            JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            JOIN amsguests ON amsguests.guestID = amsreservation.guestID
            WHERE amsguests.guestName = @name"
            Command = New MySqlCommand(FetchRmandDate, MysqlConn)
            Command.Parameters.AddWithValue("@name", Reservation.DataGridView1.Rows(Reservation.DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString)
            CmdRead = Command.ExecuteReader

            While CmdRead.Read
                customerName = CmdRead.GetString("guestName")
                roomName = CmdRead.GetString("roomName")
                Date1 = CmdRead.GetString("reservationDateFrom")
                Date2 = CmdRead.GetString("reservationDateTo")
                guestEmailAdd = CmdRead.GetString("guestEmail")
            End While

            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub FetchEmailOnce()
        Dim Query As String


        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT guestEmail
            FROM amsreservation
            JOIN amsguests ON amsguests.guestID = amsreservation.guestID
            WHERE amsguests.guestName = @email"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@email", Reservation.DataGridView1.Rows(Reservation.DataGridView1.CurrentCell.RowIndex).Cells("Guest Name").Value.ToString)
            CmdRead = Command.ExecuteReader

            While CmdRead.Read
                guestEmailAdd = CmdRead.GetString("guestEmail")
            End While

            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub Threshold()
        Dim MDA As New MySqlDataAdapter
        Dim BS As New BindingSource
        Dim DT As New DataTable
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "SELECT productsinformation.productName
            FROM inventory
            JOIN productsinformation on productsinformation.productID = inventory.productID
            WHERE productStocks <= threshold"
            Command = New MySqlCommand(Query, MysqlConn)

            MDA.SelectCommand = Command
            MDA.Fill(DT)

            Dim rowCount As Integer = DT.Rows.Count

            If rowCount > 0 Then
                MessageBox.Show(rowCount & " products hit the threshold, Go to inventory and check it!", "Threshold", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Close()
            Command.Dispose()
            MysqlConn.Dispose()
        End Try
    End Sub
End Class
