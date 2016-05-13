<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEmailOwn.aspx.cs" Inherits="SendMail.frmEmailOwn" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
         #containMain {
            width: 1200px;
            margin: auto;
            text-align:center
        }
        .content {
            width:600px;
            margin: auto;
            align-self: center;
        }

        .marginControl {
            margin: 10px;
        }
        .gridView {
            margin-top:20px;
        }
    </style>
    <div id="containMain">
        <div class="content">
            <h3 style="text-align: center">Quản lý Email hệ thống</h3>
            <div class="gridView">
                <dx:ASPxGridView ID="gridView" runat="server" DataSourceID="EmailOwnDataSource" AutoGenerateColumns="False" KeyFieldName="ID" OnRowInserting="gridView_RowInserting" OnRowUpdating="gridView_RowUpdating" Width="683px">
                    <Columns>
                        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowEditButton="True"></dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="1" Visible="False">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="2" Width="200px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Password" VisibleIndex="3" Width="100px">
                            <PropertiesTextEdit Width="100px">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="4" Width="350px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="isActive" VisibleIndex="5" Width="50px"></dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="EmailOwnDataSource" ConflictDetection="CompareAllValues" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' DeleteCommand="DELETE FROM [EmailOwn] WHERE [ID] = @original_ID AND [Email] = @original_Email AND [Password] = @original_Password AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND [isActive] = @original_isActive" InsertCommand="INSERT INTO [EmailOwn] ([Email], [Password], [Description], [isActive]) VALUES (@Email, @Password, @Description, @isActive)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [EmailOwn]" UpdateCommand="UPDATE [EmailOwn] SET [Email] = @Email, [Password] = @Password, [Description] = @Description, [isActive] = @isActive WHERE [ID] = @original_ID AND [Email] = @original_Email AND [Password] = @original_Password AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND [isActive] = @original_isActive">
                    <DeleteParameters>
                        <asp:Parameter Name="original_ID" Type="Int64"></asp:Parameter>
                        <asp:Parameter Name="original_Email" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Description" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_isActive" Type="Boolean"></asp:Parameter>
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Description" Type="String"></asp:Parameter>
                        <asp:Parameter Name="isActive" Type="Boolean"></asp:Parameter>
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Description" Type="String"></asp:Parameter>
                        <asp:Parameter Name="isActive" Type="Boolean"></asp:Parameter>
                        <asp:Parameter Name="original_ID" Type="Int64"></asp:Parameter>
                        <asp:Parameter Name="original_Email" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Description" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_isActive" Type="Boolean"></asp:Parameter>
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
