<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="SendMail.SendMail" %>

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

        #content_send_mail {
            margin-top: 30px;
        }

        .groupImportList {
            float: right;
            margin-right: 100px;
        }

        .marginControl {
            margin: 8px;
        }

        .listBoxEmail {
            width: 200px !important;
            height: 500px !important;
            /*border-style:dotted;
            border:2px;
            background:border-box;*/
        }

        #ConentlistBoxEmail {
            width: 300px;
            height: 500px;
            margin-top: 30px;
            float: right;
        }

        #ct_listCheckBoxEmail {
            width: 220px;
            height: 500px;
            border-style: double;
            border-width: 1px;
            OVERFLOW-Y: scroll;
        }

        .checkboxSelectAll {
            margin-left: 10px;
        }
    </style>

    <div id="content" style="width: 1200px !important; display: inline-block; float: right;">
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
                <div style="float: left" class="input-group">
                    <span class="input-group-addon">To Email</span>
                    <input runat="server" type="text" id="ip_txt_to_email" class="form-control" placeholder="Nhập email nhận" aria-describedby="basic-addon1">
                </div>
            </div>
            <div class="groupImportList" runat="server">
                <asp:FileUpload ID="FileUpload1" runat="server" class="marginControl" />
                <asp:Button ID="btnUpload" class="btn btn-primary btn-sm marginControl" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                <asp:Label ID="txtNameFileUpload" runat="server">(File excel .xls, .xlsx)</asp:Label>
                <br />
                <asp:Button ID="btnImportList" class="btn btn-success marginControl" runat="server" Text="Import list email" OnClick="btnImportList_Click" />
            </div>
            <div class="input-group" style="width: 700px">
                <span class="input-group-addon" style="width: 100px">Subject</span>
                <input runat="server" type="text" id="ip_txt_subject" class="form-control" style="max-width: 590px; width: 590px" placeholder="Nhập tiêu đề mail" aria-describedby="basic-addon1">
            </div>
            <div class="form-group">
                <textarea class="form-control" rows="7" id="txt_content_mail" placeholder="Nhập nội dung email..." runat="server" style="width: 700px"></textarea>
            </div>
            <asp:Button ID="btnSendMail" class="btn btn-success" runat="server" Text="Gửi Email đơn" OnClick="btnSendMail_Click" />
            <asp:Button ID="btnSendListMail" class="btn btn-success" runat="server" Text="Gửi theo danh sách" OnClick="btnSendListMail_Click" />
            <br />
            <dx:ASPxProgressBar ID="progressBar" runat="server" Visible="false" Position="0" Width="200px"></dx:ASPxProgressBar>
        </div>
        <div id="ConentlistBoxEmail">
            <div>
                <label style="font-size: 12pt; font-weight: bold; margin-left: 10px;">Danh sách Email Import</label>
                <br />
                <dx:ASPxCheckBox ID="checkBoxSelectAll" runat="server" Text="Select all" CssClass="checkboxSelectAll" EnableViewState="true" OnCheckedChanged="CheckedChanged">
                </dx:ASPxCheckBox>
                <br />
                <%--<asp:ListBox ID="listBoxEmail" runat="server" CssClass="listBoxEmail" class="list-group"></asp:ListBox>--%>
                <div id="ct_listCheckBoxEmail">
                    <dx:ASPxCheckBoxList Border-BorderStyle="None" ID="checkBoxListEmail" runat="server" RepeatColumns="1" RepeatLayout="Table"></dx:ASPxCheckBoxList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
