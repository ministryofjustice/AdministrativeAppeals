<%@ Page Language="VB" AutoEventWireup="true" %>

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim ex As Exception = Server.GetLastError()
            ' Optional: Log the error or inspect it
            Server.ClearError() ' Prevent default error processing
        Catch
            ' Ignore logging errors
        End Try
    End Sub
</script>

<!DOCTYPE html>
<html>
<head>
    <title>Error</title>
    <style>
        body { font-family: Arial, sans-serif; background: #f8f8f8; margin: 0; padding: 0; }
        .container { max-width: 600px; margin: 80px auto; background: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }
        h1 { color: #d9534f; }
    </style>
</head>
<body>
    <div class="container">
        <h1>Oops! Something went wrong.</h1>
        <p>An unexpected error occurred. Please try again later.</p>
        <p><a href="/">Go back to home</a></p>
    </div>
</body>
</html>
