<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EuroSpaceCenter.Register.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register to Euro Space Center</title>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/site.css" rel="stylesheet" />
</head>
<body>
    <div class="navbar navbar-default navbar-static-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/">Euro Space Center 🚀</a>
            </div>
            <nav class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li><a href="/">Home</a></li>
                    <li><a href="/Home/About">About</a></li>
                    <li><a href="/Home/Contact">Contact</a></li>
                </ul>
            </nav>
        </div>
    </div>
    <form id="form1" runat="server" class="container">
        <h2>Register</h2>
        <p>Make an account for Euro Space Center</p>
        <div class="form-group">
            <label for="name" class="control-label">Your Name</label>
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-user" aria-hidden="true"></i></span>
                <asp:TextBox ID="name" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="RequiredNameValidator" ControlToValidate="name" runat="server" ErrorMessage="You need to have a name" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label for="email" class="control-label">Email address</label>
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-envelope" aria-hidden="true"></i></span>
                <input type="email" name="email" id="email" class="form-control" runat="server" />

            </div>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredEmailValidator" ControlToValidate="email" runat="server" ErrorMessage="You need to have an email address"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionEmailValidator" runat="server" ErrorMessage="This is not a valid email address" ControlToValidate="email" ValidationExpression=".+@.+(\.|:).+" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label for="password" class="control-label">Password</label>
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-lock" aria-hidden="true"></i></span>
                <asp:TextBox ID="password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredPasswordValidator" ControlToValidate="password" runat="server" ErrorMessage="You need to have a password"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="passwordRegex" runat="server" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$" ErrorMessage="Password must be minimum 8 characters, at least 1 uppercase letter, 1 Lowercase letter and 1 number" ControlToValidate="password" Display="Dynamic" CssClass="form-control-feedback" />
        </div>
        <div class="form-group">
            <label for="password1" class="control-label">Repeat password</label>
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-lock" aria-hidden="true"></i></span>
                <asp:TextBox ID="password1" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredPassword1Validator" ControlToValidate="password1" runat="server" ErrorMessage="You need to repeat the password"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="PasswordEqualsValidator" ControlToCompare="password" ControlToValidate="password1" runat="server" ErrorMessage="You need to type the password the same twice"></asp:CompareValidator>
        </div>
        <div class="form-group">
            <asp:Button ID="submit" runat="server" Text="Create" CssClass="btn btn-success" OnClick="submit_Click" />
            <asp:Label ID="output" runat="server" />
        </div>
    </form>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>
