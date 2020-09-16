<%@ Page Language="VB" AutoEventWireup="true" %>

<%@ Import Namespace="System.Security.Cryptography" %>

<%@ Import Namespace="System.Threading" %>

 

<script runat="server">

    Sub Page_Load()

        Dim delay As Byte() = New Byte(0) {}

        Dim prng As RandomNumberGenerator = New RNGCryptoServiceProvider()

    

        prng.GetBytes(delay)

        Thread.Sleep(CType(delay(0), Integer))

    

        Dim disposable As IDisposable = TryCast(prng, IDisposable)

        If Not disposable Is Nothing Then

            disposable.Dispose()

        End If

    End Sub

</script>

 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

 

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>

</head>

<body>

    <div>

        An error occurred while processing your request.

    </div>

</body>

</html>

