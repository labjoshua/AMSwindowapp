Imports MySql.Data.MySqlClient

Public Class Delete
    Public Sub DeleteUser()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM amsusers where userID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", Users.DataGridView1.Rows(Users.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("User Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
                Command.Dispose()
                MessageBox.Show(ex.Message)
            End If

        End Try
    End Sub

    Public Sub DeleteGuest()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM amsguests where guestID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", GuestForm.DataGridView1.Rows(GuestForm.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("User Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()
            LoadThis.ReadGuest()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
                Command.Dispose()
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Public Sub DeleteReservation()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM amsreservation WHERE reservationID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", Reservation.DataGridView1.Rows(Reservation.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Reservation Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadReservation()
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

    Public Sub DeleteFood()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM productsinformation WHERE productID =@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", FoodInformation.DataGridView1.Rows(FoodInformation.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Food Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadFoods(FoodInformation.DataGridView1)
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

    Public Sub DeleteRoom()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM roominformation WHERE roomID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", Rooms.DataGridView1.Rows(Rooms.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Room Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadRoomInformation()
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

    Public Sub DeletePackageandPromos()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM packageandpromos WHERE packageID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", PackageandPromos.DataGridView1.Rows(PackageandPromos.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Package Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadPackageandpromos(PackageandPromos.DataGridView1)
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

    Public Sub DeleteEquipments()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM equipments WHERE equipID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", Equipment.DataGridView1.Rows(Equipment.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Equipment Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.ReadEquipments(Equipment.DataGridView1)
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

    Public Sub DeleteCertificate()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM divercertificate WHERE certificateID=@ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("ID", Certificates.DataGridView1.Rows(Certificates.DataGridView1.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Certificate Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.DivingCertificate(Certificates.DataGridView1)
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

    Public Sub DeleteFoodsOrder()
        Dim Query As String
        Dim QueryRetrieveQty As String
        Dim QueryRetrieveQtyCommand As MySqlCommand
        Dim QtyOrder As Integer
        Dim QtyOrderCommand As MySqlCommand
        Dim UpdateQueryInv As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            ' Retrieve the quantity from the guestorderfoods table before deleting the record
            QueryRetrieveQty = "SELECT productsinformation.productName, SUM(guestorderfoods.Qty)'Qty'
            FROM guestorderfoods
            LEFT JOIN productsinformation on productsinformation.productID = guestorderfoods.productID
            WHERE productName LIKE @orderName"
            QueryRetrieveQtyCommand = New MySqlCommand(QueryRetrieveQty, MysqlConn)
            QueryRetrieveQtyCommand.Parameters.AddWithValue("@orderName", GuestOrders.DataGridView2.Rows(GuestOrders.DataGridView2.CurrentCell.RowIndex).Cells("Order").Value.ToString())

            ' Execute the reader to get the quantity and productID
            Using reader As MySqlDataReader = QueryRetrieveQtyCommand.ExecuteReader()
                If reader.Read() Then
                    QtyOrder = reader.GetInt32("Qty")
                    Dim productName As String = reader.GetString("productName")
                    ' Close the reader before proceeding to update the inventory
                    reader.Close()

                    ' Update the inventory with the retrieved quantity
                    UpdateQueryInv = "UPDATE inventory
                    LEFT JOIN productsinformation ON productsinformation.productID = inventory.productID
                    SET productStocks = productStocks + @deletedQty
                    WHERE productsinformation.productName = @productID"
                    QtyOrderCommand = New MySqlCommand(UpdateQueryInv, MysqlConn)
                    QtyOrderCommand.Parameters.AddWithValue("@deletedQty", QtyOrder)
                    QtyOrderCommand.Parameters.AddWithValue("@productID", productName)
                    QtyOrderCommand.ExecuteNonQuery()
                End If
            End Using


            ' Proceed to delete the record from guestorderfoods table after updating the inventory

            Query = "DELETE FROM guestorderfoods
            WHERE productID IN (
            SELECT productID
            FROM productsinformation
            WHERE productName =@productName
            )"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@productName", GuestOrders.DataGridView2.Rows(GuestOrders.DataGridView2.CurrentCell.RowIndex).Cells("Order").Value.ToString())

            Command.ExecuteNonQuery()

            MessageBox.Show("Order Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadThis.ReadOrders(GuestOrders.DataGridView2)

        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
                Command.Dispose()
                MessageBox.Show(ex.Message)
            End If
        Finally
            MysqlConn.Close()
            Command.Dispose()
        End Try
    End Sub


    Public Sub DeleteEquipment()
        Dim DeleteQuery As String
        Dim RetrieveQtyQuery As String
        Dim QueryRetrieveQtyCommand As MySqlCommand
        Dim QtyOrder As Integer
        Dim ProductName As String
        Dim QtyOrderCommand As MySqlCommand
        Dim UpdateQueryinv As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            RetrieveQtyQuery = "SELECT equipments.equipName, SUM(guestorderequipment.Qty)'Qty'
            FROM guestorderequipment
            LEFT JOIN equipments ON equipments.equipID = guestorderequipment.equipID
            WHERE equipName =@equipname"
            QueryRetrieveQtyCommand = New MySqlCommand(RetrieveQtyQuery, MysqlConn)
            QueryRetrieveQtyCommand.Parameters.AddWithValue("@equipname", GuestOrders.DataGridView1.Rows(GuestOrders.DataGridView1.CurrentCell.RowIndex).Cells("Order").Value.ToString())

            Using reader As MySqlDataReader = QueryRetrieveQtyCommand.ExecuteReader
                If reader.Read() Then
                    QtyOrder = reader.GetInt32("Qty")
                    ProductName = reader.GetString("equipName")
                    reader.Close()

                    UpdateQueryinv = "UPDATE equipinventory
                    LEFT JOIN equipments ON equipments.equipID = equipinventory.equipID
                    SET equipStocks = equipStocks + @deletedQuery
                    WHERE equipments.equipName = @equipID"
                    QtyOrderCommand = New MySqlCommand(UpdateQueryinv, MysqlConn)
                    QtyOrderCommand.Parameters.AddWithValue("@deletedQuery", QtyOrder)
                    QtyOrderCommand.Parameters.AddWithValue("@equipID", ProductName)
                    QtyOrderCommand.ExecuteNonQuery()
                End If
            End Using

            DeleteQuery = "DELETE FROM guestorderequipment
            WHERE equipID IN (
            SELECT equipID 
            FROM equipments
            WHERE equipName = @equipname
            )"
            Command = New MySqlCommand(DeleteQuery, MysqlConn)
            Command.Parameters.AddWithValue("@equipname", GuestOrders.DataGridView1.Rows(GuestOrders.DataGridView1.CurrentCell.RowIndex).Cells("Order").Value.ToString())

            Command.ExecuteNonQuery()
            MessageBox.Show("Equipment deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadThis.ReadOrdersEquipments()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub DeletePackage()
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            MysqlConn.Open()
            Query = "DELETE FROM guestavailedpackage WHERE orderID = @orderID"
            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@orderID", GuestOrders.DataGridView4.Rows(GuestOrders.DataGridView4.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            CmdRead = Command.ExecuteReader

            MessageBox.Show("Package Availed Removed", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadThis.CheckInPackage()
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
                Command.Dispose()
                MessageBox.Show(ex.Message)
            End If
        Finally
            MysqlConn.Close()
            Command.Dispose()
        End Try
    End Sub
End Class
