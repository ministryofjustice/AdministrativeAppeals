<%@ Page Language="VB" AutoEventWireup="true" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Diagnostics" %>

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lastError As Exception = Server.GetLastError()
            ' Log the error to console (stdout) so it can be picked up by ECS or Kubernetes logs
            If lastError IsNot Nothing Then
                LogErrorToConsole(lastError)
                Server.ClearError()
            End If
            Server.ClearError() 
        Catch
            ' Ignore logging errors
        End Try
    End Sub

    Private Sub LogErrorToConsole(ByVal ex As Exception, Optional ByVal additionalInfo As String = "")
        Dim errorMsg As String = String.Format("{0}: {1} - {2}", DateTime.Now, ex.Message, additionalInfo)
        Console.WriteLine("ERROR: " & errorMsg)
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

