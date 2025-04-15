<%@ Page Language="VB" AutoEventWireup="true" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="NLog" %>

<script runat="server">
    Private Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Logger.Info("Entered the error page.")
        Dim lastError As Exception = Server.GetLastError()
        
        If lastError IsNot Nothing Then
            Logger.Error(lastError, "Error from Server.GetLastError()")
            Server.ClearError()
        Else
            ' If Server.GetLastError() is null, try to get error from Session
            lastError = TryCast(Session("LastError"), Exception)
            
            If lastError IsNot Nothing Then
                Logger.Error(lastError, "Error retrieved from session")
                
                ' Clear the error from session
                Session("LastError") = Nothing
            Else
                Logger.Warn("No error found to log.")
            End If
        End If
    Catch ex As Exception
        Logger.Error(ex, "Error while logging an error")
        End Try
    End Sub
    
</script>

<!DOCTYPE html>
<html>
<head>
    <title>Error</title>
    <style>
        body { font-family: Arial; margin: 20px; background: #f5f5f5; }
        .error-box { max-width: 600px; margin: 0 auto; padding: 20px; background: white; border-radius: 5px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        h1 { color: #d9534f; margin-top: 0; }
        .contact { margin-top: 20px; padding-top: 20px; border-top: 1px solid #eee; }
    </style>
</head>
<body>
    <div class="error-box">
        <h1>An Error Occurred</h1>
        <p>We apologize, but an error occurred while processing your request. Our technical team has been notified and will investigate the issue.</p>
        <div class="contact">
            <p>If you need immediate assistance, please contact our support team:</p>
            <p>Email: <%= ConfigurationSettings.AppSettings("DCA.TribunalsService.Ossc.Web.SupportEmail") %></p>
        </div>
        <p><a href="/">Return to Home Page</a></p>
    </div>
</body>
</html>

