<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUser.aspx.cs" Inherits="SendMail.frmUser" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
            <h3 style="text-align: center">Quản lý người dùng</h3>
            <div class="gridView">
                <dx:ASPxGridView ID="gridView" runat="server" AutoGenerateColumns="False" DataSourceID="userDataSource" KeyFieldName="UserID" OnRowInserting="gridView_RowInserting" OnRowUpdating="gridView_RowUpdating" OnRowDeleting="gridView_RowDeleting">
                    <Columns>
                        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowEditButton="True"></dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="UserID" ReadOnly="True" VisibleIndex="1">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="AccountName" VisibleIndex="2"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Password" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="TimeCreated" VisibleIndex="4" ReadOnly="True"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="isAdmin" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="userDataSource" ConflictDetection="CompareAllValues" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' DeleteCommand="DELETE FROM [User] WHERE [UserID] = @original_UserID AND [AccountName] = @original_AccountName AND [Password] = @original_Password AND [TimeCreated] = @original_TimeCreated AND [isAdmin] = @original_isAdmin" InsertCommand="INSERT INTO [User] ([AccountName], [Password], [TimeCreated], [isAdmin]) VALUES (@AccountName, @Password, @TimeCreated, @isAdmin)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [UserID], [AccountName], [Password], [TimeCreated], [isAdmin] FROM [User]" UpdateCommand="UPDATE [User] SET [AccountName] = @AccountName, [Password] = @Password, [TimeCreated] = @TimeCreated, [isAdmin] = @isAdmin WHERE [UserID] = @original_UserID AND [AccountName] = @original_AccountName AND [Password] = @original_Password AND [TimeCreated] = @original_TimeCreated AND [isAdmin] = @original_isAdmin">
                    <DeleteParameters>
                        <asp:Parameter Name="original_UserID" Type="Int64"></asp:Parameter>
                        <asp:Parameter Name="original_AccountName" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_TimeCreated" Type="DateTime"></asp:Parameter>
                        <asp:Parameter Name="original_isAdmin" Type="Boolean"></asp:Parameter>
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="AccountName" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="TimeCreated" Type="DateTime"></asp:Parameter>
                        <asp:Parameter Name="isAdmin" Type="Boolean"></asp:Parameter>
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="AccountName" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="TimeCreated" Type="DateTime"></asp:Parameter>
                        <asp:Parameter Name="isAdmin" Type="Boolean"></asp:Parameter>
                        <asp:Parameter Name="original_UserID" Type="Int64"></asp:Parameter>
                        <asp:Parameter Name="original_AccountName" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Password" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_TimeCreated" Type="DateTime"></asp:Parameter>
                        <asp:Parameter Name="original_isAdmin" Type="Boolean"></asp:Parameter>
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
