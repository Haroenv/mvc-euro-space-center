<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EuroSpaceCenter.Views.Account.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register to Euro Space Center</title>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/site.css" rel="stylesheet" />
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/">Application name</a>
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
        </div>
        <div class="form-group">
            <label for="email" class="control-label">Email address</label>
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-envelope" aria-hidden="true"></i></span>
                <input type="email" name="email" id="email" class="form-control" runat="server"/>
            </div>
        </div>
        <div class="form-group">
            <label for="password" class="control-label">Password</label>
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-lock" aria-hidden="true"></i></span>
                <asp:TextBox ID="password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RegularExpressionValidator ID="passwordRegex" runat="server" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$" ErrorMessage="Password must be minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet and 1 Number" ControlToValidate="password"/>
            </div>
        </div>
        <div class="form-group">
            <label for="password1" class="control-label">Repeat password</label>
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-lock" aria-hidden="true"></i></span>
                <asp:TextBox ID="password1" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <asp:Button ID="submit" runat="server" Text="Create" CssClass="btn btn-success" OnClick="submit_Click"/>
            <asp:Label ID="output" runat="server" />
        </div>
    </form>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>
