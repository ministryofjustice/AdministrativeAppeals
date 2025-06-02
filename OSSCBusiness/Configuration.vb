Imports System.Configuration

Namespace Business
    Public Class Configuration

        '/ <summary>
        '/ Gets an integer value from the web configuration file.
        '/ </summary>
        '/ <param name="key"></param>
        '/ <returns></returns>
        Public Shared Function GetInt(ByVal key As String) As Integer
            Return Utility.ParseInt(ConfigurationManager.AppSettings(key))
        End Function


        '/ <summary>
        '/ Gets a string value from the web configuration file.
        '/ </summary>
        '/ <param name="key"></param>
        '/ <returns></returns>
        Public Shared Function GetString(ByVal key As String) As String
            Return ConfigurationManager.AppSettings(key)
        End Function
    End Class


End Namespace
