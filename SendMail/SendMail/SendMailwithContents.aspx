<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMailwithContents.aspx.cs" Inherits="SendMail.SendMailwithContents" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .input-group {
            width: 400px;
            margin: 8px;
        }

        .input-group-addon {
            width: 100px;
        }

        #btn_import_list {
            padding-top: 10px;
            float: right;
        }

        .groupImportList {
            float: right;
            margin-right: 100px;
        }

        .marginControl {
            margin: 8px;
        }

        .gridView {
            margin: 8px;
            width: 600px;
        }
    </style>

    <div id="content" style="width: 1200px !important; display: inline-block; margin-top: 30px">
        <div id="content_send_mail" style="width: 900px !important; float: left">
            <div id="group_radio_select_service">
                <label for="comment">Chọn service: </label>
                <label class="radio-inline">
                    <input runat="server" type="radio" name="radio_service" id="radio_service_google">Google Serice</label>
                <label class="radio-inline">
                    <input runat="server" type="radio" name="radio_Service" id="radio_service_stpm">STPM Service</label>
            </div>
            <div style="float: left" runat="server">
                <div class="input-group">
                    <span class="input-group-addon">From Email</span>
                    <input runat="server" type="text" id="ip_txt_from_email" class="form-control" placeholder="Nhập email gửi" aria-describedby="basic-addon1">
                </div>
                <div class="input-group">
                    <span class="input-group-addon">Password</span>
                    <input runat="server" type="password" id="ip_txt_pass_email" class="form-control" placeholder="Mật khẩu email gửi" aria-describedby="basic-addon1">
                </div>
            </div>
            <div class="groupImportList" runat="server">
                <asp:FileUpload ID="FileUpload1" runat="server" class="marginControl" />
                <asp:Button ID="btnUpload" class="btn btn-primary btn-sm marginControl" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                <asp:Label ID="txtNameFileUpload" runat="server">(File excel .xls, .xlsx)</asp:Label>
                <asp:Button ID="btnImportList" class="btn btn-success marginControl" runat="server" Text="Import list email" OnClick="btnImportList_Click" />
            </div>
            <br />
            <asp:Button ID="btnSendListMail" class="btn btn-success" runat="server" Text="Gửi theo danh sách" OnClick="btnSendListMail_Click" />
        </div>
        <br />
    </div>
    <div class="gridView">
        <dx:ASPxGridView ID="gridView" runat="server" csClass="gridview" EnableTheming="True" Theme="BlackGlass">
        </dx:ASPxGridView>
    </div>

</asp:Content>
