<%@ Page Title="Register an external login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterExternalLogin.aspx.cs" Inherits="MyCloudClassroom.Account.RegisterExternalLogin" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<h3>Register with your <%: ProviderName %> account</h3>

    <asp:PlaceHolder runat="server">
        <div class="form-horizontal">
            <h4>Association Form</h4>
            <hr />
            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
            <p class="text-info">
                You've authenticated with <strong><%: ProviderName %></strong>. Please enter your information below for MyCloudClassroom.
            </p>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="userName" CssClass="col-md-2 control-label">EmailId</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="userName" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="userName"
                        Display="Dynamic" CssClass="text-danger" ErrorMessage="User name is required" />
                    <asp:ModelErrorMessage runat="server" ModelStateKey="userName" CssClass="text-error" />
                </div>
                <asp:Label runat="server" AssociatedControlID="nameUser" CssClass="col-md-2 control-label">Name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="nameUser" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="nameUser"
                        Display="Dynamic" CssClass="text-danger" ErrorMessage="Name is required" />
                    <asp:ModelErrorMessage runat="server" ModelStateKey="nameUser" CssClass="text-error" />
                </div>
                <asp:Label runat="server" AssociatedControlID="contactNo" CssClass="col-md-2 control-label">Contact No</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="contactNo" CssClass="form-control" />
                    <asp:ModelErrorMessage runat="server" ModelStateKey="contactno" CssClass="text-error" />
                </div>
                <asp:Label runat="server" AssociatedControlID="userType" CssClass="col-md-2 control-label">User Type</asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList runat="server" ID="userType" CssClass="form-control" >
                        <asp:ListItem Selected="True" Text="Student" Value=0></asp:ListItem>
                        <asp:ListItem Selected="False" Text="Professor" Value=1></asp:ListItem>
                    </asp:DropDownList>
                    <asp:ModelErrorMessage runat="server" ModelStateKey="userType" CssClass="text-error" />
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" Text="Log in" CssClass="btn btn-default" OnClick="LogIn_Click" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
