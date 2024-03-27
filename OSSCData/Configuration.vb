Imports System.Configuration

Namespace Data
    Public Class Configuration

        Public Shared Function SqlConnectionString() As String
            'Return ConfigurationSettings.AppSettings("DBConnection").ToString()

            Dim dbConnection As String = ConfigurationSettings.AppSettings("DBConnection").ToString()
            Dim passwordPlaceholder As String = "%DB_PASSWORD%"

            ' Check if the DBConnection contains the password placeholder
            If dbConnection.Contains(passwordPlaceholder) Then

                ' Retrieve the password from the environment variable
                Dim password As String = Environment.GetEnvironmentVariable("DB_PASSWORD")

                ' Replace the placeholder with the actual password
                dbConnection = dbConnection.Replace(passwordPlaceholder, password)
            End If

            Return dbConnection
        End Function

    End Class
End Namespace

