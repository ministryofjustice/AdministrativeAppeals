<%@ Register TagPrefix="uc1" TagName="AdminNav" Src="../UserControls/AdminNav.ascx" %>
<%@ Register TagPrefix="tt" Namespace="OSSCSC.Web" Assembly="OSSCSC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Edit.aspx.vb" Inherits="OSSCSC.Web.Edit" EnableEventValidation="false" %>
<script type="text/javascript">
function populate(catSelect,formName,subSelectName,selected)
{
    subSelect = eval("document.forms." + formName + "." + subSelectName);
    subSelect.options.length = 0;
    var catNum = catSelect.options[selected].value;

    if (subTextArray[catNum] == null)
    {
        var noneOption = new Option("-- No Subcategories --", "-1");
        subSelect.options[0] = noneOption;
        return;
    }

    var allOption = new Option("-- Choose a Subcategory --", "-1");
    subSelect.options[0] = allOption;
    for (var i = 1; i < subTextArray[catNum].length; i++)
    {
        // there might be blank ones, so skip them out
        if (subTextArray[catNum][i] != null)
        {
            var tmpOption = new Option(subTextArray[catNum][i], subValueArray[catNum][i]);
            subSelect.options[i] = tmpOption;
        }
    }
}
</script>
<h1>Create/Edit Decisions</h1>
<form id="Form1" method="post" runat="server">
	<table>
		<tr>
			<td>
				<div align="right"><uc1:adminnav id="AdminNav1" runat="server"></uc1:adminnav></div>
			</td>
		</tr>
	</table>
	<table class="form-table percent80">
		<tr>
			<th class="percent25">
				ID:</th>
			<td><asp:label id="lblID" runat="server"></asp:label></td>
		</tr>
		<tr>
			<th>
				Date Updated:</th>
			<td><asp:label id="lblUpdated" runat="server"></asp:label></td>
		</tr>
		<tr>
			<th>
				Date Created:</th>
			<td><asp:label id="lblCreated" runat="server"></asp:label></td>
		</tr>
		<tr>
			<th vAlign="top">
				Judge/Commissioner:</th>
			<td><asp:listbox id="drpCommissioner" runat="server" SelectionMode="Multiple" Rows="6"></asp:listbox></td>
		</tr>
		<tr>
			<th>
				Published:</th>
			<td><asp:checkbox id="chkIsPublished" runat="server"></asp:checkbox></td>
		</tr>
		<TR>
			<TH>
				<tt:labelforcontrol id="Labelforcontrol4" runat="server" Control="txtReported1" Text=""></tt:labelforcontrol>Neutral 
				Citation Number</TH>
			<TD>&nbsp;[&nbsp;
				<asp:dropdownlist id="drpNCNYear" runat="server"></asp:dropdownlist>&nbsp;]
				<asp:textbox id="txtNCNCode1" runat="server" Width="48px" maxlength="5" cssclass="percent15">UKUT</asp:textbox>&nbsp;
				<asp:dropdownlist id="drpNCNCitation" runat="server"></asp:dropdownlist>&nbsp;(
				<asp:textbox id="txtNCNCode2" runat="server" Width="40px" maxlength="5" cssclass="percent15">AAC</asp:textbox>)
				<asp:Label id="lblError" runat="server" ForeColor="Red"></asp:Label></TD>
		</TR>
		<tr>
			<th>
				File Number</th>
			<td><asp:textbox id="txtPrefix" runat="server" Width="50px" MaxLength="5"></asp:textbox><asp:textbox id="txtCaseNo" runat="server" Width="50px" MaxLength="5"></asp:textbox><asp:textbox id="txtYear" runat="server" Width="50px" MaxLength="4"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" ErrorMessage="File Prefix Required"
					ControlToValidate="txtPrefix" Display="Dynamic">File Prefix Required</asp:requiredfieldvalidator>&nbsp;&nbsp;
				<asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Year Required" ControlToValidate="txtYear"
					Display="Dynamic">Year Required</asp:requiredfieldvalidator>&nbsp;&nbsp;
				<asp:rangevalidator id="RangeValidator1" runat="server" ErrorMessage="Invalid Year" ControlToValidate="txtYear"
					Display="Dynamic" MinimumValue="1900" MaximumValue="2100">Invalid Year</asp:rangevalidator>&nbsp;&nbsp;
				<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Case No Required" ControlToValidate="txtCaseNo"
					Display="Dynamic">Case No Required</asp:requiredfieldvalidator></td>
		</tr>
		<tr>
			<th>
				Reported Number:</th>
			<td>&nbsp;R&nbsp;(&nbsp;
				<asp:textbox id="txtReportedNumber1" runat="server" Width="40px" MaxLength="3"></asp:textbox>&nbsp;)&nbsp;
				<asp:textbox id="txtReportedNumber2" runat="server" Width="40px" MaxLength="3"></asp:textbox>&nbsp;/&nbsp;
				<asp:textbox id="txtReportedNumber3" runat="server" Width="40px" MaxLength="2"></asp:textbox></td>
		</tr>
		<tr>
			<th>
				Date of Decision:</th>
			<td><tt:htmldatecontrol id="txtDecisionDate" runat="server" maxlength="10"></tt:htmldatecontrol><asp:comparevalidator id="CompareValidator1" runat="server" ErrorMessage="Invalid Date" ControlToValidate="txtDecisionDate"
					Display="Dynamic" EnableViewState="False" ToolTip="Invalid Date" Operator="DataTypeCheck" Type="Date">Invalid Date</asp:comparevalidator><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ErrorMessage="Date Required" ControlToValidate="txtDecisionDate"
					Display="Dynamic" ToolTip="Required Field">Date Required</asp:requiredfieldvalidator></td>
		</tr>
		<tr>
			<th vAlign="top">
				Appellant:</th>
			<td><asp:textbox id="txtClaimant" runat="server" Rows="2" MaxLength="5000" TextMode="MultiLine" Columns="30"></asp:textbox></td>
		</tr>
		<tr>
			<TH>
				<tt:labelforcontrol id="Labelforcontrol3" runat="server" Control="txtRespondent" Text=""></tt:labelforcontrol>Respondent</TH>
			<TD><asp:textbox id="txtRespondent" runat="server"></asp:textbox></TD>
		</tr>
		<tr>
			<th>
				Main Category (required):</th>
			<td></td>
		</tr>
		<tr>
			<th>
				Category:</th>
			<td><asp:dropdownlist id="drpMainCategory" runat="server"></asp:dropdownlist></td>
		</tr>
		<tr>
			<th>
				Subcategory</th>
			<td><asp:dropdownlist id="drpMainSubCategory" runat="server">
					<asp:ListItem Value="-1">-- Choose a Category first --</asp:ListItem>
				</asp:dropdownlist><asp:comparevalidator id="CompareValidator2" runat="server" ErrorMessage="Subcategory Required" ControlToValidate="drpMainSubCategory"
					Operator="GreaterThan" ValueToCompare="0">Subcategory Required</asp:comparevalidator></td>
		</tr>
		<tr>
			<th>
				Secondary Category (optional):</th>
			<td></td>
		</tr>
		<tr>
			<th>
				Category:</th>
			<td><asp:dropdownlist id="drpSecondaryCategory" runat="server"></asp:dropdownlist></td>
		</tr>
		<tr>
			<th>
				Subcategory:</th>
			<td><asp:dropdownlist id="drpSecondarySubCategory" runat="server">
					<asp:ListItem Value="-1">-- Choose a Category first --</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<th vAlign="top">
				Notes:</th>
			<td colSpan="2"><asp:textbox id="txtNotes" runat="server" Rows="5" MaxLength="5000" TextMode="MultiLine" Columns="50"></asp:textbox></td>
		</tr>
		</TR>
		<tr>
			<th>
				Current Decision Files:</th>
			<td><asp:placeholder id="phLinks" runat="server"></asp:placeholder></td>
		</tr>
		<tr>
			<th>
			</th>
			<td><input id="fileDecision" type="file" runat="server">&nbsp;&nbsp;
				<asp:linkbutton id="btnAdd" runat="server" Text="Add File"></asp:linkbutton></td>
		</tr>
		<tr>
			<td align="right" colSpan="2"><asp:button id="Button1" runat="server" Text="Submit"></asp:button></td>
		</tr>
		<tr>
			<td colSpan="2"></td>
		</tr>
	</table>
</form>
