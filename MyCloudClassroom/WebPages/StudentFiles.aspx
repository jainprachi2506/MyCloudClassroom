<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentFiles.aspx.cs" Inherits="MyCloudClassroom.WebPages.StudentFiles"%>

<asp:Content ID="content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
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
							IFNULL(Comments,'') Comments
							FROM mccdb.course 
							INNER JOIN mccdb.resource ON course.CourseId = resource.CourseId
							INNER JOIN mccdb.usercourse on usercourse.CourseId = course.CourseId
                            INNER JOIN mccdb.user on resource.uploadedby = user.UserId
							LEFT OUTER JOIN mccdb.resourcefeedback ON mccdb.resource.resourceid = mccdb.resourcefeedback.resourceid
                            WHERE usercourse.UserId=@UserId AND (UserType=1 OR user.UserId=@UserId)">
                    <SelectParameters>
        <asp:SessionParameter
            Name="UserId"
            SessionField="UserId" />
        </SelectParameters>
        </asp:SqlDataSource>
        <asp:Label ID="lblpath" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand"
            OnRowDataBound="GridView1_RowDataBound" EmptyDataText="No resources found.">
            <Columns>
                <asp:BoundField DataField="CourseId"  HeaderText="Course Id" />
                <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName"/>
                <asp:BoundField DataField="ResourceId" HeaderText="Resource Id"/>
                <asp:BoundField DataField="ResourceType" HeaderText="Resource Type" SortExpression="ResourceType" />
                <asp:BoundField DataField="ResourceFile" HeaderText="Resource File"/>
                <asp:ButtonField ButtonType="Link" DataTextField="ResourceName" HeaderText="Resource" SortExpression="ResourceType" CommandName="getURL"/>
                <asp:TemplateField HeaderText="Grade">
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
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server"  OnTextChanged="txtComments_TextChanged" AutoPostBack="true"></asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    </asp:Content>

