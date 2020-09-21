<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Login.aspx.vb" Inherits="OSSCSC.Web.Login" %>





<h1>Login</h1>
<p>This is a private, restricted system for authorised 
users only. Access to this site is available only to persons with a valid user 
name and password.</p>
<form id=form1 runat="server">
<p><asp:label id=lblError runat="server"></asp:Label></p>
<table class="form-table percent40">
  <tr>
    <th>User:</th>
    <td><asp:textbox id=txtUser runat="server"></asp:textbox></td>
  </tr>
  <tr>
    <th>Password:</th>
    <td><asp:textbox id=txtPassword runat="server" TextMode="Password"></asp:textbox></td>
  </tr>
  <tr>
    <td></td>
    <td><asp:button id=btnLogin runat="server" Text="Login"></asp:button></td>
  </tr>
</table>
</form>