<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditUsers.aspx.vb" Inherits="OSSCSC.Web.EditUsers"%>
<%@ Register TagPrefix="uc1" TagName="AdminNav" Src="../UserControls/AdminNav.ascx" %>
<script language="javascript">

// Validate the password field ONLY if the user is new.
function ValidatePassword (source, arguments) {
	if (-1 == parseInt(Form1.UserDropDownList.value)
		&& 0 == Form1.PasswordTextBox.value.length)
	{
    	arguments.IsValid = false;
		return false;
	}
	else {
		arguments.IsValid = true;
		return true;
	}
}
</script>
<h1>User Management</h1>
<form id="Form1" method="post" runat="server">
	<table>
		<tr>
			<td align="right" colSpan="2"><uc1:adminnav id="AdminNav1" runat="server"></uc1:adminnav></td>
		</tr>
		<tr>
			<td vAlign="top"><IMG src="../images/icon_tip.gif"></td>
			<td class="smaller" vAlign="top">Tip: You can create, edit or delete users. It is 
				not possible to delete all users from the database as at least one User is 
				needed in order to access the application. Duplicate login names are not 
				permitted.
			</td>
		</tr>
	</table>
	<table class="form-table percent80">
		<tr>
			<th>
				Selected User:</th>
			<td><asp:dropdownlist id="UserDropDownList" Runat="server" DataTextField="EmailAddress" DataValueField="UserID"
					AutoPostBack="True" Width="240px"></asp:dropdownlist><br>
				<br>
			</td>
		</tr>
		<tr>
			<th>
				Email/Login:</th>
			<td><asp:textbox id="EmailTextBox" Runat="server" Width="240px"></asp:textbox><asp:requiredfieldvalidator id="EmailReqFieldValidator" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="The email address is requred">*</asp:requiredfieldvalidator><asp:customvalidator id="UniqueEmailValidator" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="The specified email already exists">**</asp:customvalidator></td>
		</tr>
		<tr>
			<th>
				First name:</th>
			<td><asp:textbox id="FirstNameTextBox" Runat="server" Width="240px"></asp:textbox><asp:requiredfieldvalidator id="FirstNameReqFieldValidator" runat="server" ControlToValidate="FirstNameTextBox"
					ErrorMessage="Invalid first name.">*</asp:requiredfieldvalidator></td>
		</tr>
		<tr>
			<th>
				Last name:</th>
			<td><asp:textbox id="LastNameTextbox" Runat="server" Width="240px"></asp:textbox><asp:requiredfieldvalidator id="LastNameReqFieldValidator" runat="server" ControlToValidate="LastNameTextbox"
					ErrorMessage="Invalid last name.">*</asp:requiredfieldvalidator></td>
		</tr>
		<tr>
			<th>
				Password:</th>
			<td><asp:textbox id="PasswordTextBox" Runat="server" Width="240px" TextMode="Password"></asp:textbox><asp:customvalidator id="PasswordCustomValidator" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Please provide the user password."
					ClientValidationFunction="ValidatePassword">*</asp:customvalidator></td>
		</tr>
		<tr>
			<th>
				Confirm password:</th>
			<td><asp:textbox id="ConfirmPasswordTextbox" Runat="server" Width="240px" TextMode="Password"></asp:textbox><asp:comparevalidator id="ConfirmPasswordCompareValidator" runat="server" ControlToValidate="PasswordTextBox"
					ErrorMessage="The confirmation password doesn't match." ControlToCompare="ConfirmPasswordTextbox">*</asp:comparevalidator></td>
		</tr>
		<tr>
			<td align="right" colSpan="2">
				<asp:Button id="btnDelete" runat="server" Text="Delete"></asp:Button><asp:button id="SaveBtn" Runat="server" Text="Save"></asp:button><asp:button id="CancelBtn" Runat="server" Text="Cancel" CausesValidation="False"></asp:button></td>
		</tr>
		<tr>
			<td colSpan="2">
				<p><asp:label id="MessageLabel" runat="server" ForeColor="Red" EnableViewState="False"></asp:label></p>
			</td>
		</tr>
		<tr>
			<td colSpan="2"><asp:validationsummary id="UserValidationSummary" runat="server"></asp:validationsummary></td>
		</tr>
	</table>
</form>
