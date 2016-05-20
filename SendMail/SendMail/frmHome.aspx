<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHome.aspx.cs" Inherits="SendMail._Default" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .table_content td {
            margin: 5px 10px;
        }
        .td_subject {
            width:150px;
        }
        .td_body {
            width:300px;
        }
    </style>
    <h3 style="text-align:center">Tra cứu lịch sử gửi email</h3>
    <table class="table_content">
        <tr>
            <td style="align-items:center">
                <dx:ASPxDateEdit ID="date_tu_ngay" runat="server" Date="01/01/2016 23:59:59" OnButtonClick="date_tu_ngay_ButtonClick"></dx:ASPxDateEdit>
            </td>
            <td style="align-items:center">
                <dx:ASPxDateEdit ID="date_den_ngay" runat="server" Date="05/20/2016 14:58:50" OnButtonClick="date_den_ngay_ButtonClick"></dx:ASPxDateEdit>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <dx:ASPxGridView ID="gridView" runat="server" AutoGenerateColumns="False" DataSourceID="v_LogSendEmailDataSource" KeyFieldName="SendMailID" OnCustomUnboundColumnData="gridView_CustomUnboundColumnData">
                    <Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>

                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Columns>
                        <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0"></dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="SendMailID" ReadOnly="True" VisibleIndex="2" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="UserID" VisibleIndex="3" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ContactID" VisibleIndex="4" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeSend" VisibleIndex="5"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="StatusSend" VisibleIndex="18"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="IDEmailOwn" VisibleIndex="6" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TypeServiceUsed" VisibleIndex="7"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CampaignID" VisibleIndex="9" Visible="False"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="13" Width="150px" CellStyle-CssClass="td_subject">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Body" VisibleIndex="14" Width="300px" CellStyle-CssClass="td_body">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CampaignName" VisibleIndex="10"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeBegin" VisibleIndex="15" Visible="False"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeEnd" VisibleIndex="16" Visible="False"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeSchedule" VisibleIndex="17"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="isActive" VisibleIndex="19"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="EmailContact" VisibleIndex="12"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EmailSend" VisibleIndex="11"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="AccountName" VisibleIndex="8"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Number" UnboundType="String" Caption="STT" VisibleIndex="1"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="v_LogSendEmailDataSource" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' SelectCommand="SELECT * FROM [V_LogSendEmail] WHERE (([TimeSend] >= @TimeSend2) AND ([TimeSend] <= @TimeSend))">
                    <SelectParameters>
                        <asp:FormParameter FormField="date_tu_ngay" DefaultValue="01/01/2016" Name="TimeSend2" Type="DateTime"></asp:FormParameter>
                        <asp:FormParameter FormField="date_den_ngay" Name="TimeSend" Type="DateTime" DefaultValue="01/01/2099"></asp:FormParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>

    </table>

</asp:Content>
