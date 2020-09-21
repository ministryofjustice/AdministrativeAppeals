<%@ Register TagPrefix="trans" Namespace="DCA.TribunalsService.Web.UI.Controls" Assembly="DCA.TribunalsService.Web.UI.Controls" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditSubCategory.aspx.vb" Inherits="OSSCSC.Web.EditSubCategory" %>
<%@ Register TagPrefix="uc1" TagName="AdminNav" Src="../UserControls/AdminNav.ascx" %>
<form id="Form1" method="post" runat="server">
<h1>Subcategory Management&nbsp;-&nbsp;
<asp:Label id=lblHead runat="server"></asp:Label></h1>
	<table>
		<tr>
			<td colspan="2" align="right"><uc1:adminnav id=AdminNav1 runat="server"></uc1:adminnav></td></tr>
		<tr>
			<td valign="top"><img src="../images/icon_tip.gif"></td>
			<td valign="top" class="smaller">Tip: Subcategories are listed here by category. They can only be deleted if there are no Judgments
											related to that Subcategory.</td>
		</tr>
		<tr>
			<td align=right colspan="2"><p><asp:linkbutton id=btnAddNew runat="server">Add New Subcategory</asp:linkbutton></p></td></tr>
		<tr>
			<td colspan="2">
				<trans:groupedgrid id="SubCatDataGrid" runat="server" CellPadding="2" AutoGenerateColumns="False" AllowCustomSorting="False"
					AllowRowHighlighting="True" Border="0" CssClass="search-results-table percent80" ItemStyle-CssClass="GridLine" HeaderStyle-CssClass="GridHeader" width="100%">
					<ItemStyle CssClass="GridLine"></ItemStyle>
					<HeaderStyle CssClass="GridHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn Visible="False" HeaderText="ID">
							<ItemTemplate>
								<asp:label id=SubCatIDLabel runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
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
						<asp:TemplateColumn HeaderText="Number">
							<ItemTemplate>
								<asp:Label id=NumberLabel runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Num") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=NumberTextBox runat="server" Width="280px" Text='<%# DataBinder.Eval(Container, "DataItem.Num") %>'></asp:TextBox>
								<asp:RequiredFieldValidator id="NumberRequiredfieldvalidator" runat="server" ErrorMessage="Number cannot be empty." ControlToValidate="NumberTextBox">*</asp:RequiredFieldValidator>
								
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit">
							<itemstyle HorizontalAlign="Center" />					
						</asp:EditCommandColumn>
						<asp:ButtonColumn Text="Delete" CommandName="Delete">
							<itemstyle HorizontalAlign="Center" />
						</asp:ButtonColumn>
					</Columns>
				</trans:groupedgrid>
			</td>
		</tr>
		<tr>
			<td colspan="2"><asp:Label id=MessageLabel runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></td>
		</tr>
		<tr>
			<td colspan="2"><asp:ValidationSummary id="SubcategoryValidationSummary" runat="server"></asp:ValidationSummary></td>
		</tr>
	</table>

</form>
