Imports System
Imports System.Configuration

Namespace DCA.TribunalsService.Ossc.Business
    Public Class Configuration

        '/ <summary>
        '/ Gets an integer value from the web configuration file.
        '/ </summary>
        '/ <param name="key"></param>
        '/ <returns></returns>
        Public Shared Function GetInt(ByVal key As String) As Integer
            Return Utility.ParseInt(ConfigurationSettings.AppSettings(key))
        End Function


        '/ <summary>
        '/ Gets a string value from the web configuration file.
        '/ </summary>
        '/ <param name="key"></param>
        '/ <returns></returns>
        Public Shared Function GetString(ByVal key As String) As String
            Return ConfigurationSettings.AppSettings(key)
        End Function
    End Class


End Namespace
