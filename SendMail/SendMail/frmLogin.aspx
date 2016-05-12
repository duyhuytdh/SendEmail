<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="SendMail.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" onauthenticate="ValidateUser">
        <div>
            <div class="input-group">
                <span class="input-group-addon">Account</span>
                <input runat="server" type="text" id="txt_account" class="form-control" placeholder="Nhập tên tài khoản" aria-describedby="basic-addon1">
            </div>
            <div class="input-group">
                <span class="input-group-addon">Password</span>
                <input runat="server" type="password" id="txt_password" class="form-control" placeholder="Nhập mật khẩu" aria-describedby="basic-addon1">
            </div>
            <asp:Button ID="btnLogin" class="btn btn-success" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </div>
        <asp:Label runat="server" ID="lbl_failure"></asp:Label>
    </form>
</body>
</html>
