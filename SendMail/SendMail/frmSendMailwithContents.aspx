<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSendMailwithContents.aspx.cs" Inherits="SendMail.frmSendMailwithContents" %>

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
                <asp:Label ID="txtNameFileUpload" runat="server"></asp:Label>
                <asp:Button ID="btnImportList" class="btn btn-success marginControl" runat="server" Text="Import list email" OnClick="btnImportList_Click" />
            </div>
            <br />
            <asp:Button ID="btnSendListMail" class="btn btn-success" runat="server" Text="Gửi theo danh sách" OnClick="btnSendListMail_Click" />
        </div>
        <br />
    </div>
    <div class="gridView">
        <dx:ASPxGridView ID="gridView" runat="server" csClass="gridview" EnableTheming="True" Theme="Default" DataSourceID="EmailDataSource" AutoGenerateColumns="False" KeyFieldName="ID" Width="572px">
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" Visible="False" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="STT" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ContentEmail" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:SqlDataSource ID="EmailDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SendMailConnectionString %>" DeleteCommand="DELETE FROM [TempSendEmails] WHERE [ID] = @ID" InsertCommand="INSERT INTO [TempSendEmails] ([STT], [Email], [Subject], [ContentEmail]) VALUES (@STT, @Email, @Subject, @ContentEmail)" SelectCommand="SELECT * FROM [TempSendEmails]" UpdateCommand="UPDATE [TempSendEmails] SET [STT] = @STT, [Email] = @Email, [Subject] = @Subject, [ContentEmail] = @ContentEmail WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int64" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="STT" Type="Int64" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="ContentEmail" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="STT" Type="Int64" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="ContentEmail" Type="String" />
                <asp:Parameter Name="ID" Type="Int64" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>

</asp:Content>
