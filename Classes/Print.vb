Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Imports Org.BouncyCastle.Crypto.Modes.Gcm

Public Class Print
    Public Sub PrintOrderFoods(Grd As DataGridView)
        Dim DataAdapt As New MySqlDataAdapter
        Dim DataSource As New BindingSource
        Dim DataCollection As New datAms
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            DataCollection.Clear()
            MysqlConn.Open()
            DataAdapt.Update(DataCollection.Tables(0))

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
            Command.Parameters.AddWithValue("@ID", Grd.Rows(Grd.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            DataAdapt.SelectCommand = Command
            DataAdapt.Fill(DataCollection.Tables(0))

            DataSource.DataSource = DataCollection.Tables(0)

            BillOut.ReportViewer1.ProcessingMode = ProcessingMode.Local
            BillOut.ReportViewer1.LocalReport.DataSources.Clear()
            BillOut.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSetFoods", DataSource))
            BillOut.ReportViewer1.DocumentMapCollapsed = True
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        End Try
        Prnt.PrintOrderEquip(Grd)
    End Sub

    Public Sub PrintOrderEquip(Grd As DataGridView)
        Dim DataAdapt As New MySqlDataAdapter
        Dim DataSource As New BindingSource
        Dim DataCollection As New datAms
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            DataCollection.Clear()
            MysqlConn.Open()
            DataAdapt.Update(DataCollection.Tables(0))

            Query = "SELECT guestorderequipment.orderID'ID', equipments.equipName'Order', 
            FORMAT(equipments.equipPrice, 2)'Price', 
            SUM(guestorderequipment.Qty)'Qty', 
            FORMAT(SUM(guestorderequipment.Total), 2)'Total'
            FROM guestorderequipment
            LEFT JOIN checkin ON guestorderequipment.checkinID = checkin.checkinID
            LEFT JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            LEFT JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            LEFT JOIN equipments ON equipments.equipID = guestorderequipment.equipID
            WHERE checkin.checkinID LIKE @ID
            GROUP BY equipments.equipName, equipments.equipPrice"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", Grd.Rows(Grd.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            DataAdapt.SelectCommand = Command
            DataAdapt.Fill(DataCollection.Tables(0))

            DataSource.DataSource = DataCollection.Tables(0)

            BillOut.ReportViewer1.ProcessingMode = ProcessingMode.Local

            BillOut.ReportViewer1.LocalReport.ReportPath = reportPathh
            BillOut.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSetEquipment", DataSource))
            BillOut.ReportViewer1.DocumentMapCollapsed = True
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        End Try
        Prnt.Printpackage(Grd)
    End Sub

    Public Sub Printpackage(Grd As DataGridView)
        Dim DataAdapt As New MySqlDataAdapter
        Dim DataSource As New BindingSource
        Dim DataCollection As New datAms
        Dim Query As String

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            DataCollection.Clear()
            MysqlConn.Open()
            DataAdapt.Update(DataCollection.Tables(0))

            Query = "SELECT orderID'ID', packageandpromos.packageName'Package', FORMAT(packageandpromos.packagePrice, 2)'Price',
            FORMAT(packageandpromos.packagePrice, 2)'Total'
            FROM guestavailedpackage
            LEFT JOIN packageandpromos ON packageandpromos.packageID = guestavailedpackage.packageID
            LEFT JOIN checkin ON checkin.checkinID = guestavailedpackage.checkinID
            LEFT JOIN amsreservation ON checkin.reservationID = amsreservation.reservationID
            LEFT JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            WHERE checkin.checkinID LIKE @ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", Grd.Rows(Grd.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            DataAdapt.SelectCommand = Command
            DataAdapt.Fill(DataCollection.Tables(0))

            DataSource.DataSource = DataCollection.Tables(0)

            BillOut.ReportViewer1.ProcessingMode = ProcessingMode.Local

            BillOut.ReportViewer1.LocalReport.ReportPath = reportPathh
            BillOut.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSetPackage", DataSource))
            BillOut.ReportViewer1.DocumentMapCollapsed = True
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        End Try
        Prnt.PrintRoom(Grd)
    End Sub

    Public Sub PrintRoom(Grd As DataGridView)
        Dim Query As String
        Dim DataAdapt As New MySqlDataAdapter
        Dim DataSource As New BindingSource
        Dim DataCollection As New datAms

        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & host & "; userid=" & user & ";password=" & password & ";database=" & database & ""

        Try
            DataCollection.Clear()
            MysqlConn.Open()
            DataAdapt.Update(DataCollection.Tables(0))
            Query = "SELECT roominformation.roomId'ID', roomName'RoomName', 
            FORMAT(roominformation.roomPrice, 2)'Price', 
            amsreservation.reservationDateFrom'DateofStay', 
            amsreservation.reservationDateTo'Upto',
            FORMAT(Total, 2)'Total'
            FROM checkin
            JOIN amsreservation ON amsreservation.reservationID = checkin.reservationID
            JOIN roominformation ON roominformation.roomID = amsreservation.roomID
            JOIN amsguests ON amsreservation.guestID = amsguests.guestID
            WHERE checkin.checkinID LIKE @ID"

            Command = New MySqlCommand(Query, MysqlConn)
            Command.Parameters.AddWithValue("@ID", Grd.Rows(Grd.CurrentCell.RowIndex).Cells("ID").Value.ToString)

            DataAdapt.SelectCommand = Command
            DataAdapt.Fill(DataCollection.Tables(0))

            For Each row As DataRow In DataCollection.Tables(0).Rows
                Dim dateWithTimeOfStay As DateTime = CDate(row("DateofStay"))
                Dim dateWithTimeUpto As DateTime = CDate(row("Upto"))

                ' Set the time component to midnight to remove the time part
                Dim dateOnlyOfStay As DateTime = dateWithTimeOfStay.Date
                Dim dateOnlyUpto As DateTime = dateWithTimeUpto.Date

                ' Update the date values in the row
                row("DateofStay") = dateOnlyOfStay
                row("Upto") = dateOnlyUpto
            Next

            DataSource.DataSource = DataCollection.Tables(0)

            BillOut.ReportViewer1.ProcessingMode = ProcessingMode.Local

            BillOut.ReportViewer1.LocalReport.ReportPath = reportPathh
            BillOut.ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSetRoom", DataSource))
            BillOut.ReportViewer1.DocumentMapCollapsed = True
            MysqlConn.Close()
        Catch ex As Exception
            If MysqlConn.State = ConnectionState.Open Then
                MysqlConn.Close()
            End If
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
