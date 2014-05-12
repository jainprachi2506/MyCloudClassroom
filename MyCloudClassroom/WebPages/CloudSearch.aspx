<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CloudSearch.aspx.cs" Inherits="MyCloudClassroom.WebPages.CloudSearch" %>
<asp:Content ID="content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    <div>
        <asp:Label runat="server" Text="Please click on the following link to Search the documents. You will be redirected to AWS Cloud Search."></asp:Label>
        <p></p>
        <asp:LinkButton ID="LinkButton1" runat="server" Text="AWS Search" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
    </div>
    <div>
        <asp:Image ID="image1" runat="server" ImageUrl="~/Images/164469574.jpg" Height="567px" Width="1009px" />
    </div>
    </asp:Content>
