Imports MySql.Data.MySqlClient
Imports System.CodeDom
Imports System.Deployment.Application
Imports System.Security.Cryptography
Imports System.Text

Public Class CreateClass

    Public Sub CreateNewUser()
        Dim Query As String
        Dim SHA256 As New SHA256Managed()


        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "INSERT INTO amsusers (userFullName, userName, userPassword, userAccountRole, email)
                    VALUES (@name, @username, @password, @role, @email)"
            Command = New MySqlCommand(Query, MysqlConn)

            Dim hashBytes() As Byte = SHA256.ComputeHash(Encoding.UTF8.GetBytes(AddUpdateUsers.TextBox3.Text))
            Dim hashedValue As String = BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToUpper()


            Command.Parameters.AddWithValue("@name", AddUpdateUsers.TextBox2.Text)
            Command.Parameters.AddWithValue("@username", AddUpdateUsers.TextBox1.Text)
            Command.Parameters.AddWithValue("@password", hashedValue)
            Command.Parameters.AddWithValue("@email", AddUpdateUsers.TextBox4.Text)
            Command.Parameters.AddWithValue("@role", AddUpdateUsers.ComboBox1.Text)
            CmdRead = Command.ExecuteReader


            MessageBox.Show("New User Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub CreateNewGuest()
        Dim Query As String


        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""


        Try
            MysqlConn.Open()
            Query = "INSERT INTO amsguests (guestName, guestContactInfo, guestEmail, userID, EncodedDate)
            SELECT @guestName, @guestContact, @guestEmail, userID, @EncodedDate
            FROM amsusers
            WHERE userName = @LoginUser"

            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@guestName", AddUpdateGuest.TextBox2.Text)
            Command.Parameters.AddWithValue("@guestContact", AddUpdateGuest.TextBox1.Text)
            Command.Parameters.AddWithValue("@LoginUser", Enconder)
            Command.Parameters.AddWithValue("@EncodedDate", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@guestEmail", AddUpdateGuest.TextBox3.Text)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("New Guest Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadGuest()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
            GuestForm.Refresh()
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try

    End Sub

    Public Sub CreateReservation()
        Dim Query As String
        Dim Status As String = "Reserved"

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""


        Try
            MysqlConn.Open()
            Query = "INSERT INTO amsreservation (guestID, roomID, userID, reservationDateFrom, reservationDateTo, EncodedDate, reservationStatus)
            SELECT amsguests.guestID, roominformation.roomID, amsusers.userID, @FromDate, @ToDate, @DateNow, @Reservestatus
            FROM amsguests
            JOIN roominformation ON amsguests.guestName LIKE @guestName AND roominformation.roomName LIKE @room
            JOIN amsusers ON amsusers.userName LIKE @LoginUser"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@FromDate", Format(CDate(AddUpdateReservation.DateTimePicker1.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@ToDate", Format(CDate(AddUpdateReservation.DateTimePicker2.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@DateNow", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@Reservestatus", Status)
            Command.Parameters.AddWithValue("@guestName", AddUpdateReservation.TextBox2.Text)
            Command.Parameters.AddWithValue("@room", AddUpdateReservation.TextBox1.Text)
            Command.Parameters.AddWithValue("@LoginUser", Enconder)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("New Reservation Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadReservation()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
            LoadThis.ReadReservation()
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub CreateFoods(Combx As System.Windows.Forms.ComboBox)
        Dim Query As String


        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""


        Try
            MysqlConn.Open()
            Query = "INSERT INTO productsinformation (productName, productPrice, productcategoryID)
            SELECT @productName, @productPrice, productcategoryID
            FROM productcategory
            WHERE productCategory LIKE @selectedValue"

            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@productName", AddUpdateFood.TextBox2.Text)
            Command.Parameters.AddWithValue("@productPrice", AddUpdateFood.TextBox1.Text)
            Command.Parameters.AddWithValue("@selectedValue", "%" & Combx.SelectedItem.ToString() & "%")
            CmdRead = Command.ExecuteReader

            MessageBox.Show("New Product Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadFoods(FoodInformation.DataGridView1)
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try

    End Sub

    Public Sub CreateRoom()
        Dim Query As String


        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "INSERT INTO roominformation (roomName, roomPrice)
                    VALUES (@name, @price)"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@name", AddUpdateRooms.TextBox3.Text)
            Command.Parameters.AddWithValue("@price", AddUpdateRooms.roomPrice.Text)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("New Room Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadRoomInformation()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub Createpackageandpromos()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "INSERT INTO packageandpromos (packageName, packageDescription, packagePrice)
                    VALUES (@name, @description, @price)"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@name", AddUpdatePackageandPromos.TextBox2.Text)
            Command.Parameters.AddWithValue("@description", AddUpdatePackageandPromos.TextBox3.Text)
            Command.Parameters.AddWithValue("@price", AddUpdatePackageandPromos.TextBox1.Text)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("New Package Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadPackageandpromos(PackageandPromos.DataGridView1)
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try
    End Sub
    Public Sub CreateEquipment()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "INSERT INTO equipments (equipName, equipPrice)
                    VALUES (@name, @price)"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@name", AddUpdateEquipment.TextBox2.Text)
            Command.Parameters.AddWithValue("@price", AddUpdateEquipment.TextBox1.Text)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("New Equipment Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadEquipments(Equipment.DataGridView1)
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try
    End Sub
    Public Sub CreateDivingCertificate()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "INSERT INTO divercertificate (certificateName, certificateLicense, userID)
            SELECT @name, @License, userID
            FROM amsusers
            WHERE userName LIKE @Loginuser"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@name", AddUpdateCertificates.TextBox2.Text)
            Command.Parameters.AddWithValue("@License", AddUpdateCertificates.TextBox1.Text)
            Command.Parameters.AddWithValue("@Loginuser", Enconder)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("New License Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.DivingCertificate(Certificates.DataGridView1)
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
            CmdRead.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub InsertGuestOrderProduct()
        Dim InsertCommand As MySqlCommand
        Dim getstockCommand As MySqlCommand
        Dim updateStockCommand As MySqlCommand
        Dim TotalOrdered As Decimal
        Dim InsertOrderQuery As String
        Dim GetStockQuery As String
        Dim UpdateStock As String
        Dim CurrentStock As Integer
        Dim NewStock As Integer
        Dim productName As String
        Dim QuantityOrdered As Integer
        Dim i As Integer

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            For j As Integer = 0 To AddUpdateOrder.DataGridView2.Rows.Count - 1 Step 1
                productName = AddUpdateOrder.DataGridView2.Rows(j).Cells(0).Value.ToString()
                QuantityOrdered = Convert.ToInt32(AddUpdateOrder.DataGridView2.Rows(j).Cells(2).Value)
                TotalOrdered = AddUpdateOrder.DataGridView2.Rows(j).Cells(3).Value

                'Second, get the stocks from the database
                GetStockQuery = "SELECT productsinformation.productName, productStocks
                FROM inventory
                LEFT JOIN productsinformation ON productsinformation.productID = inventory.productID
                WHERE productsinformation.productName LIKE @productname"
                getstockCommand = New MySqlCommand(GetStockQuery, MysqlConn)
                getstockCommand.Parameters.AddWithValue("@productname", productName)
                CmdRead = getstockCommand.ExecuteReader()

                If CmdRead.Read() Then
                    CurrentStock = CmdRead.GetInt32("productStocks")
                End If
                CmdRead.Close()

                'Third, deduct the Qty from stocks. But first, the order must not be higher than stocks
                If CurrentStock >= QuantityOrdered Then
                    NewStock = CurrentStock - QuantityOrdered

                    'Fourth, put a conditional statement to prevent insert when stocks are insufficient
                    If QuantityOrdered > 0 Then
                        'Insert Order First
                        InsertOrderQuery = "INSERT INTO guestorderfoods (checkinID, productID, Qty, Total,dateConsumed)
                        SELECT checkin.checkinID, productsinformation.productID, @Qty, @Total, @dateConsumed
                        FROM checkin
                        JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
                        JOIN amsguests ON amsreservation.guestID = amsguests.guestID
                        JOIN productsinformation ON productsinformation.productName = @productName
                        WHERE amsguests.guestName = @guestName"
                        InsertCommand = New MySqlCommand(InsertOrderQuery, MysqlConn)
                        InsertCommand.Parameters.Clear()
                        InsertCommand.Parameters.AddWithValue("@guestName", AddUpdateOrder.TextBox2.Text)
                        InsertCommand.Parameters.AddWithValue("@productName", productName)
                        InsertCommand.Parameters.AddWithValue("@Qty", QuantityOrdered)
                        InsertCommand.Parameters.AddWithValue("@Total", TotalOrdered)
                        InsertCommand.Parameters.AddWithValue("@dateConsumed", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
                        i = InsertCommand.ExecuteNonQuery()

                        MessageBox.Show("Order Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    'Lastly, update the stock
                    UpdateStock = "UPDATE inventory
                    LEFT JOIN productsinformation ON productsinformation.productID = inventory.productID
                    SET productStocks = @productStocks
                    WHERE productsinformation.productName LIKE @productname"
                    updateStockCommand = New MySqlCommand(UpdateStock, MysqlConn)
                    updateStockCommand.Parameters.AddWithValue("@productStocks", NewStock)
                    updateStockCommand.Parameters.AddWithValue("@productname", productName)
                    i = updateStockCommand.ExecuteNonQuery()
                Else
                    MessageBox.Show("Insufficient Stocks for item: " & productName)
                    Exit For
                End If
            Next


            AddUpdateOrder.DataGridView2.Rows.Clear()
            AddUpdateOrder.DataGridView2.Columns.Clear()
            LoadThis.ReadOrders(GuestOrders.DataGridView2)
            AddUpdateOrder.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Close()
        End Try
    End Sub


    Public Sub InsertGuestOrderEquipment()
        Dim InsertCommand As MySqlCommand
        Dim getstockCommand As MySqlCommand
        Dim updateStockCommand As MySqlCommand
        Dim TotalOrdered As Decimal
        Dim InsertOrderQuery As String
        Dim GetStockQuery As String
        Dim UpdateStock As String
        Dim CurrentStock As Integer
        Dim NewStock As Integer
        Dim productName As String
        Dim QuantityOrdered As Integer
        Dim i As Integer

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            For j As Integer = 0 To AddOrderEquipment.DataGridView2.Rows.Count - 1 Step 1
                productName = AddOrderEquipment.DataGridView2.Rows(j).Cells(0).Value.ToString()
                QuantityOrdered = Convert.ToInt32(AddOrderEquipment.DataGridView2.Rows(j).Cells(2).Value)
                TotalOrdered = AddOrderEquipment.DataGridView2.Rows(j).Cells(3).Value

                'Second, get the stocks from the database
                GetStockQuery = "SELECT equipments.equipName, equipinventory.equipStocks
                FROM equipinventory
                LEFT JOIN equipments ON equipments.equipID = equipinventory.equipID
                WHERE equipments.equipName LIKE @equipname"
                getstockCommand = New MySqlCommand(GetStockQuery, MysqlConn)
                getstockCommand.Parameters.AddWithValue("@equipname", productName)
                CmdRead = getstockCommand.ExecuteReader()

                If CmdRead.Read() Then
                    CurrentStock = CmdRead.GetInt32("equipStocks")
                End If
                CmdRead.Close()

                'Third, deduct the Qty from stocks. But first, the order must not be higher than stocks
                If CurrentStock >= QuantityOrdered Then
                    NewStock = CurrentStock - QuantityOrdered

                    'Fourth, put a conditional statement to prevent insert when stocks are insufficient
                    If QuantityOrdered > 0 Then
                        'Insert Order First
                        InsertOrderQuery = "INSERT INTO guestorderequipment (checkinID, equipID, Qty, Total)
                        SELECT checkin.checkinID, equipments.equipID, @Qty, @Total
                        FROM checkin
                        JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
                        JOIN amsguests ON amsreservation.guestID = amsguests.guestID
                        JOIN equipments ON equipments.equipName = @equipname
                        WHERE amsguests.guestName = @guestName"
                        InsertCommand = New MySqlCommand(InsertOrderQuery, MysqlConn)
                        InsertCommand.Parameters.Clear()
                        InsertCommand.Parameters.AddWithValue("@guestName", AddOrderEquipment.TextBox2.Text)
                        InsertCommand.Parameters.AddWithValue("@equipname", productName)
                        InsertCommand.Parameters.AddWithValue("@Qty", QuantityOrdered)
                        InsertCommand.Parameters.AddWithValue("@Total", TotalOrdered)
                        i = InsertCommand.ExecuteNonQuery()

                        MessageBox.Show("Order Equipment Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    'Lastly, update the stock
                    UpdateStock = "UPDATE equipinventory
                    LEFT JOIN equipments ON equipments.equipID = equipinventory.equipID
                    SET equipStocks = @equipStocks
                    WHERE equipments.equipName LIKE @equipName"
                    updateStockCommand = New MySqlCommand(UpdateStock, MysqlConn)
                    updateStockCommand.Parameters.AddWithValue("@equipStocks", NewStock)
                    updateStockCommand.Parameters.AddWithValue("@equipName", productName)
                    i = updateStockCommand.ExecuteNonQuery()
                Else
                    MessageBox.Show("Insufficient Stocks for item: " & productName)
                    Exit For
                End If
            Next

            AddOrderEquipment.DataGridView2.Rows.Clear()
            AddOrderEquipment.DataGridView2.Columns.Clear()
            LoadThis.ReadOrdersEquipments()
            AddUpdateOrder.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Close()
        End Try
    End Sub

    Public Sub OrderPackageandPromos()
        Dim Query As String
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            For j As Integer = 0 To AddPackage.DataGridView2.Rows.Count - 1 Step 1
                Dim count As Integer = CInt(AddPackage.DataGridView2.Rows(j).Cells(3).Value)
                Query = "INSERT INTO guestavailedpackage(checkinID, packageID)
                SELECT checkinID, packageID
                FROM checkin
                JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
                JOIN amsguests ON amsreservation.guestID = amsguests.guestID
                JOIN packageandpromos ON packageandpromos.packageName = @packagename
                WHERE amsguests.guestName = @name"
                Command = New MySqlCommand(Query, MysqlConn)
                Command.Parameters.AddWithValue("@packagename", AddPackage.DataGridView2.Rows(j).Cells(1).Value.ToString())
                Command.Parameters.AddWithValue("@name", AddPackage.TextBox2.Text)

                For i As Integer = 1 To count
                    Command.ExecuteNonQuery()
                Next
            Next
            MessageBox.Show("Order Package Added", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            AddPackage.DataGridView2.Rows.Clear()
            LoadThis.CheckInPackage()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Close()
        End Try
    End Sub

    Public Sub CheckinReservation(Fmr As Form)
        Dim InsertfromRes As String
        Dim UpdateStat As String
        Dim UpdateStatComand As New MySqlCommand
        Dim UpdateCheckin As String
        Dim UpdateCheckinCommand As New MySqlCommand

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            InsertfromRes = "INSERT INTO checkin (reservationID) VALUES (@ID)"
            Command = New MySqlCommand(InsertfromRes, MysqlConn)
            Command.Parameters.AddWithValue("@ID", Reservation.DataGridView1.Rows(Reservation.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            CmdRead = Command.ExecuteReader
            MessageBox.Show("Checkin Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Fmr.Close()
            MysqlConn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Close()
            UpdateStatComand.Dispose()
            Command.Dispose()
        End Try

        Try
            MysqlConn.Open()
            UpdateStat = "UPDATE amsreservation
            SET reservationStatus = @resStat
            WHERE reservationID = @guestName"
            UpdateStatComand = New MySqlCommand(UpdateStat, MysqlConn)
            UpdateStatComand.Parameters.AddWithValue("@guestName", Reservation.DataGridView1.Rows(Reservation.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            UpdateStatComand.Parameters.AddWithValue("@resStat", "Occupied")
            UpdateStatComand.ExecuteNonQuery()

            MysqlConn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Close()
            UpdateStatComand.Dispose()
            Command.Dispose()
        End Try

        Try
            MysqlConn.Open()
            UpdateCheckin = "UPDATE checkin
            SET checkinStat = @stat
            WHERE reservationID = @name"
            UpdateCheckinCommand = New MySqlCommand(UpdateCheckin, MysqlConn)
            UpdateCheckinCommand.Parameters.AddWithValue("@stat", "Check-in")
            UpdateCheckinCommand.Parameters.AddWithValue("@name", Reservation.DataGridView1.Rows(Reservation.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            UpdateCheckinCommand.ExecuteNonQuery()

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Close()
            UpdateStatComand.Dispose()
            Command.Dispose()
        End Try
        LoadThis.ReadReservation()
    End Sub

    Public Sub CheckForCheckIn()
        Dim query As String
        Dim insertquery As String
        Dim updatequery As String

        Dim currentDat As DateTime = Date.Now
        Dim FormattedDate As String = currentDat.ToString("yyyy-MM-dd")

        Using MysqlConn As New MySqlConnection("server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & "")
            Try
                MysqlConn.Open()
                query = "SELECT reservationID FROM amsreservation WHERE reservationDateFrom <= @DateToday AND reservationDateTo >= @DateToday"
                Using Command As New MySqlCommand(query, MysqlConn)
                    Command.Parameters.AddWithValue("@DateToday", currentDat)
                    Dim reservationIDs As New List(Of Integer)

                    Using CmdRead = Command.ExecuteReader()
                        While CmdRead.Read
                            Dim id As Integer = CmdRead.GetInt32("reservationID")
                            reservationIDs.Add(id)
                        End While
                    End Using

                    ' Close the DataReader
                    CmdRead.Close()

                    ' Process the list of reservationIDs
                    For Each id As Integer In reservationIDs
                        ' INSERT into checkin table
                        insertquery = "INSERT INTO checkin (reservationID, checkinStat) VALUES (@reservation, @stat);"
                        Using insertCmd As New MySqlCommand(insertquery, MysqlConn)
                            insertCmd.Parameters.AddWithValue("@reservation", id)
                            insertCmd.Parameters.AddWithValue("@stat", "Check-In")
                            insertCmd.ExecuteNonQuery()
                        End Using

                        ' UPDATE amsreservation table
                        updatequery = "UPDATE amsreservation SET reservationStatus = @status WHERE reservationID = @reservation"
                        Using updateCmd As New MySqlCommand(updatequery, MysqlConn)
                            updateCmd.Parameters.AddWithValue("@reservation", id)
                            updateCmd.Parameters.AddWithValue("@status", "Occupied")
                            updateCmd.ExecuteNonQuery()
                        End Using
                    Next
                End Using

                MessageBox.Show("Guests have Checked-In", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadThis.ReadReservation()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using
    End Sub

    Public Sub OverallTotal()
        LoadThis.ReadOrders(GuestOrders.DataGridView2)
        LoadThis.ReadRoomCheckIn()
        LoadThis.CheckInPackage()
        LoadThis.ReadOrdersEquipments()

        Dim totalDec As Decimal
        totalDec = TotalOrders + TotalOrdersEquipments + TotalCheckIN + TotalCheckInPackage

        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE checkin
            SET overTotal = @Total
            WHERE checkinID = @ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@Total", totalDec)
            Command.Parameters.AddWithValue("@ID", CheckInList.DataGridView1.Rows(CheckInList.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            CmdRead = Command.ExecuteReader
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If
        Finally
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

End Class