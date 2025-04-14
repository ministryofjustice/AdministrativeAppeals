<%@ Application Language="VB" %>
<%@ Import Namespace="NLog" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">
    Private ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()
    
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Create logs directory if it doesn't exist
            Dim logsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")
            If Not Directory.Exists(logsPath) Then
                Directory.CreateDirectory(logsPath)
                Logger.Info("Created logs directory at: {0}", logsPath)
            End If

            ' Create archive directory if it doesn't exist
            Dim archivePath As String = Path.Combine(logsPath, "archive")
            If Not Directory.Exists(archivePath) Then
                Directory.CreateDirectory(archivePath)
                Logger.Info("Created archive directory at: {0}", archivePath)
            End If

            Logger.Info("Application started")
        Catch ex As Exception
            Logger.Error(ex, "Error during application startup: {0}", ex.Message)
            Throw
        End Try
    End Sub
    
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex = Server.GetLastError()
        Logger.Error(ex, "An unhandled exception occurred")
    End Sub
    
    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Logger.Info("Session started")
    End Sub
</script>