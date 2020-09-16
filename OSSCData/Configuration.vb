Imports System.Configuration

Namespace DCA.TribunalsService.Ossc.Data
    Public Class Configuration

        Public Shared Function SqlConnectionString() As String
            Return ConfigurationSettings.AppSettings("DBConnection").ToString()
        End Function

    End Class
End Namespace

