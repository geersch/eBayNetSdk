<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ebay_Category>>" %>
<%@ Import Namespace="Northwind.Web.UI"%>
<%@ Import Namespace="Northwind.Web.UI.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Link Northwind Category To eBay Category
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LinkToeBayCategory</h2>
    
    <%= Html.TreeView("categories", Model, c => c.ChildCategories, c => Html.ActionLink(c.Name, "LinkToeBayCategory", new { id = ViewData["id"], ebaycategoryid = c.CategoryID }, null))%>

</asp:Content>
