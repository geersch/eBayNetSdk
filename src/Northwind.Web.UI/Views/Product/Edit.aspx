<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Web.UI.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="ProductName">Name:</label>
                <%= Html.TextBox("ProductName", Model.ProductName) %>
                <%= Html.ValidationMessage("ProductName", "*") %>
            </p>
            <p>
                <label for="SupplierID">Supplier:</label>
                <%= Html.DropDownList("SupplierID", (IEnumerable<SelectListItem>) ViewData["suppliers"], "") %>
                <%= Html.ValidationMessage("SupplierID", "*") %>
            </p>
            <p>
                <label for="CategoryID">Category:</label>
                <%= Html.DropDownList("CategoryID", (IEnumerable<SelectListItem>) ViewData["categories"], "")%>
                <%= Html.ValidationMessage("CategoryID", "*") %>
            </p>
            <p>
                <label for="QuantityPerUnit">Quantity Per Unit:</label>
                <%= Html.TextBox("QuantityPerUnit", Model.QuantityPerUnit) %>
                <%= Html.ValidationMessage("QuantityPerUnit", "*") %>
            </p>
            <p>
                <label for="UnitPrice">UnitPrice:</label>
                <%= Html.TextBox("UnitPrice", String.Format("{0:F}", Model.UnitPrice)) %>
                <%= Html.ValidationMessage("UnitPrice", "*") %>
            </p>
            <p>
                <label for="UnitsInStock">Units In Stock:</label>
                <%= Html.TextBox("UnitsInStock", Model.UnitsInStock) %>
                <%= Html.ValidationMessage("UnitsInStock", "*") %>
            </p>
            <p>
                <label for="ReorderLevel">Reorder Level:</label>
                <%= Html.TextBox("ReorderLevel", Model.ReorderLevel) %>
                <%= Html.ValidationMessage("ReorderLevel", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

