<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHome.aspx.cs" Inherits="SendMail._Default" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .table_content td {
            padding: 5px 5px;
            text-align:center;
        }

        .td_subject {
            width: 150px;
        }

        .td_body {
            width: 300px;
        }
    </style>
    <script type="text/javascript">
        function grid_refresh(s, e) {
            grid.Refresh();
            grid.PerformCallback("databind");
        }
    </script>
    <h3 style="text-align: center">Tra cứu lịch sử gửi email</h3>
    <table class="table_content">
        <tr>
            <td style="text-align:center">
                <dx:ASPxDateEdit ID="date_tu_ngay" runat="server" Date="2016-01-01" Caption="Từ ngày:">
                    <ClientSideEvents ValueChanged="grid_refresh" />
                </dx:ASPxDateEdit>
            </td>
            <td style="text-align:center">
                <dx:ASPxDateEdit ID="date_den_ngay" runat="server" Date="2019-01-01" Caption="Đến ngày:">
                    <ClientSideEvents ValueChanged="grid_refresh" />
                </dx:ASPxDateEdit>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <dx:ASPxGridView ID="gridView" runat="server" AutoGenerateColumns="False" DataSourceID="v_LogSendEmailDataSource" KeyFieldName="SendMailID" OnCustomUnboundColumnData="gridView_CustomUnboundColumnData" ClientInstanceName="grid" OnCustomCallback="gridView_CustomCallback">
                    <Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" UnboundType="String">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SendMailID" ReadOnly="True" VisibleIndex="0" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="UserID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ContactID" VisibleIndex="2" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeSend" VisibleIndex="3"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="StatusSend" VisibleIndex="4"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="IDEmailOwn" VisibleIndex="5" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TypeServiceUsed" VisibleIndex="14"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CampaignID" VisibleIndex="7" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="10" CellStyle-CssClass="td_subject">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Body" VisibleIndex="11" CellStyle-CssClass="td_body">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CampaignName" VisibleIndex="12"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeBegin" VisibleIndex="13" Visible="False"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeEnd" VisibleIndex="15" Visible="False"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeSchedule" VisibleIndex="16"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="isActive" VisibleIndex="17"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="EmailContact" VisibleIndex="9"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EmailSend" VisibleIndex="8"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="AccountName" VisibleIndex="6"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="v_LogSendEmailDataSource" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' SelectCommand="SELECT * FROM [V_LogSendEmail] WHERE (([TimeSend] <= @TimeSend2) AND ([TimeSend] >= @TimeSend))">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="date_den_ngay" PropertyName="Value" DefaultValue="01/01/2099" Name="TimeSend2" Type="DateTime"></asp:ControlParameter>
                        <asp:ControlParameter ControlID="date_tu_ngay" PropertyName="Value" DefaultValue="01/01/2016" Name="TimeSend" Type="DateTime"></asp:ControlParameter>

                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>

    </table>

</asp:Content>
