<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSendMailwithContents.aspx.cs" Inherits="SendMail.frmSendMailwithContents" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #btnImportList {
            float: right;
        }

        .groupImportList {
            float: left;
            margin-left: 10px;
        }

        .gridView {
            margin: 8px;
            width: 900px;
        }

        .cmbCampaign {
            width: 350px;
        }

        .cmbEmailOwn {
            width: 350px;
        }

        #defaultCountdown {
            width: 240px;
            height: 50px;
            float: right;
            margin-right: 125px;
        }

        .table_content td {
            padding: 5px 10px;
        }

        .schedule {
            margin-left:5px;
        }
    </style>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="Styles/calendar-blue.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="Styles/jquery.countdown.css">
    <script type="text/javascript" src="Scripts/jquery.plugin.js"></script>
    <script type="text/javascript" src="Scripts/jquery.countdown.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txt_date_schedule.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y %H:%M",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BC",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
        $(function () {
            var newYear = new Date()
            newYear = new Date(newYear.getFullYear() + 1, 1 - 1, 1);
            $('#defaultCountdown').countdown({ until: newYear });
        });
    </script>

    <div style="margin: auto; width: 100%">
        <div id="content" style="width: 1200px !important; display: inline-block;">
            <table class="table_content" style="width: 1200px">
                <tr>
                    <td>
                        <div id="group_radio_select_service" style="display: inline-block;">
                            <label for="comment">Chọn service: </label>
                            <label class="radio-inline">
                                <input runat="server" type="radio" name="radio_service" id="radio_service_google">Google Serice</label>
                            <label class="radio-inline">
                                <input runat="server" type="radio" name="radio_Service" id="radio_service_stpm">STPM Service</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>

                        <dx:ASPxComboBox runat="server" ID="cmbCampaign" IncrementalFilteringMode="StartsWith"
                            DataSourceID="CampaignDataSource" TextField="CampaignName" ValueField="CampaignID" Caption="Chiến dịch:"
                            EnableSynchronization="False" CssClass="cmbCampaign">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }" />
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="CampaignDataSource" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' SelectCommand="SELECT [CampaignID], [CampaignName] FROM [Campaign] WHERE ([isActive] = @isActive)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="true" Name="isActive" Type="Boolean"></asp:Parameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbEmailOwn" runat="server" DataSourceID="EmailOwnDataSource" TextField="Email" ValueField="ID" Caption="Email hệ thống:"
                            EnableSynchronization="False" CssClass="cmbEmailOwn">
                            <Columns>
                                <dx:ListBoxColumn FieldName="Email"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="Password" Visible="False"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="EmailOwnDataSource" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' SelectCommand="SELECT [ID], [Email], [Password] FROM [EmailOwn]"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="td_FileUpload1" colspan="1">
                        <div>
                            <asp:FileUpload ID="FileUpload1" runat="server" class="groupImportList" />
                            <asp:Button ID="btnUpload" class="btn btn-primary btn-sm groupImportList" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                            <asp:Label ID="txtNameFileUpload" runat="server" CssClass="groupImportList"></asp:Label>
                            <asp:Button ID="btnImportList" class="btn btn-success btn-sm groupImportList" runat="server" Text="Import data" OnClick="btnImportList_Click" />
                        </div>
                    </td>
                    <td>
                        <div id="defaultCountdown"></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="gridView">
                            <dx:ASPxGridView ID="gridView" runat="server" csClass="gridview" EnableTheming="True" Theme="Default" DataSourceID="EmailDataSource" AutoGenerateColumns="False" KeyFieldName="ID" Width="1040px">
                                <Columns>
                                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="50px">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" Visible="False" VisibleIndex="1">
                                        <EditFormSettings Visible="False" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="STT" VisibleIndex="2" Width="30px" CellStyle-HorizontalAlign="Center">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="3" Width="120px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="4" Width="250px">
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSendListMail" class="btn btn-success btn-sm" runat="server" Text="Gửi email ngay" OnClick="btnSendListMail_Click" />
                    </td>
                    <td>
                        <div style="display: inline-block; float: left;">
                            <span style="margin-left: 10px; font-weight: bold" class="schedule">Đặt lịch:</span>
                            <asp:TextBox ID="txt_date_schedule" runat="server" ReadOnly="true" placeholder="Chọn thời điểm gửi" class="schedule"></asp:TextBox>
                            <asp:Button ID="btnSchedule" class="btnSchedule btn btn-success btn-sm schedule" runat="server" Text="Bắt đầu" OnClick="btnSchedule_Click" />
                        </div>

                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
