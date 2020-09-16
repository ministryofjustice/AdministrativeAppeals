<%@ Register TagPrefix="uc1" TagName="AdminNav" Src="../UserControls/AdminNav.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Default.aspx.vb" Inherits="DCA.TribunalsService.Ossc.Web._DefaultAdmin" %>
<%@ Register TagPrefix="tt" Namespace="DCA.TribunalsService.Ossc.Web" Assembly="DCA.TribunalsService.Ossc.Web" %>
<%@ Register TagPrefix="trans" Namespace="DCA.TribunalsService.Web.UI.Controls" Assembly="DCA.TribunalsService.Web.UI.Controls" %>
<script type="text/javascript">
function populate(catSelect,formName,subSelectName,selected)
{
    criteria.Notes = String.Empty
    subSelect = eval("document.forms." + formName + "." + subSelectName);
    subSelect.options.length = 0;
    var catNum = catSelect.options[selected].value;
    
    if (subTextArray[catNum] == null)
    {
        var noneOption = new Option("-- No Subcategories --", "");
        subSelect.options[0] = noneOption;
        return;
    }

    var allOption = new Option("-- Choose a Subcategory --", "");
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
<h1>Admin Decisions</h1>
<form id="Form1" method="post" runat="server">
	<table>
		<tbody>
			<tr>
				<td colSpan="4">
					<div><uc1:adminnav id="AdminNav1" runat="server"></uc1:adminnav></div>
				</td>
			</tr>
			<tr>
				<td colSpan="4">
					<p>[<A href="default.aspx">Search Again</A>]&nbsp; [<A href="default.aspx">Most Recent 
							Decisions</A>]
					</p>
				</td>
			</tr>
		</tbody></table>
	<table class="form-table percent100">
		<tbody>
			<tr>
				<th>
					<tt:labelforcontrol id="Labelforcontrol1" runat="server" Control="drpCategory" Text="Category:"></tt:labelforcontrol></th>
				<td><asp:dropdownlist id="drpCategory" runat="server"></asp:dropdownlist></td>
			</tr>
			<tr>
				<th>
					<tt:labelforcontrol id="Labelforcontrol2" runat="server" Control="drpSubcategory" Text="SubCategory:"></tt:labelforcontrol></th>
				<td><asp:dropdownlist id="drpSubcategory" runat="server" EnableViewState="False">
						<asp:ListItem Value="-1">-- Choose a Category first --</asp:ListItem>
					</asp:dropdownlist></td>
			</tr>
			<tr>
				<th>
					<tt:labelforcontrol id="Labelforcontrol3" runat="server" Control="txtDecisionDate" Text="Decision Date:"></tt:labelforcontrol></th>
				<td><tt:htmldatecontrol id="txtDecisionDate" runat="server" maxlength="10"></tt:htmldatecontrol></td>
			</tr>
			<tr>
				<th>
					<tt:labelforcontrol id="Labelforcontrol8" runat="server" Control="txtYear" Text="File No.:"></tt:labelforcontrol></th>
				<td><asp:textbox id="txtPrefix" runat="server" maxlength="5" cssclass="percent15"></asp:textbox>&nbsp;
					<asp:textbox id="txtCase" runat="server" maxlength="5" cssclass="percent15"></asp:textbox>&nbsp;
					<asp:textbox id="txtYear" runat="server" maxlength="4" cssclass="percent15"></asp:textbox></td>
			</tr>
			<tr>
				<th>
					<tt:labelforcontrol id="Labelforcontrol4" runat="server" Control="txtFromDate" Text="From Date:"></tt:labelforcontrol></th>
				<td><tt:htmldatecontrol id="txtFromDate" runat="server" maxlength="10"></tt:htmldatecontrol></td>
				</tr>
			<tr>
			<th>
					<tt:labelforcontrol id="Labelforcontrol5" runat="server" Control="txtToDate" Text="To Date:"></tt:labelforcontrol></th>
				<td><tt:htmldatecontrol id="txtToDate" runat="server" maxlength="10"></tt:htmldatecontrol></td>
			</tr>
			<tr>
				<th>
					<tt:labelforcontrol id="Labelforcontrol6" runat="server" Control="txtClaimant" Text="Claimant:"></tt:labelforcontrol></th>
				<td><asp:textbox id="txtClaimant" runat="server"></asp:textbox></td>
				</tr>
			<tr>
			<th>
					<tt:labelforcontrol id="Labelforcontrol7" runat="server" Control="drpCommissioner" Text="Judge/Commissioner:"></tt:labelforcontrol></th>
				<td><asp:dropdownlist id="drpCommissioner" runat="server" EnableViewState="False"></asp:dropdownlist></td>
			</tr>
			<tr>
				<th>
					<tt:labelforcontrol id="Labelforcontrol9" runat="server" Control="txtReported1" Text="Reported No.:"></tt:labelforcontrol></th>
				<td colspan="3">&nbsp;R&nbsp;(&nbsp;<asp:textbox id="txtReported1" runat="server" maxlength="3" cssclass="percent15"></asp:textbox>&nbsp;)&nbsp;
					<asp:textbox id="txtReported2" runat="server" maxlength="3" cssclass="percent15"></asp:textbox>&nbsp;/&nbsp;
					<asp:textbox id="txtReported3" runat="server" maxlength="2" cssclass="percent15"></asp:textbox></td>
			</tr>
			<TR>
				<TH>
					<tt:labelforcontrol id="Labelforcontrol10" runat="server" Text="Neutral &#13;&#10;&#9;&#9;&#9;&#9;&#9;Citation Number"
						Control="txtReported1"></tt:labelforcontrol></TH>
				<TD colspan="3">&nbsp;[&nbsp;
					<asp:DropDownList id="drpNCNYear" runat="server"></asp:DropDownList>&nbsp;] 
					UKUT&nbsp;
					<asp:DropDownList id="drpNCNCitation" runat="server"></asp:DropDownList>&nbsp;(AAC)</TD>
			</TR>
			<tr>
				<td colSpan="4">
					<div align="right"><asp:button id="btnSearch" runat="server" Text="Search >>"></asp:button></div>
				</td>
			</tr>
		</tbody>
	</table>
	<trans:groupedgrid id="DecisionGrid" runat="server" EnableViewState="False" AutoGenerateColumns="False"
		AllowCustomSorting="True" CssClass="search-results-table percent100" AllowRowHighlighting="True"
		RemoveRedundantBorders="False" ReorganiseItemStyles="True" UseAccessibleHeader="True">
		<Columns>
			<asp:BoundColumn DataField="decision_datetime" SortExpression="decision_datetime" HeaderText="Date"></asp:BoundColumn>
			<asp:BoundColumn DataField="prefix" SortExpression="file_no_1" HeaderText="File No."></asp:BoundColumn>
			<asp:BoundColumn DataField="category" SortExpression="category" HeaderText="Category"></asp:BoundColumn>
			<asp:BoundColumn DataField="subcategory" SortExpression="subcategory" HeaderText="Subcategory"></asp:BoundColumn>
			<asp:BoundColumn DataField="is_published" SortExpression="is_published" HeaderText="Published"></asp:BoundColumn>
		</Columns>
	</trans:groupedgrid><trans:pager id="PagerControl" runat="server" cssclass="form-table percent80"></trans:pager>
</form>
<script type="text/javascript">
function RowOnClick(id)
{
	document.location.href='edit.aspx?id=' + id;
}
</script>
