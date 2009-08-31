<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mike.NHibernateDemo.Model.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Customer</h2>

    <p>
        Name:
        <%= Model.Name %>
    </p>
    <p>Orders:</p>
    <% foreach (var order in Model.Orders)
       { %>
        <%= order.OrderDate.ToShortDateString() %>
        
        <%
           foreach (var orderLine in order.OrderLines)
           { %>
                <%= orderLine.Product.Name %> Quantity: <%= orderLine.Quantity.ToString() %> Total: <%= orderLine.GetTotalPrice().ToString() %>
          <% } %>        
        
    <% } %>
    <p>
        <%=Html.ActionLink("Create", "Create", new { /* id=Model.PrimaryKey */ }) %> |
    </p>

</asp:Content>

