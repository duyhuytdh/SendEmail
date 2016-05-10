<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCampaign.aspx.cs" Inherits="SendMail.frmCampaign" %>

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

        .marginControl {
            margin: 8px;
        }

        .gridView {
            margin: 8px;
            width: 600px;
        }
    </style>

    <div id="content" style="width: 1200px !important;  margin-top: 30px">
        <div id="content_send_mail" style="width: 900px !important;">
            <div runat="server">
                <div class="input-group">
                    <span class="input-group-addon">Tên chiến dịch</span>
                    <input runat="server" type="text" id="txt_campaign_name" class="form-control" placeholder="Nhập tên chiến dịch" aria-describedby="basic-addon1">
                </div>
                <div class="form-group">
                <textarea class="form-control" rows="4" id="txt_desciption" placeholder="Mô tả chiến dịch" runat="server" style="width: 700px"></textarea>
            </div>
            </div>
            <br />
            <asp:Button ID="btnCreateCampaign" class="btn btn-success" runat="server" Text="Tạo chiến dịch" OnClick="btnCreate_Click" />
        </div>
        <br />
    </div>
   

    <div class="gridView">
         <dx:ASPxGridView ID="gridCampaign" runat="server" AutoGenerateColumns="False" Width="720px" DataSourceID="CampaignDataSource" KeyFieldName="CampaignID">
             <SettingsSearchPanel Visible="True" />
             <Columns>
                 <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                 </dx:GridViewCommandColumn>
                 <dx:GridViewDataTextColumn FieldName="CampaignName" VisibleIndex="1" Width="300px">
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataCheckColumn FieldName="isActive" VisibleIndex="2" Width="50px">
                 </dx:GridViewDataCheckColumn>
                 <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3" Width="370">
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="CampaignID" ReadOnly="True" Visible="False" VisibleIndex="4">
                 </dx:GridViewDataTextColumn>
             </Columns>
         </dx:ASPxGridView>
         <asp:SqlDataSource ID="CampaignDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SendMailConnectionString %>" DeleteCommand="DELETE FROM [Campaign] WHERE [CampaignID] = @CampaignID" InsertCommand="INSERT INTO [Campaign] ([CampaignName], [isActive], [Description]) VALUES (@CampaignName, @isActive, @Description)" SelectCommand="SELECT [CampaignName], [isActive], [Description], [CampaignID] FROM [Campaign]" UpdateCommand="UPDATE [Campaign] SET [CampaignName] = @CampaignName, [isActive] = @isActive, [Description] = @Description WHERE [CampaignID] = @CampaignID">
             <DeleteParameters>
                 <asp:Parameter Name="CampaignID" Type="Int64" />
             </DeleteParameters>
             <InsertParameters>
                 <asp:Parameter Name="CampaignName" Type="String" />
                 <asp:Parameter Name="isActive" Type="Boolean" DefaultValue="True"/>
                 <asp:Parameter Name="Description" Type="String" />
                 <asp:Parameter Name="CampaignID" Type="Int64" />
             </InsertParameters>
             <UpdateParameters>
                 <asp:Parameter Name="CampaignName" Type="String" />
                 <asp:Parameter Name="isActive" Type="Boolean"/>
                 <asp:Parameter Name="Description" Type="String" />
                 <asp:Parameter Name="CampaignID" Type="Int64" />
             </UpdateParameters>
         </asp:SqlDataSource>
    </div>

</asp:Content>
