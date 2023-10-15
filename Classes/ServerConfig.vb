Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Bcpg

Public Class ServerConfig

    Public Sub TestConnection(hst As TextBox, usr As TextBox, pas As TextBox, dat As TextBox)
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "server=" & hst.Text & "; userid=" & usr.Text & ";password=" & pas.Text & ";database=" & dat.Text & ""

        Try
            MysqlConn.Open()
            MessageBox.Show("Connected Successfully", "Connected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MysqlConn.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Public Sub LoadConfiguration(Serverr As TextBox, Userr As TextBox, Passwordd As TextBox, Databasee As TextBox)
        Dim Appset = ConfigurationManager.AppSettings

        Try
            Serverr.Text = Appset("Server")
            Databasee.Text = Appset("Database")
            Userr.Text = Appset("Username")
            Passwordd.Text = Appset("Password")

            host = Serverr.Text
            database = Databasee.Text
            user = Userr.Text
            password = Passwordd.Text

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub LoadPathConfig(Cred As TextBox, Rsv As TextBox, Updt As TextBox, cnl As TextBox, reportPatthh As TextBox, ApName As TextBox)
        Dim Appset = ConfigurationManager.AppSettings

        Try
            Cred.Text = Appset("Credentials")
            Rsv.Text = Appset("Reservation")
            Updt.Text = Appset("Update")
            cnl.Text = Appset("Canncell")
            reportPatthh.Text = Appset("Rptport")
            ApName.Text = Appset("ApName")

            Credd = Cred.Text
            Rsvv = Rsv.Text
            Updtt = Updt.Text
            cnll = cnl.Text
            reportPathh = reportPatthh.Text
            ApNaamee = ApName.Text

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub SavePathConfig(ConfigFrm As Form, Cred As TextBox, Rsv As TextBox, Updt As TextBox, cnl As TextBox, reportPatthh As TextBox, ApName As TextBox)
        Dim Configs As Configuration = ConfigurationManager.OpenExeConfiguration(Application.StartupPath & "\AMS.exe")
        Dim Appsettings As AppSettingsSection = Configs.AppSettings

        Try
            Appsettings.Settings.Item("Credentials").Value = Cred.Text
            Appsettings.Settings.Item("Reservation").Value = Rsv.Text
            Appsettings.Settings.Item("Update").Value = Updt.Text
            Appsettings.Settings.Item("Canncell").Value = cnl.Text
            Appsettings.Settings.Item("Rptport").Value = reportPatthh.Text
            Appsettings.Settings.Item("ApName").Value = ApName.Text

            Configs.Save(ConfigurationSaveMode.Modified)

            Cred.Refresh()
            Rsv.Refresh()
            Updt.Refresh()
            cnl.Refresh()
            reportPatthh.Refresh()
            ApName.Refresh()

            ConfigFrm.Hide()

            MessageBox.Show("Configuration saved successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub SaveConfiguration(ConfigForm As Form, Serverr As TextBox, User As TextBox, Pasword As TextBox, Database As TextBox)
        Dim Configs As Configuration = ConfigurationManager.OpenExeConfiguration(Application.StartupPath & "\AMS.exe")
        Dim Appsettings As AppSettingsSection = Configs.AppSettings

        Try
            Appsettings.Settings.Item("Server").Value = Serverr.Text
            Appsettings.Settings.Item("Database").Value = Database.Text
            Appsettings.Settings.Item("Username").Value = User.Text
            Appsettings.Settings.Item("Password").Value = Pasword.Text


            Configs.Save(ConfigurationSaveMode.Modified)

            Serverr.Refresh()
            User.Refresh()
            Pasword.Refresh()
            Database.Refresh()

            ConfigForm.Hide()

            MessageBox.Show("Configuration saved successfully, Restart Application", "Connection Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class
