<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Commissioners.aspx.vb" Inherits="OSSCSC.Web.Commissioners"%>
<%@ Register TagPrefix="uc1" TagName="AdminNav" Src="../UserControls/AdminNav.ascx" %>
<%@ Register TagPrefix="trans" Namespace="DCA.TribunalsService.Web.UI.Controls" Assembly="DCA.TribunalsService.Web.UI.Controls" %>
<h1>Commissioner Management</h1>
<form id=Form1 method=post runat="server">
<table>
  <tr>
    <td colspan="2" align="right"><uc1:adminnav id=AdminNav1 runat="server"></uc1:adminnav></td></tr>
  <tr>
	<td valign="top"><img src="../images/icon_tip.gif"></td>
	<td valign="top" class="smaller">Tip: Commissioners can only be deleted if there are no Judgments
											related to that particular commissioner.</td>
  </tr>
  <tr>
    <td align=right colspan="2"><p><asp:linkbutton id=btnAddNew runat="server">Add New Commissioner</asp:linkbutton></p></td></tr>
  <tr>
    <td colspan="2"><p><asp:label id=MessageLabel runat="server" EnableViewState="False" ForeColor="Red"></asp:label></p></td></tr>
  <tr>
    <td colspan="2"><trans:groupedgrid id=CommDataGrid runat="server" width="100%" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridLine" CssClass="search-results-table percent80" Border="0" AllowRowHighlighting="True" AllowCustomSorting="False" AutoGenerateColumns="False" CellPadding="2">
					<ItemStyle CssClass="GridLine"></ItemStyle>
					<HeaderStyle CssClass="GridHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn Visible="False" HeaderText="ID">
							<ItemTemplate>
								<asp:label id=CommIDLabel runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'>
								</asp:label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Prefix">
							<ItemTemplate>
								<asp:Label id=PrefixLabel runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Prefix") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=PrefixTextBox runat="server" Width="180px" Text='<%# DataBinder.Eval(Container, "DataItem.Prefix") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Surname">
							<ItemTemplate>
								<asp:Label id="SurnameLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Surname") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="SurnameTextbox" runat="server" Width="180px" Text='<%# DataBinder.Eval(Container, "DataItem.Surname") %>'>
								</asp:TextBox>
								<asp:RequiredFieldValidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="Surname cannot be empty." ControlToValidate="SurnameTextBox">*</asp:RequiredFieldValidator>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Suffix">
							<ItemTemplate>
								<asp:Label id="SuffixLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Suffix") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="SuffixTextbox" runat="server" Width="180px" Text='<%# DataBinder.Eval(Container, "DataItem.Suffix") %>'>
								</asp:TextBox>
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
    <td colspan="2"><asp:validationsummary id=CommissionerValidationSummary runat="server"></asp:validationsummary></td></tr>
 </table>
 </form>
 <script language=javascript>
function RowOnClick(id)
{
	
}
</script>

