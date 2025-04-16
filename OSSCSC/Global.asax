<%@ Application Language="VB" %>
<%@ Import Namespace="NLog" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.SessionState" %>

<script runat="server">
    Private ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()
    
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Logger.Info("//////////////// Application started \\\\\\\\\\\\\\\\")
        Catch ex As Exception
            Logger.Error(ex, "Error during application startup: {0}", ex.Message)
            Throw
        End Try
    End Sub
    
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex As Exception = Server.GetLastError()
        
        If ex IsNot Nothing Then
            Logger.Error(ex, "An unhandled exception occurred on the error page: ")
        End If
    End Sub
    
    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Logger.Info("Session started")
    End Sub
</script>