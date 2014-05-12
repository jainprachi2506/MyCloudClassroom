<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyFiles.aspx.cs" Inherits="MyCloudClassroom.WebPages.MyFiles"%>
<asp:Content ID="content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    <div>
        <asp:Image ID="image" runat="server" ImageUrl="~/Images/myfile.jpg" Height="146px" Width="1008px" /> 
    </div>
    <div>
        
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MCCdbConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:MCCdbConnectionString.ProviderName %>" 
            SelectCommand="SELECT course.Courseid,
                                    course.CourseName, 
                                    CASE resource.ResourceType
									WHEN 0 THEN 'Lectures'
									WHEN 1 THEN 'Assignments'
									ELSE 'References'
									END as ResourceType,
                            resource.ResourceId,
							resource.ResourceName,
							resource.ResourceFile,
							IFNULL(Grade,'') Grade,
							IFNULL(Comments,'') Comments,
                            user.nameuser
							FROM mccdb.course 
							INNER JOIN mccdb.resource ON course.CourseId = resource.CourseId
							INNER JOIN mccdb.usercourse on usercourse.CourseId = course.courseId
                            INNER JOIN mccdb.user ON resource.uploadedby = user.userid
							LEFT OUTER JOIN mccdb.resourcefeedback ON mccdb.resource.resourceid = mccdb.resourcefeedback.resourceid
                            WHERE usercourse.UserId=@UserId">
                    <SelectParameters>
        <asp:SessionParameter
            Name="UserId"
            SessionField="UserId" />
        </SelectParameters>
        </asp:SqlDataSource>
        <asp:Label ID="lblpath" runat="server"></asp:Label>
        <table border="0" style="border: medium double #0099FF; width: 1007px;">
            <tr><td>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand"
            OnRowDataBound="GridView1_RowDataBound" EmptyDataText="No resources found." BorderStyle="Groove" Width="1000px">
            <AlternatingRowStyle BackColor="#3399FF" BorderColor="#0033CC" BorderStyle="Groove" />
            <Columns>
                <asp:BoundField DataField="CourseId"  HeaderText="Course Id" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue" />
                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue"/>
                <asp:BoundField DataField="ResourceId" HeaderText="Resource Id" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue"/>
                <asp:BoundField DataField="ResourceType" HeaderText="Resource Type" SortExpression="ResourceType" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue" />
                <asp:BoundField DataField="ResourceFile" HeaderText="Resource File" HeaderStyle-Font-Size HeaderStyle-ForeColor="MidnightBlue"/>
                <asp:ButtonField ButtonType="Link" DataTextField="ResourceName" HeaderText="Resource" SortExpression="ResourceType" CommandName="getURL" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue"/>
                <asp:TemplateField HeaderText="Grade" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue">
                    <ItemTemplate>
                        <asp:DropDownList ID="drpGrades" runat="server" OnSelectedIndexChanged="drpGrades_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                            <asp:ListItem>C</asp:ListItem>
                            <asp:ListItem>D</asp:ListItem>
                        </asp:DropDownList>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue">
                    <ItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server"  OnTextChanged="txtComments_TextChanged" AutoPostBack="true"></asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nameuser" HeaderText="Uploaded by" HeaderStyle-Font-Italic HeaderStyle-ForeColor="MidnightBlue"/>
            </Columns>
        </asp:GridView>
</td></tr>
            </table>
    </div>
      </asp:Content>

