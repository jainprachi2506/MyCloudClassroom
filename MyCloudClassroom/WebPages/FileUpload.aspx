<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="MyCloudClassroom.WebPages.FileUpload" %>
<asp:Content ID="content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="StyleSheet1.css" rel="stylesheet" type="text/css" />

    <div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MCCdbConnectionString %>" ProviderName="<%$ ConnectionStrings:MCCdbConnectionString.ProviderName %>" 
        SelectCommand="SELECT a.CourseId,CourseName FROM mccdb.course a JOIN mccdb.usercourse b ON a.Courseid=b.Courseid WHERE UserId=@UserId">
        <SelectParameters>
        <asp:SessionParameter
            Name="UserId"
            SessionField="UserId" />
        </SelectParameters>
    </asp:SqlDataSource>
        <asp:Panel ID="Panel1" runat="server" Direction="LeftToRight" Height="175px" Width="182px">
            <table>
                <tr>
                    <td>
            <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label></td>
                    <td>
            <asp:DropDownList ID="drpCourse" runat="server" DataSourceID="SqlDataSource1" DataTextField="coursename" DataValueField="courseid">
            </asp:DropDownList></td>
           </tr>
                <tr><td>
            <asp:Label ID="lblResourceType" runat="server" Text="Resource Type"></asp:Label></td>
                    <td>
            <asp:DropDownList ID="drpResourceType" runat="server">
                <asp:ListItem>Lectures</asp:ListItem>
                <asp:ListItem>Assignments</asp:ListItem>
                <asp:ListItem>References</asp:ListItem>
            </asp:DropDownList></td></tr>
            <tr><td>
            <asp:Label ID="lblResourceName" runat="server" Text="Name"></asp:Label></td>
                <td>
            <asp:TextBox ID="txtResourceName" runat="server"></asp:TextBox></td>

            </tr>
            <tr><td>
            <asp:Label ID="lblFile" runat="server" Text="File"></asp:Label>
                </td>
                <td>
            
            <asp:FileUpload ID="fileResource" runat="server" />
                    </td></tr>
                <tr><td>
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                    </td>
                    <td></td></tr>
            </table>
        </asp:Panel>
            <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false"></asp:Label>
    </div>
       </asp:Content>