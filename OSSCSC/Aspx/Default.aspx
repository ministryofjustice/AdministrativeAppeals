<%@ Page Language="vb" AutoEventWireup="false" codebehind="Default.aspx.vb" Inherits="OSSCSC.Web.Search" EnableEventValidation="false" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../UserControls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../UserControls/Footer.ascx" %>
<%@ Register TagPrefix="tt" Namespace="OSSCSC.Web" Assembly="OSSCSC" %>
<%@ Register TagPrefix="trans" Namespace="DCA.TribunalsService.Web.UI.Controls" Assembly="DCA.TribunalsService.Web.UI.Controls" %>
<uc1:Header id="Header1" runat="server"></uc1:Header>
<script src="../Javascript/PopulateSubcategory.js" type="text/javascript"></script>
<h1>Search For Decisions</h1>

<h3>This website includes decisions prior to January 2016. Details of newer cases can be found at the following: <a href="https://www.gov.uk/administrative-appeals-tribunal-decisions">https://www.gov.uk/administrative-appeals-tribunal-decisions</a></h3>

<form id="Form1" method="post" runat="server">
	<p><A href="default.aspx">Search Again</A>&nbsp;|&nbsp;<A href="default.aspx">Most 
			Recent Decisions</A>
	</p>
	<table class="form-table percent100">
		<tr>
			<th>
				<tt:labelforcontrol id="Labelforcontrol1" runat="server" Control="drpCategory" Text=""></tt:labelforcontrol>Category</th>
			<td><asp:dropdownlist id="drpCategory" runat="server"></asp:dropdownlist></td>
		</tr>
		<tr>
			<th>
				<tt:labelforcontrol id="Labelforcontrol2" runat="server" Control="drpSubcategory" Text=""></tt:labelforcontrol>SubCategory</th>
			<td><asp:dropdownlist id="drpSubcategory" runat="server" EnableViewState="False">
					<asp:ListItem Value="-1">-- Choose a Category first --</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<!--
				<tr>
					<th>
						<tt:labelforcontrol id="Labelforcontrol3" runat="server" Control="txtDecisionDate" Text=""></tt:labelforcontrol>Decision 
						Date</th>
					<td><tt:htmldatecontrol id="txtDecisionDate" runat="server" maxlength="10"></tt:htmldatecontrol></td>
				</tr>
				-->
		<tr>
			<th>
				<tt:labelforcontrol id="Labelforcontrol8" runat="server" Control="txtYear" Text=""></tt:labelforcontrol>File 
				Number</th>
			<td><asp:textbox id="txtPrefix" runat="server" maxlength="5" cssclass="percent15"></asp:textbox>&nbsp;
				<asp:textbox id="txtCase" runat="server" maxlength="5" cssclass="percent15"></asp:textbox>&nbsp;
				<asp:textbox id="txtYear" runat="server" maxlength="4" cssclass="percent15"></asp:textbox></td>
		</tr>
		<tr>
			<th>
				Decision Date</th>
			<td><tt:htmldatecontrol id="txtFromDate" runat="server" maxlength="10"></tt:htmldatecontrol>
				to
				<tt:htmldatecontrol id="txtToDate" runat="server" maxlength="10"></tt:htmldatecontrol></td>
		</tr>
		<tr>
			<th>
				Date Added</th>
			<td><tt:htmldatecontrol id="txtFromDateAdded" runat="server" maxlength="10"></tt:htmldatecontrol>
				to
				<tt:htmldatecontrol id="txtToDateAdded" runat="server" maxlength="10"></tt:htmldatecontrol></td>
		</tr>
		<TR>
			<TH>
				Appellant</TH>
			<TD>
				<asp:textbox id="txtClaimant" runat="server"></asp:textbox></TD>
		</TR>
		<tr>
			<TH>
				Respondent</TH>
			<TD>
				<asp:textbox id="txtRespondent" runat="server"></asp:textbox></TD>
		</tr>
		<TR>
			<TH>
				<tt:labelforcontrol id="Labelforcontrol7" runat="server" Text="" Control="drpCommissioner"></tt:labelforcontrol>Judge/Commissioner</TH>
			<TD>
				<asp:dropdownlist id="drpCommissioner" runat="server" EnableViewState="False"></asp:dropdownlist></TD>
		</TR>
		<TR>
			<TH>
				<tt:labelforcontrol id="Labelforcontrol4" runat="server" Text="" Control="txtReported1"></tt:labelforcontrol>Neutral 
				Citation Number (NCN)</TH>
			<TD>[
				<asp:DropDownList id="drpNCNYear" runat="server"></asp:DropDownList>] 
				UKUT&nbsp;
				<asp:DropDownList id="drpNCNCitation" runat="server"></asp:DropDownList>&nbsp;(AAC)</TD>
		</TR>
		<TR>
			<TH>
				<tt:labelforcontrol id="Labelforcontrol9" runat="server" Text="" Control="txtReported1"></tt:labelforcontrol>Reported 
				Number</TH>
			<TD>&nbsp;R&nbsp;(&nbsp;
				<asp:textbox id="txtReported1" runat="server" cssclass="percent15" maxlength="3"></asp:textbox>&nbsp;)&nbsp;
				<asp:textbox id="txtReported2" runat="server" cssclass="percent15" maxlength="3"></asp:textbox>&nbsp;/&nbsp;
				<asp:textbox id="txtReported3" runat="server" cssclass="percent15" maxlength="2"></asp:textbox></TD>
		</TR>
		<TR>
			<TH>
				<tt:labelforcontrol id="Labelforcontrol10" runat="server" Text="" Control="txtNotes"></tt:labelforcontrol>Notes
			</TH>
			<TD>
				<asp:textbox id="txtNotes" runat="server" maxlength="50"></asp:textbox></TD>
		</TR>
		<TR>
			<TD colSpan="4">
				<DIV align="right">
					<asp:button id="btnSearch" runat="server" Text="Search >>"></asp:button></DIV>
			</TD>
		</TR>
	</table>
	<trans:groupedgrid id="DecisionGrid" runat="server" EnableViewState="False" UseAccessibleHeader="True"
		AutoGenerateColumns="False" AllowCustomSorting="True" CssClass="search-results-table percent100"
		AllowRowHighlighting="True" HeaderStyle-CssClass="GridHeader" RemoveRedundantBorders="False" ReorganiseItemStyles="True">
		<HeaderStyle CssClass="GridHeader"></HeaderStyle>
		<Columns>
			<asp:BoundColumn DataField="decision_datetime" SortExpression="decision_datetime" HeaderText="Decision Date"></asp:BoundColumn>
			<asp:BoundColumn DataField="prefix" SortExpression="file_no_1" HeaderText="File No."></asp:BoundColumn>
			<asp:BoundColumn DataField="ncnyear" SortExpression="ncn_year" HeaderText="NCN"></asp:BoundColumn>
			<asp:BoundColumn DataField="category" SortExpression="category" HeaderText="Category"></asp:BoundColumn>
			<asp:BoundColumn DataField="subcategory" SortExpression="subcategory" HeaderText="Subcategory"></asp:BoundColumn>
			<asp:TemplateColumn HeaderText="Decision Added">
				<ItemTemplate>
					<asp:Label id="createddate" runat="server" text='<%# OSSCSC.Business.Business.Utility.GetDate(DataBinder.Eval(Container, "DataItem.created_datetime")) %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</trans:groupedgrid><trans:pager id="PagerControl" runat="server" cssclass="form-table percent100" PageSize="10"></trans:pager>
	<br>
</form>
<script language="javascript" type="text/javascript">
function RowOnClick(id)
{
	document.forms[0].action = "view.aspx?id=" + id;
	//document.forms[0].removeChild(document.forms[0].__VIEWSTATE);
	removeViewState();
	document.forms[0].submit();
	//document.location.href='view.aspx?id=' + id;
}

// javascript function to remove viewstate form element.
	function removeViewState() {
    var viewstate = document.getElementById('__VIEWSTATE');
    viewstate.parentNode.removeChild(viewstate);
}
</script>
<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
