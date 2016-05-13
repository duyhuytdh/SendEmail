<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="SendMail.frmLogin" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
        #containMain {
            width: 1200px;
            height: 600px;
            margin: auto;
            text-align:center
        }
        .content {
            width: 430px;
            height: 260px;
            margin: auto;
            margin-top:150px;
            border:solid;
            border-radius: 5px 10px;
            background-color:black;
        }
        .cssGroup {
            width:350px !important;
            margin:10px;
            text-align:center;
            margin-left:40px;
        }
        .input-group-addon {
            width:100px !important;
        }
        #btnLogin {
            margin-top:10px;
            width:100px;
        }
        #lbl_failure {
            text-align:center;
            color:red;
        }
        #rememberMe {
            margin-left:20px;
            color:red;
        }
    </style>
    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" onauthenticate="ValidateUser">
        <div id="containMain">
            <div class="content">
                <h3 style="text-align:center; margin-bottom:10px; color:white; font-weight:300; font-family:Corbel;">Đăng nhập hệ thống</h3>
                <div class="input-group cssGroup">
                    <span class="input-group-addon">Account</span>
                    <input runat="server" type="text" id="txt_account" class="form-control" placeholder="Nhập tên tài khoản" aria-describedby="basic-addon1" />
                </div>
                <div class="input-group cssGroup">
                    <span class="input-group-addon">Password</span>
                    <input runat="server" type="password" id="txt_password" class="form-control" placeholder="Nhập mật khẩu" aria-describedby="basic-addon1" />
                </div>
                <asp:Label runat="server"  ID="lbl_failure"></asp:Label>
                <br />
                 <asp:CheckBox ID="rememberMe" Text="Nhớ mật khẩu" runat="server"/>
                <br />
                <asp:Button ID="btnLogin" class="btn btn-success" runat="server" Text="Login" OnClick="btnLogin_Click" />
            </div>
        </div>
    </form>
</body>
</html>
