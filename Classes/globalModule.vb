Imports Google.Apis.Gmail.v1
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json.Linq
Imports AMS
Imports FontAwesome.Sharp

Module globalModule

    '================Declaration of Class
    Public Gen As New GeneralClass
    Public Server As New ServerConfig
    Public Insert As New CreateClass
    Public LoadThis As New Read
    Public UpdateFunction As New Update
    Public Eraase As New Delete
    Public Look As New SearchClass
    Public TextLocal As New TextLocalAPI
    Public Prnt As New Print
    Public Salesss As New Sales



    '============Forgot Password Variables==============
    Public usrFullN, usrN, usrP, usreml, usrAccrole As String
    Public usrID As Integer


    Public usr As String

    '================Declaration of Public Variable
    Public host, user, password, database As String
    Public MysqlConn As New MySqlConnection
    Public Command As New MySqlCommand
    Public CmdRead As MySqlDataReader
    Public Enconder As String
    Public AccountType As String
    Public AdminEmail As String
    Public guestEmail As String = ""
    Public AdminForgot As String
    Public OTPCode As String
    Public usersID As Integer
    Public AdminID As Integer



    '================Public Variable of Deletion
    Public customerName As String = ""
    Public roomName As String
    Public Date1 As String
    Public Date2 As String
    Public guestEmailAdd As String
    Public Choose As String

    Public autoin As Integer
    Public reservationIDs As New List(Of Integer)

    '==================Path Variable
    Public Credd, Rsvv, Updtt, cnll, reportPathh, ApNaamee As String

    Public Button As IconButton
    Public BtnName As String
    Public patthh As String
    Public executeCheckForCheckIn As Boolean = False

    '=========Computation ng Overall Total
    Public TotalOrders As Decimal 'ReadOrders
    Public TotalOrdersEquipments As Decimal 'ReadOrdersEquipments
    Public TotalCheckIN As Decimal 'ReadRoomCheckIn
    Public TotalCheckInPackage As Decimal 'CheckInPackage

End Module
