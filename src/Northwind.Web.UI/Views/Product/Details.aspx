<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Web.UI.Models.ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            <strong>ID:</strong>
            <%= Html.Encode(Model.ProductId) %>
        </p>
        <p>
            <strong>Name:</strong>
            <%= Html.Encode(Model.ProductName) %>
        </p>
        <p>
            <strong>Supplier:</strong>
            <%= Html.Encode(Model.Supplier) %>
        </p>
        <p>
            <strong>Category:</strong>
            <%= Html.Encode(Model.Category) %>
        </p>
        <p>
            <strong>Quantity Per Unit:</strong>
            <%= Html.Encode(Model.QuantityPerUnit)%>
        </p>
        <p>
            <strong>UnitPrice:</strong>
            <%= Html.Encode(String.Format("{0:F}", Model.UnitPrice)) %>
        </p>
        <p>
            <strong>Units In Stock:</strong>
            <%= Html.Encode(Model.UnitsInStock) %>
        </p>
        <p>
            <strong>Units On Order:</strong>
            <%= Html.Encode(Model.UnitsOnOrder) %>
        </p>
        <p>
            <strong>Reorder Level:</strong>
            <%= Html.Encode(Model.ReorderLevel) %>
        </p>
    </fieldset>
    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id = Model.ProductId }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

