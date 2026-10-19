<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginPage" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
    <style>
        body { font-family: Segoe UI, Arial; background: #1f3a5f; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .box { background: #fff; padding: 28px 34px; border-radius: 8px; width: 320px; }
        input[type=text], input[type=password] { width: 100%; padding: 7px; margin: 4px 0 10px; box-sizing: border-box; }
        .btn { background: #1f3a5f; color: #fff; border: 0; padding: 8px 18px; border-radius: 4px; cursor: pointer; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="box">
        <h2>Academic Portal Login</h2>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
        <div>Username</div>
        <asp:TextBox ID="txtUser" runat="server" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUser" ErrorMessage="Username required" ForeColor="Red" Display="Dynamic" />
        <div>Password</div>
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPass" ErrorMessage="Password required" ForeColor="Red" Display="Dynamic" />
        <asp:CheckBox ID="chkRemember" runat="server" Text="Remember me" /><br /><br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
        <p style="font-size:12px;color:#666">Demo: admin/admin123, student1/pass123, faculty1/pass123</p>
    </div>
</form>
</body>
</html>
