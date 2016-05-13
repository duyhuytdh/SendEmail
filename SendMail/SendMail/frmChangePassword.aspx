<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmChangePassword.aspx.cs" Inherits="SendMail.frmChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        #containMain {
            width: 1200px;
            height: 600px;
            margin: auto;
            text-align:center
        }
        .content {
            width: 440px;
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
            width:150px !important;
        }
        #btnChangePass {
            margin-top:20px;
            width:100px;
        }
        #lbl_failure {
            margin-top:8px;
            text-align:center;
            color:red;
        }
    </style>
    <div id="containMain">
        <div class="content">
            <h3 style="text-align:center; margin-bottom:10px; color:white; font-weight:300; font-family:Corbel;">Đổi mật khẩu</h3>
            <div class="input-group cssGroup">
                <span class="input-group-addon">Mật khẩu cũ</span>
                <input runat="server" type="password" id="txt_pass_old" class="form-control" placeholder="Mật khẩu cũ" aria-describedby="basic-addon1">
            </div>
            <div class="input-group cssGroup">
                <span class="input-group-addon">Mật khẩu mới</span>
                <input runat="server" type="password" id="txt_pass_new" class="form-control" placeholder="Nhập mật khẩu mới" aria-describedby="basic-addon1">
            </div>
            <div class="input-group cssGroup">
                <span class="input-group-addon">Nhập lại mật khẩu</span>
                <input runat="server" type="password" id="txt_pass_new_repeat" class="form-control" placeholder="Nhập lại mật khẩu mới" aria-describedby="basic-addon1">
            </div>
            <asp:Button ID="btnChangePass" class="btn btn-success" runat="server" Text="Đổi mật khẩu" OnClick="btnChangePass_Click" />
            <br />
            <asp:Label runat="server" ID="lbl_failure" style="color:red"></asp:Label>
        </div>
    </div>
</asp:Content>
