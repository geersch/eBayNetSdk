<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Northwind.Web.UI.Models.ProductModel>>" %>

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
                Supplier
            </th>
            <th>
                Category
            </th>
            <th style="text-align: right">
                UnitPrice
            </th>
            <th>
                Quantity Per Unit
            </th>
            <th style="text-align: right">
                Units In Stock
            </th>
            <th style="text-align: right">
                Units On Order
            </th>
            <th style="text-align: right">
                Reorder Level
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ProductId }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.ProductId })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.ProductId }) %> |
                <%= Html.ActionLink("Publish On eBay", "PublishOnEBay", new { id=item.ProductId }) %>
            </td>
            <td align="center">
                <%= Html.Encode(item.ProductId) %>
            </td>
            <td>
                <%= Html.Encode(item.ProductName) %>
            </td>
            <td>
                <%= Html.Encode(item.Supplier) %>
            </td>
            <td>
                <%= Html.Encode(item.Category) %>
            </td>
            <td align="right">
                <%= Html.Encode(String.Format("{0:C}", item.UnitPrice)) %>
            </td>
            <td>
                <%= Html.Encode(item.QuantityPerUnit) %>
            </td>
            <td align="right">
                <%= Html.Encode(item.UnitsInStock) %>
            </td>
            <td align="right">
                <%= Html.Encode(item.UnitsOnOrder) %>
            </td>
            <td align="right">
                <%= Html.Encode(item.ReorderLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

