<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseRegistration.aspx.cs" Inherits="MyCloudClassroom.WebPages.CourseRegistration" %>

<asp:Content ID="content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    <div>
        <asp:Image ID="image1" runat="server" ImageUrl="~/Images/5130183.jpg" Height="204px" Width="1054px" />
    </div>
    <div>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MCCdbConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:MCCdbConnectionString.ProviderName %>" 
            SelectCommand="SELECT distinct course.Courseid,
                                    course.CourseName
							FROM course 
							INNER JOIN usercourse ON course.CourseId = usercourse.CourseId
                            WHERE UserId=@UserId">
                    <SelectParameters>
        <asp:SessionParameter
            Name="UserId"
            SessionField="UserId" />
        </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MCCdbConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:MCCdbConnectionString.ProviderName %>" 
            SelectCommand="SELECT distinct course.Courseid,
                                    course.courseName
							FROM course">
            </asp:SqlDataSource>
        <table style="width: 100%">
            <tr><td colspan="3">
        <asp:Label ID="Label1" runat="server" Text="Current Courses:" Font-Bold="true" Font-Italic="true" Font-Underline="true" ForeColor="MidnightBlue"></asp:Label>
   
                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="true" EmptyDataText="No courses found." Width="100%">
                </asp:GridView>
                </td></tr>
            <tr><td>
        <asp:Label ID="Label2" runat="server" Text="Enroll into a new course"></asp:Label>
                </td><td>
        <asp:DropDownList ID="drpCourse" DataTextField="CourseName" DataValueField="Courseid" runat="server" DataSourceID="SqlDataSource2"></asp:DropDownList>
        </td><td>
                    <asp:Button ID="Button1" runat="server" Text="Enroll" OnClick="Button1_Click"/>
            </td></tr></table>
        <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false"></asp:Label>
    </div>
     </asp:Content>
