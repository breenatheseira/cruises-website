<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ddac.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<!--start main -->
<div class="main_bg">
<div class="wrap">
	<div class="main">
        <div class="account">				
            <h3><%: Title %>.</h3>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <asp:Label runat="server" ID="notification" />
            <div class="form-horizontal">
                <h4>Create a new account.</h4>
                <hr />
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Name</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="nameBox" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="nameBox" CssClass="text-danger" Display="Dynamic" ErrorMessage="The name field is required." />                        
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Passport Number</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="passportBox" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="passportBox"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The passport field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Nationality</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="nationalityBox" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="nationalityBox"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The Nationality field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Age</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="ageBox" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ageBox"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The age field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Gender</asp:Label>
                    <div class="col-md-10">
                        <asp:RadioButtonList ID="genderRBL"  runat="server">
                            <asp:ListItem ID="maleRB" runat="server" Text=" Male" Value="M" Selected="True"/>
                            <asp:ListItem ID="femaleRB" runat="server" Text=" Female" Value="F"/>
                        </asp:RadioButtonList>
                        
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Email</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="emailBox" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="emailBox"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The email field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Password</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="passwordBox" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordBox"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The password field is required." />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Confirm password</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="passwordCBox" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordCBox"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                        <asp:CompareValidator runat="server" ControlToCompare="passwordCBox" ControlToValidate="passwordBox"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button runat="server" OnClick="CreatePassenger" Text="Register" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
</asp:Content>

