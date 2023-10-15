Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Controls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class Update
    Public Sub UpdateUser()
        Dim Query As String
        Dim SHA256 As New SHA256Managed()

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & ";userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()

            ' Hash the new password
            Dim hashBytes() As Byte = SHA256.ComputeHash(Encoding.UTF8.GetBytes(AddUpdateUsers.TextBox3.Text))
            Dim hashedValue As String = BitConverter.ToString(hashBytes).Replace("-", String.Empty).ToUpper()


            ' Build the SQL query
            Query = "UPDATE amsusers 
                 SET userFullName = @name, 
                     userPassword = @password, 
                     userAccountRole = @role, 
                     email = @email 
                 WHERE userID = @ID"

            ' Prepare and execute the SQL query
            Using Command As New MySqlCommand(Query, MysqlConn)
                Command.Parameters.AddWithValue("@ID", Users.DataGridView1.Rows(Users.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
                Command.Parameters.AddWithValue("@name", AddUpdateUsers.TextBox2.Text)
                Command.Parameters.AddWithValue("@username", AddUpdateUsers.TextBox1.Text)
                Command.Parameters.AddWithValue("@password", hashedValue)
                Command.Parameters.AddWithValue("@email", AddUpdateUsers.TextBox4.Text)
                Command.Parameters.AddWithValue("@role", AddUpdateUsers.ComboBox1.Text)

                Command.ExecuteNonQuery()

                MessageBox.Show("User Updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If

            ' Note: You don't need to explicitly dispose of MySqlCommand objects in VB.NET
            MysqlConn.Dispose()
        End Try
    End Sub


    Public Sub UpdateToCheckOut()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE checkin
            SET checkinStat = @stat
            WHERE checkinID = @ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@stat", "Check-Out")
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

    Public Sub AddEquipmentStocks()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE equipinventory
            SET equipStocks = equipStocks + @equipStocks
            WHERE invequipID = @invequipID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@equipStocks", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@invequipID", EquipStocks.DataGridView1.Rows(EquipStocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Add Equipment Stock", "Restocks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()
            LoadThis.EquipmentInventory(EquipStocks.DataGridView1)

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

    Public Sub UpdateEquipmentStock()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE equipinventory
            SET equipStocks = equipStocks - @equipStocks
            WHERE invequipID = @invequipID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@equipStocks", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@invequipID", EquipStocks.DataGridView1.Rows(EquipStocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Update Equipment Stock", "Restocks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()

            MysqlConn.Open()
            Dim dateQuery As String = "INSERT INTO logsss (inventoryID, dateRestock, stockAdded, actionPerform) 
            VALUES (@id, @date, @count, @action)"
            Command = New MySqlCommand(dateQuery, MysqlConn)
            Command.Parameters.AddWithValue("@id", Stocks.DataGridView1.Rows(Stocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@date", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@count", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@action", "Deleted")
            CmdRead = Command.ExecuteReader
            MysqlConn.Close()
            LoadThis.EquipmentInventory(EquipStocks.DataGridView1)

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


    Public Sub MinusStocks()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE inventory
            SET productStocks = productStocks - @Amount
            WHERE inventoryID = @ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@Amount", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@ID", Stocks.DataGridView1.Rows(Stocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Update Stock", "Restocks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()
            LoadThis.ReadInventory(Stocks.DataGridView1)
            MysqlConn.Close()


            MysqlConn.Open()
            Dim dateQuery As String = "INSERT INTO logsss (inventoryID, dateRestock, stockAdded, actionPerform) 
            VALUES (@id, @date, @count, @action)"
            Command = New MySqlCommand(dateQuery, MysqlConn)
            Command.Parameters.AddWithValue("@id", Stocks.DataGridView1.Rows(Stocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@date", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@count", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@action", "Deleted")
            CmdRead = Command.ExecuteReader
            MysqlConn.Close()
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

    Public Sub AddStocks()
        Dim Query As String
        Dim dateQuery As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE inventory
            SET productStocks = productStocks + @Amount
            WHERE inventoryID = @ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@Amount", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@ID", Stocks.DataGridView1.Rows(Stocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Added Stock", "Restocks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()

            MysqlConn.Open()
            dateQuery = "INSERT INTO logsss (inventoryID, dateRestock, stockAdded, actionPerform) 
            VALUES (@id, @date, @count, @action)"
            Command = New MySqlCommand(dateQuery, MysqlConn)
            Command.Parameters.AddWithValue("@id", Stocks.DataGridView1.Rows(Stocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@date", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@count", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@action", "Re-Stock")
            CmdRead = Command.ExecuteReader
            MysqlConn.Close()

            LoadThis.ReadInventory(Stocks.DataGridView1)

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

    Public Sub UpdateGuest()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE amsguests, amsusers
            SET amsguests.guestName=@GuestName, amsguests.guestContactInfo=@ContactInfo, amsguests.userID = amsusers.userID, amsguests.EncodedDate=@EncodedDate, amsguests.guestEmail=@email
            WHERE amsguests.guestID=@guestID AND amsusers.userName=@user;"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@GuestName", AddUpdateGuest.TextBox2.Text)
            Command.Parameters.AddWithValue("@ContactInfo", AddUpdateGuest.TextBox1.Text)
            Command.Parameters.AddWithValue("@email", AddUpdateGuest.TextBox3.Text)
            Command.Parameters.AddWithValue("@EncodedDate", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@guestID", GuestForm.DataGridView1.Rows(GuestForm.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@user", Enconder)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Guest Data Update", "Update Guest", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadGuest()
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub UpdateReservation()
        Dim Query As String
        Dim Status As String = "Reserved"

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE amsreservation " &
                    "JOIN amsguests ON amsguests.guestID = amsreservation.guestID " &
                    "JOIN roominformation ON roominformation.roomID = amsreservation.roomID " &
                    "JOIN amsusers ON amsusers.userID = amsreservation.userID " &
                    "SET amsreservation.reservationDateFrom = @FromDate, " &
                    "    amsreservation.reservationDateTo = @ToDate, " &
                    "    amsreservation.EncodedDate = @DateNow, " &
                    "    amsreservation.reservationStatus = @ReservationStatus " &
                    "WHERE amsguests.guestName LIKE @GuestName " &
                    "  AND roominformation.roomName LIKE @Room " &
                    "  AND amsusers.userName LIKE @LoginUser"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@FromDate", Format(CDate(AddUpdateReservation.DateTimePicker1.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@ToDate", Format(CDate(AddUpdateReservation.DateTimePicker2.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@DateNow", Format(CDate(DashboardForm.Label3.Text), "yyyy-MM-dd"))
            Command.Parameters.AddWithValue("@ReservationStatus", Status)
            Command.Parameters.AddWithValue("@GuestName", AddUpdateReservation.TextBox2.Text)
            Command.Parameters.AddWithValue("@Room", AddUpdateReservation.TextBox1.Text)
            Command.Parameters.AddWithValue("@LoginUser", Enconder)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Reservation Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadReservation()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            LoadThis.ReadReservation()
        Finally
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub UpdateFood()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE productsinformation
            SET productName = @productName, productPrice = @productPrice, productcategoryID = (
              SELECT productcategoryID
              FROM productcategory
              WHERE productCategory = @productCat
            )
            WHERE productID = @ID"
            Command = New MySqlCommand(Query, MysqlConn)

            Command.Parameters.AddWithValue("@ID", FoodInformation.DataGridView1.Rows(FoodInformation.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@productName", AddUpdateFood.TextBox2.Text)
            Command.Parameters.AddWithValue("@productPrice", AddUpdateFood.TextBox1.Text)
            Command.Parameters.AddWithValue("@productCat", AddUpdateFood.ComboBox1.SelectedItem.ToString())

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Food Data Update", "Update Food", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadFoods(FoodInformation.DataGridView1)
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub UpdateRoom()
        Dim Query As String
        Dim price As Integer = AddUpdateRooms.roomPrice.Text
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE .roominformation set roomName=@Name, roomPrice=@price WHERE roomID=@ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", Rooms.DataGridView1.Rows(Rooms.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@Name", AddUpdateRooms.TextBox3.Text)
            Command.Parameters.AddWithValue("@price", price)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Room Data Update", "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadRoomInformation()
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub Updatepackageandpromos()
        Dim Query As String
        Dim price As Integer = AddUpdatePackageandPromos.TextBox1.Text

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE packageandpromos set packageName=@Name, packageDescription=@Description, packagePrice=@price WHERE packageID=@ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", PackageandPromos.DataGridView1.Rows(PackageandPromos.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@Name", AddUpdatePackageandPromos.TextBox2.Text)
            Command.Parameters.AddWithValue("@Description", AddUpdatePackageandPromos.TextBox3.Text)
            Command.Parameters.AddWithValue("@price", price)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Package Updated", "Update Package", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadPackageandpromos(PackageandPromos.DataGridView1)
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub UpdateEquipment()
        Dim Query As String
        'Dim price As Integer = 

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE equipments set equipName=@Name, equipPrice=@price WHERE equipID=@ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", Equipment.DataGridView1.Rows(Equipment.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@Name", AddUpdateEquipment.TextBox2.Text)
            Command.Parameters.AddWithValue("@price", AddUpdateEquipment.TextBox1.Text)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Package Equipment", "Update Equipment", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadEquipments(Equipment.DataGridView1)
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub

    Public Sub UpdateCertificate()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE divercertificate set certificateName=@Name, certificateLicense=@License, userID = (
            SELECT userID
            FROM amsusers
            WHERE userName LIKE @Login)
            WHERE certificateID=@ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", Certificates.DataGridView1.Rows(Certificates.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@Name", AddUpdateCertificates.TextBox2.Text)
            Command.Parameters.AddWithValue("@License", AddUpdateCertificates.TextBox1.Text)
            Command.Parameters.AddWithValue("@Login", Enconder)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Certificate Updated", "Update Certificate", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.DivingCertificate(Certificates.DataGridView1)
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub


    Public Sub UpdateOrders()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE divercertificate set certificateName=@Name, certificateLicense=@License, userID = (
            SELECT userID
            FROM amsusers
            WHERE userName LIKE @Login)
            WHERE certificateID=@ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", Certificates.DataGridView1.Rows(Certificates.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            Command.Parameters.AddWithValue("@Name", AddUpdateCertificates.TextBox2.Text)
            Command.Parameters.AddWithValue("@License", AddUpdateCertificates.TextBox1.Text)
            Command.Parameters.AddWithValue("@Login", Enconder)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Certificate Updated", "Update Certificate", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.DivingCertificate(Certificates.DataGridView1)
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()

                MessageBox.Show(ex.Message)

                MysqlConn.Dispose()
                Command.Dispose()
            End If

            MysqlConn.Dispose()
            Command.Dispose()
        End Try
    End Sub


    Public Sub UpdateOrderFood()
        Dim Query As String
        Dim NewTotal As Double
        Dim productPrice As Double
        Dim UpdateStock As String = ""
        Dim productName As String = ""

        Try
            Using MysqlConn As New MySqlConnection("server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & "")
                MysqlConn.Open()

                ' Retrieve the product name and price based on the orderID
                Dim getProductNameQuery As String = "SELECT productsinformation.productName, productsinformation.productPrice
                FROM guestorderfoods
                LEFT JOIN productsinformation ON guestorderfoods.productID = productsinformation.productID
                WHERE guestorderfoods.orderID = @orderID"
                Dim getProductNameCommand As New MySqlCommand(getProductNameQuery, MysqlConn)
                getProductNameCommand.Parameters.AddWithValue("@orderID", GuestOrders.DataGridView2.Rows(GuestOrders.DataGridView2.CurrentCell.RowIndex).Cells("ID").Value.ToString())

                Using reader As MySqlDataReader = getProductNameCommand.ExecuteReader()
                    If reader.Read() Then
                        productName = reader.GetString("productName")
                        productPrice = reader.GetDouble("productPrice")
                    End If
                End Using

                ' Calculate the new total based on the product price and quantity
                Dim newQty As Integer = Convert.ToInt32(UpdateQtyOrder.TextBox1.Text)
                NewTotal = newQty * productPrice
                '=============================================================
                ' Retrieve the previous quantity from the guestorderfoods table
                Dim previousQty As Integer
                Dim getPreviousQtyQuery As String = "SELECT Qty FROM guestorderfoods WHERE orderID = @orderID"
                Dim getPreviousQtyCommand As New MySqlCommand(getPreviousQtyQuery, MysqlConn)
                getPreviousQtyCommand.Parameters.AddWithValue("@orderID", GuestOrders.DataGridView2.Rows(GuestOrders.DataGridView2.CurrentCell.RowIndex).Cells("ID").Value.ToString())
                previousQty = Convert.ToInt32(getPreviousQtyCommand.ExecuteScalar())

                Dim quantityDifference As Integer = newQty - previousQty

                ' Update the Qty and Total in the guestorderfoods table
                Query = "UPDATE guestorderfoods
                SET Qty = @Qty, Total = @Total
                WHERE orderID = @orderID"

                Using updateCommand As New MySqlCommand(Query, MysqlConn)
                    updateCommand.Parameters.AddWithValue("@Qty", newQty)
                    updateCommand.Parameters.AddWithValue("@Total", NewTotal)
                    updateCommand.Parameters.AddWithValue("@orderID", GuestOrders.DataGridView2.Rows(GuestOrders.DataGridView2.CurrentCell.RowIndex).Cells("ID").Value.ToString())
                    updateCommand.ExecuteNonQuery()
                End Using

                MessageBox.Show("Order Updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadThis.ReadOrders(GuestOrders.DataGridView2)

                ' Now let's adjust the inventory based on the quantity difference
                If quantityDifference <> 0 Then
                    If quantityDifference > 0 Then
                        UpdateStock = "UPDATE inventory
                        LEFT JOIN productsinformation ON productsinformation.productID = inventory.productID
                        SET productStocks = productStocks - @quantityDifference
                        WHERE productsinformation.productName LIKE @productName"
                    ElseIf quantityDifference < 0 Then
                        UpdateStock = "UPDATE inventory
                        LEFT JOIN productsinformation ON productsinformation.productID = inventory.productID
                        SET productStocks = productStocks + @quantityDifference
                        WHERE productsinformation.productName LIKE @productName"
                    End If

                    Using updateStockCommand As New MySqlCommand(UpdateStock, MysqlConn)
                        updateStockCommand.Parameters.AddWithValue("@quantityDifference", Math.Abs(quantityDifference))
                        updateStockCommand.Parameters.AddWithValue("@productName", productName)
                        updateStockCommand.ExecuteNonQuery()
                    End Using
                End If
            End Using

        Catch ex As MySqlException
            MessageBox.Show("MySQL Exception: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Exception: " & ex.Message)
        End Try
    End Sub

    Public Sub UpdateOrderEquipment()
        Dim Query As String
        Dim QueryRetrieveQty As String
        Dim newQty As Integer
        Dim NewTotal As Double
        Dim previousQty As Integer
        Dim QueryRetrieveQtyCommand As MySqlCommand
        Dim productName As String = ""
        Dim productprice As Integer
        Dim QtyOrderQuery As String
        Dim QtyDifference As Integer
        Dim QtyOrderCommand As MySqlCommand
        Dim UpdateQueryInv As String = ""

        Try

            Using MysqlConn As New MySqlConnection("server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & "")
                MysqlConn.Open()
                QueryRetrieveQty = "SELECT equipments.equipName, equipments.equipPrice
            FROM guestorderequipment
            LEFT JOIN equipments ON equipments.equipID = guestorderequipment.equipID
            WHERE guestorderequipment.orderID = @equipName"
                QueryRetrieveQtyCommand = New MySqlCommand(QueryRetrieveQty, MysqlConn)
                QueryRetrieveQtyCommand.Parameters.AddWithValue("@equipName", GuestOrders.DataGridView1.Rows(GuestOrders.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString())
                '========================================================================
                Using reader As MySqlDataReader = QueryRetrieveQtyCommand.ExecuteReader()
                    If reader.Read() Then
                        productName = reader.GetString("equipName")
                        productprice = reader.GetInt32("equipPrice")
                    End If
                End Using

                newQty = Convert.ToInt32(UpdateEquipQty.TextBox1.Text)
                NewTotal = newQty * productprice
                '========================================================================
                QtyOrderQuery = "SELECT Qty FROM guestorderequipment WHERE orderID =@orderID"
                QtyOrderCommand = New MySqlCommand(QtyOrderQuery, MysqlConn)
                QtyOrderCommand.Parameters.AddWithValue("@orderID", GuestOrders.DataGridView1.Rows(GuestOrders.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString())
                previousQty = Convert.ToInt32(QtyOrderCommand.ExecuteScalar())

                QtyDifference = newQty - previousQty

                Query = "UPDATE guestorderequipment
            SET Qty =@Qty, Total = @Total
            WHERE orderID =@orderID"

                Using updateCommand As New MySqlCommand(Query, MysqlConn)
                    updateCommand.Parameters.AddWithValue("@Qty", newQty)
                    updateCommand.Parameters.AddWithValue("@Total", NewTotal)
                    updateCommand.Parameters.AddWithValue("@orderID", GuestOrders.DataGridView1.Rows(GuestOrders.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString())
                    updateCommand.ExecuteNonQuery()
                End Using

                MessageBox.Show("Order Updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadThis.ReadOrdersEquipments()

                If QtyDifference <> 0 Then
                    If QtyDifference > 0 Then
                        UpdateQueryInv = "UPDATE equipinventory
                    LEFT JOIN equipments on equipments.equipID = equipinventory.equipID
                    SET equipStocks = equipStocks - @equipStocks
                    WHERE equipName LIKE @equipName"
                    ElseIf QtyDifference < 0 Then
                        UpdateQueryInv = "UPDATE equipinventory
                    LEFT JOIN equipments on equipments.equipID = equipinventory.equipID
                    SET equipStocks = equipStocks + @equipStocks
                    WHERE equipName LIKE @equipName"
                    End If

                    Using CommandUpdate As New MySqlCommand(UpdateQueryInv, MysqlConn)
                        CommandUpdate.Parameters.AddWithValue("@equipStocks", Math.Abs(QtyDifference))
                        CommandUpdate.Parameters.AddWithValue("@equipName", productName)
                        CommandUpdate.ExecuteNonQuery()
                    End Using
                End If
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub Updatethreshold()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "UPDATE inventory
            SET threshold = @value
            WHERE inventoryID = @ID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@value", AddUpdateStocks.TextBox1.Text)
            Command.Parameters.AddWithValue("@ID", Stocks.DataGridView1.Rows(Stocks.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)
            CmdRead = Command.ExecuteReader

            MessageBox.Show("Update threshold limit", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()
            LoadThis.ReadInventory(Stocks.DataGridView1)
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
