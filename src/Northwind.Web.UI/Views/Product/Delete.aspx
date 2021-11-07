<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Web.UI.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>
    
    <%= Html.ValidationSummary("Deletion was unsuccessful.") %>
    

    <% using (Html.BeginForm()) {%>
    
        <fieldset>
    
        <p>Are you sure you want to delete the product <strong><%= Html.Encode(Model.ProductName) %></strong> (<%= Html.Encode(Model.ProductID) %>)?</p>
    
        <input type="submit" value="Delete" />
        
        </fieldset>
    
    <% } %>
    
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

