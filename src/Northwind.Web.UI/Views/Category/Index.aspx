<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Northwind.Web.UI.Models.CategoryModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table style="width: 100%;">
        <tr>
            <th>
                Action(s)
            </th>
            <th style="text-align: center">
                Id
            </th>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th>
                eBay Category
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>                
                <%= Html.ActionLink("Link To eBay Category", "EBayCategories", new { id = item.CategoryId })%>
            </td>
            <td align="center">
                <%= Html.Encode(item.CategoryId) %>
            </td>
            <td>
                <%= Html.Encode(item.CategoryName) %>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
            <td>
                <%= Html.Encode(item.EBayCategoryName) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

