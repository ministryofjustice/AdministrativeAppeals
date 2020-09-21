<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AdminNav.ascx.vb" Inherits="OSSCSC.Web.AdminNav" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<p>
	[&nbsp;<asp:HyperLink id="hrefSearch" text="Search" runat="server" NavigateUrl="../Admin/Default.aspx">Search</asp:HyperLink>&nbsp;] 
	[&nbsp;<asp:HyperLink id="hrefCreate" text="Create" runat="server" NavigateUrl="../Admin/Edit.aspx?id=0">Create</asp:HyperLink>&nbsp;] 
	[&nbsp;<asp:HyperLink id="hrefUsers" text="Users" NavigateUrl="../Admin/EditUsers.aspx" runat="server"></asp:HyperLink>&nbsp;] 
	[&nbsp;<asp:HyperLink id="hrefCategories" text="Categories" NavigateUrl="../Admin/EditCategory.aspx" runat="server"></asp:HyperLink>&nbsp;] 
	[&nbsp;<asp:HyperLink id="hrefCommissioners" text="Commissioners" NavigateUrl="../Admin/Commissioners.aspx"
		runat="server"></asp:HyperLink>&nbsp;] [&nbsp;<asp:LinkButton id="lnkSignOut" text="Sign Out" runat="server"></asp:LinkButton>&nbsp;]
</p>
