<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSendMail.aspx.cs" Inherits="SendMail.frmSendMail" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .table_content td {
            padding: 5px 10px;
        }

        .cmbEmailOwn {
            width: 300px !important;
        }

        .cmbContact {
            width: 300px !important;
        }

        .cmbCampaign {
            width: 300px !important;
        }

        .listBoxEmail {
            width: 250px !important;
            height: 440px !important;
        }

        #ConentlistBoxEmail {
            width: 300px;
            height: 500px;
            float: right;
        }

        #ct_listCheckBoxEmail {
            width: 240px;
            height: 440px;
            border-style: double;
            border-width: 1px;
            OVERFLOW-Y: scroll;
        }

        .checkboxSelectAll {
            margin-left: 10px;
        }
    </style>

    <script type="text/javascript">
        function OnCbAllCheckedChanged(s, e) {
            if (s.GetChecked()) {
                checkBoxListEmail.SelectAll();
            } else
                checkBoxListEmail.UnselectAll();
        }

        function cbList_SelectedIndexChanged(s, e) {
            var selectedItemsCount = s.GetSelectedItems().length;
            cbAll.SetChecked(selectedItemsCount == s.GetItemCount());
        }
    </script>

    <div id="content" style="width: 1200px !important; display: inline-block; float: right;">
        <table class="table_content" style="width: 1200px">
            <tr>
                <td colspan="1">
                    <div id="group_radio_select_service">
                        <label for="comment">Chọn service: </label>
                        <label class="radio-inline">
                            <input runat="server" type="radio" name="radio_service" id="radio_service_google">Google Serice</label>
                        <label class="radio-inline">
                            <input runat="server" type="radio" name="radio_Service" id="radio_service_stpm">STPM Service</label>
                    </div>
                </td>
                <td colspan="3">
                    <dx:ASPxComboBox runat="server" ID="cmbCampaign" IncrementalFilteringMode="StartsWith"
                        DataSourceID="CampaignDataSource" TextField="CampaignName" ValueField="CampaignID" Caption="Chiến dịch:"
                        EnableSynchronization="False" CssClass="cmbCampaign">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }" />
                        <Columns>
                            <dx:ListBoxColumn FieldName="CampaignID" Visible="False"></dx:ListBoxColumn>
                            <dx:ListBoxColumn FieldName="CampaignName"></dx:ListBoxColumn>
                        </Columns>
                    </dx:ASPxComboBox>
                    <asp:SqlDataSource runat="server" ID="CampaignDataSource" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' SelectCommand="SELECT [CampaignID], [CampaignName] FROM [Campaign] WHERE ([isActive] = @isActive)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="true" Name="isActive" Type="Boolean"></asp:Parameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxComboBox ID="cmbEmailOwn" runat="server" DataSourceID="EmailOwnDataSource" TextField="Email" ValueField="ID" Caption="Email hệ thống:"
                        EnableSynchronization="False" CssClass="cmbEmailOwn">
                        <Columns>
                            <dx:ListBoxColumn FieldName="ID" Visible="False"></dx:ListBoxColumn>
                            <dx:ListBoxColumn FieldName="Email"></dx:ListBoxColumn>
                            <dx:ListBoxColumn FieldName="Password" Visible="False"></dx:ListBoxColumn>
                        </Columns>
                    </dx:ASPxComboBox>
                    <asp:SqlDataSource runat="server" ID="EmailOwnDataSource" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' SelectCommand="SELECT [ID], [Email], [Password] FROM [EmailOwn]"></asp:SqlDataSource>
                </td>
                <td colspan="2">
                    <dx:ASPxComboBox ID="cmbContact" runat="server" DataSourceID="ContactDataSource" ValueField="ContactID" Caption="Email nhận:" CssClass="cmbContact">
                        <Columns>
                            <dx:ListBoxColumn FieldName="ContactID" Visible="False"></dx:ListBoxColumn>
                            <dx:ListBoxColumn FieldName="Email"></dx:ListBoxColumn>
                        </Columns>
                    </dx:ASPxComboBox>
                    <asp:SqlDataSource runat="server" ID="ContactDataSource" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' SelectCommand="SELECT [Email], [ContactID] FROM [Contact]"></asp:SqlDataSource>
                </td>
                <td rowspan="5">
                    <div id="ConentlistBoxEmail">
                        <div>
                            <label style="font-size: 12pt; font-weight: bold; margin-left: 10px;">Danh sách Email Import</label>
                            <br />
                            <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="Select All" ClientInstanceName="cbAll" CheckState="Unchecked">
                                <ClientSideEvents CheckedChanged="OnCbAllCheckedChanged" />
                            </dx:ASPxCheckBox>
                            <br />
                            <div id="ct_listCheckBoxEmail">
                                <dx:ASPxCheckBoxList Border-BorderStyle="None" ID="checkBoxListEmail" runat="server" RepeatColumns="1" RepeatLayout="Table">
                                    <ClientSideEvents SelectedIndexChanged="cbList_SelectedIndexChanged" />
                                </dx:ASPxCheckBoxList>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" class="marginControl" />

                </td>
                <td>
                    <asp:Button ID="btnUpload" class="btn btn-primary btn-sm marginControl" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                    <asp:Label ID="txtNameFileUpload" runat="server">(File excel .xls, .xlsx)</asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnImportList" class="btn btn-success btn-sm marginControl" runat="server" Text="Import list email" OnClick="btnImportList_Click" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class="input-group">
                        <span class="input-group-addon" style="width: 100px">Subject</span>
                        <input runat="server" type="text" id="ip_txt_subject" class="form-control" style="min-width: 700px" placeholder="Nhập tiêu đề mail" aria-describedby="basic-addon1">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class="form-group">
                        <textarea class="form-control" rows="12" id="txt_content_mail" style="width: 800px" placeholder="Nhập nội dung email..." runat="server"></textarea>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSendMail" class="btn btn-success" runat="server" Text="Gửi Email đơn" OnClick="btnSendMail_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSendListMail" class="btn btn-success" runat="server" Text="Gửi theo danh sách" OnClick="btnSendListMail_Click" />
                </td>
                <td></td>
                <td></td>
            </tr>

        </table>
    </div>
</asp:Content>
