<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="MyCloudClassroom.WebPages.FileUpload" %>
<asp:Content ID="content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="StyleSheet1.css" rel="stylesheet" type="text/css">
    <div>
        <asp:Image ID="image1" runat="server" ImageUrl="~/Images/myfile.jpg" Height="206px" Width="1046px" />
    </div>
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
          <asp:Table ID="Table1" runat="server" Width="314px">

              <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <div>
                        </div>
                    </asp:TableCell>
                  </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <div id="topMenuMiddlePan">
            <asp:Label ID="lblCourse" runat="server" Text="Course"></asp:Label></td>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
            <asp:DropDownList ID="drpCourse" runat="server" DataSourceID="SqlDataSource1" DataTextField="coursename" DataValueField="courseid">
            </asp:DropDownList></td>
                        </asp:TableCell>
                </asp:TableRow>
        <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <div id="topMenuMiddlePan">
            <asp:Label ID="lblResourceType" runat="server" Text="Resource Type"></asp:Label></td>
                   </div>
                    </asp:TableCell>
             <asp:TableCell runat="server">
            <asp:DropDownList ID="drpResourceType" runat="server">
                <asp:ListItem>Lectures</asp:ListItem>
                <asp:ListItem>Assignments</asp:ListItem>
                <asp:ListItem>References</asp:ListItem>
            </asp:DropDownList></td></tr>
            </asp:TableCell>
            </asp:TableRow>
              <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <div id="topMenuMiddlePan">
            <asp:Label ID="lblResourceName" runat="server" Text="Name"></asp:Label></td>
               </div>
                    </asp:TableCell>
                   <asp:TableCell runat="server">
            <asp:TextBox ID="txtResourceName" runat="server"></asp:TextBox></td>
</asp:TableCell>
                  </asp:TableRow>
              <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <div id="topMenuMiddlePan">
            <asp:Label ID="lblFile" runat="server" Text="File"></asp:Label>
                </div>
            </asp:TableCell>
                  <asp:TableCell runat="server">
            <asp:FileUpload ID="fileResource" runat="server" />
                </asp:TableCell>
                  </asp:TableRow>
              <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <div>
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                  </div>
                    </asp:TableCell></asp:TableRow></asp:Table>
            <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false"></asp:Label>
    </asp:Panel>
            </div>
    
       </asp:Content>