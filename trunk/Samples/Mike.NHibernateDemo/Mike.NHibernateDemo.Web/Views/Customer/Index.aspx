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
    <table>
    <% foreach (var order in Model.Orders)
       { %>
        <tr><td><%= order.OrderDate.ToShortDateString() %>
        <tr><td><table>
        <%
           foreach (var orderLine in order.OrderLines)
           { %>
                <tr>
                <td><%= orderLine.Product.Name %> </td>
                <td><%= orderLine.Quantity.ToString() %> </td>
                <td><%= orderLine.GetTotalPrice().ToString() %> </td>
                </tr>
          <% } %>      
            <tr>
                <td>Total</td>
                <td></td>
                <td><%= order.GetOrderTotal().ToString() %></td>
            </tr>
          </table>  
        </td></tr>
    <% } %>
    </table>
    <p>
        <%=Html.ActionLink("Create", "Create") %> |
        <%=Html.ActionLink("Add Widget", "AddWidget")%> |
    </p>

</asp:Content>

