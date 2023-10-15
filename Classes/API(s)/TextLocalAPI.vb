Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Resources
Imports Newtonsoft.Json.Linq ' Add a reference to Newtonsoft.Json NuGet package

Public Class TextLocalAPI
    Public Function sendSMS() As String
        Dim apiKey = "NzM1YTU3NzY0NDMyNzY0YzU3NGI2NjZiNDY1NDZjN2E="
        Dim message = "Hello, this is a test message."
        Dim numbers = "09456798807"
        Dim sender = "TextLocal API"
        Dim url As String = "https://api.txtlocal.com/send/"

        Dim requestParams As String = String.Format("apikey={0}&numbers={1}&message={2}&sender={3}",
                                                    apiKey, numbers, WebUtility.UrlEncode(message), sender)

        Dim request As WebRequest = WebRequest.Create(url)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(requestParams)
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = Nothing
        Dim response As WebResponse = Nothing
        Dim responseFromServer As String = ""

        Try
            dataStream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            responseFromServer = reader.ReadToEnd()

            reader.Close()
            dataStream.Close()
            response.Close()

            ' Parse the JSON response
            Dim jsonResponse As JObject = JObject.Parse(responseFromServer)
            Dim status As String = jsonResponse("status").ToString()
            Dim balance As Double = Convert.ToDouble(jsonResponse("balance"))
            Dim batchId As Long = Convert.ToInt64(jsonResponse("batch_id"))

            ' Handle the success response
            If status = "success" Then
                ' Access the extracted values as needed
                Console.WriteLine("SMS sent successfully!")
                Console.WriteLine("Balance: " & balance)
                Console.WriteLine("Batch ID: " & batchId)
            Else
                ' Handle the API response when it's not successful
                Console.WriteLine("SMS sending failed. Status: " & status)
            End If

            Return responseFromServer
        Catch ex As Exception
            ' Handle exceptions and error scenarios here
            Console.WriteLine("An error occurred: " & ex.Message)
            Return ex.Message
        Finally
            If dataStream IsNot Nothing Then
                dataStream.Close()
            End If
            If response IsNot Nothing Then
                response.Close()
            End If
        End Try
    End Function
End Class
