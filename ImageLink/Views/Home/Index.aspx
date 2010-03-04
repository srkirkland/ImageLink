<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ImageLink.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
    
    <p>
        Plain Image Link: <%= Html.ImageLink("image.png", "alt text", "About") %>
    </p>
    <p>
        Image Link with html attributes:
        <%= Html.ImageLink("image.png", "alt text", "About", new {}, new {id = "about-link"}, new {id = "image-link"}) %>
    </p>
    
</asp:Content>
