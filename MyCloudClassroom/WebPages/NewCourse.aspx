<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewCourse.aspx.cs" Inherits="MyCloudClassroom.WebPages.NewCourse" %>
<asp:Content ID="content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    <div>
        <asp:Label ID="lblCourseName" runat="server" Text="Course Name"></asp:Label><asp:TextBox ID="txtCourseName" runat="server"></asp:TextBox>
        <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label><asp:Calendar ID="calStartDate" runat="server"></asp:Calendar>
        <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label><asp:Calendar ID="calEndDate" runat="server"></asp:Calendar>
        <asp:Label ID="lblClassOnDays" runat="server" Text="Class on Days"></asp:Label>
            <asp:CheckBoxList ID="chkDays" runat="server">
                <asp:ListItem Text="Monday" Value="M"></asp:ListItem>
                <asp:ListItem Text="Tuesday" Value="T"></asp:ListItem>
                <asp:ListItem Text="Wednesday" Value="W"></asp:ListItem>
                <asp:ListItem Text="Thursday" Value="Th"></asp:ListItem>
                <asp:ListItem Text="Friday" Value="F"></asp:ListItem>
            </asp:CheckBoxList>
        <asp:Label ID="lblStartTime" runat="server" Text="Start Time:"></asp:Label>
            <asp:Label ID="lblHours" runat="server" Text="HH"></asp:Label><asp:TextBox ID="txtStartHH" runat="server"></asp:TextBox>
            <asp:Label ID="lblMin" runat="server" Text="MM"></asp:Label><asp:TextBox ID="txtStartMM" runat="server"></asp:TextBox>
        <asp:Label ID="lblEndTime" runat="server" Text="End Time:"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="HH"></asp:Label><asp:TextBox ID="txtEndHH" runat="server"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="MM"></asp:Label><asp:TextBox ID="txtEndMM" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
    </div>
    </asp:Content>
