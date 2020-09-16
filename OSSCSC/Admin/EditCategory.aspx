<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditCategory.aspx.vb" Inherits="DCA.TribunalsService.Ossc.Web.EditCategory" %>
<%@ Register TagPrefix="uc1" TagName="AdminNav" Src="../UserControls/AdminNav.ascx" %>
<%@ Register TagPrefix="trans" Namespace="DCA.TribunalsService.Web.UI.Controls" Assembly="DCA.TribunalsService.Web.UI.Controls" %>
<h1>Category Management</h1>
<form id=Form1 method=post runat="server">
<table >
  <tr>
    <td colspan="2" align="right"><uc1:adminnav id=AdminNav1 runat="server"></uc1:adminnav></td></tr>
  <tr>
	<td valign="top"><img src="../images/icon_tip.gif"></td>
	<td valign="top" class="smaller">Tip: Click on a Category description to view the Subcategories currently assigned to it.
									Categories with related Subcategories cannot be deleted.</td>
  </tr>
  <tr>
    <td align=right colspan="2"><p><asp:linkbutton id=btnAddNew runat="server">Add New Category</asp:linkbutton></p></td></tr>
  <tr>
    <td colspan="2"><trans:groupedgrid id=CatDataGrid runat="server" width="100%" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridLine" CssClass="search-results-table percent80" Border="0" AllowRowHighlighting="True" AllowCustomSorting="False" AutoGenerateColumns="False" CellPadding="2">
					<ItemStyle CssClass="GridLine"></ItemStyle>
					<HeaderStyle CssClass="GridHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn Visible="False" HeaderText="ID">
							<ItemTemplate>
								<asp:label id=CatIDLabel runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Num") %>'>
								</asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Description">
							<ItemTemplate>
								<asp:Label id=DescriptionLabel runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=DescriptionTextBox runat="server" Width="280px" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
								</asp:TextBox>
								<asp:RequiredFieldValidator id="DescriptionReqFieldValidator" runat="server" ErrorMessage="Description cannot be empty." ControlToValidate="DescriptionTextBox">*</asp:RequiredFieldValidator>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit">
							<itemstyle HorizontalAlign="Center" />					
						</asp:EditCommandColumn>
						<asp:ButtonColumn Text="Delete" CommandName="Delete">
							<itemstyle HorizontalAlign="Center" />
						</asp:ButtonColumn>
					</Columns>
				</trans:groupedgrid></td></tr>
  <tr>
    <td colspan="2"><asp:label id=MessageLabel runat="server" EnableViewState="False" ForeColor="Red"></asp:label></td></tr>
  <tr>
    <td colspan="2"><asp:validationsummary id=CategoryValidationSummary runat="server"></asp:validationsummary></td></tr>
 </table>
 </form>
 <script language=javascript>
function RowOnClick(id)
{
	document.location.href='editsubcategory.aspx?id=' + id;
}
</script>

