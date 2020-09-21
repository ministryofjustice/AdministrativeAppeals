<%@ Page Language="vb" AutoEventWireup="false" Codebehind="View.aspx.vb" Inherits="OSSCSC.Web.View" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../UserControls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../UserControls/Footer.ascx" %>
<style type="text/css">
.highlight { FONT-WEIGHT: bold; BACKGROUND: yellow; COLOR: black; TEXT-DECORATION: none }
</style>
<uc1:Header id="Header1" runat="server"></uc1:Header>
<h1>Decision Summary Information</h1>
<form id="form1" runat="server">
	<p><asp:hyperlink id="hrefResults" runat="server" title="Back to Results">Back to Results</asp:hyperlink>&nbsp;|
		<a href="default.aspx" title="Search Again">Search Again</a>&nbsp;| <a href="default.aspx" title="Most Recent Decisions">
			Most Recent Decisions</a>
	</p>
	<table class="form-table percent100">
		<tr>
			<th>
				Neutral Citation Number:</th>
			<td><asp:literal id="litNCNNumber" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Reported Number:</th>
			<td><asp:literal id="litReportedNumber" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				File Number:</th>
			<td><asp:literal id="litFileNo" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Appellant:
			</th>
			<td><asp:literal id="litClaimant" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Respondent:
			</th>
			<td><asp:literal id="litRespondent" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Judge/Commissioner:
			</th>
			<td><asp:literal id="litCommissioners" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Date Of Decision:
			</th>
			<td><asp:literal id="litDate" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Date Added:
			</th>
			<td><asp:literal id="litDateAdded" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Main Category:
			</th>
			<td><asp:literal id="litCategory" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Main Subcategory:
			</th>
			<td><asp:literal id="litSubcategory" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Secondary Category:
			</th>
			<td><asp:literal id="litSecondaryCategory" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Secondary Subcategory:
			</th>
			<td><asp:literal id="litSecondarySubcategory" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Notes:
			</th>
			<td><asp:literal id="litNotes" runat="server" EnableViewState="False"></asp:literal></td>
		</tr>
		<tr>
			<th>
				Decision(s) to Download:
			</th>
			<td><asp:placeholder id="phLinks" runat="server"></asp:placeholder></td>
		</tr>
	</table>
</form>
<br>
<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
