<%@ Application Language="VB" %>
<%@ Import Namespace="NLog" %>

<script runat="server">
    Private ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()
    
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Logger.Info("Application started")
    End Sub
    
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex = Server.GetLastError()
        Logger.Error(ex, "An unhandled exception occurred")
    End Sub
    
    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Logger.Info("Session started")
    End Sub
</script>