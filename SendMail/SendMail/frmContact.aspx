<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmContact.aspx.cs" Inherits="SendMail.frmContact" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .mainContent {
            margin:20px;
            align-self:center;
        }
        .groupImportList {
            margin:10px;
        }
        .marginControl {
            margin:10px;
        }
    </style>
    <div class="mainContent">
        <h3 style="text-align:center">Quản lý danh bạ Email</h3>
        <div id="Div1" class="groupImportList" runat="server">
            <asp:FileUpload ID="FileUpload1" runat="server" class="marginControl" />
            <asp:Button ID="btnUpload" class="btn btn-primary btn-sm marginControl" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <asp:Label ID="txtNameFileUpload" runat="server"></asp:Label>
            <asp:Button ID="btnImportList" class="btn btn-success marginControl" runat="server" Text="Import data" OnClick="btnImportList_Click" />
        </div>

        <div>
            <dx:ASPxGridView ID="gridView" runat="server" AutoGenerateColumns="False" DataSourceID="ContactDataSource" KeyFieldName="ContactID">
                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowDeleteButton="True" ShowNewButtonInHeader="True"></dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="ContactID" ReadOnly="True" VisibleIndex="1">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="2"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FirstName" VisibleIndex="3"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="LastName" VisibleIndex="4"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FullName" VisibleIndex="5"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Phone" VisibleIndex="7"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Adress" VisibleIndex="8"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="Birthday" VisibleIndex="9"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="Gender" VisibleIndex="6">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Selected="True" Text="Nam" Value="1"></dx:ListEditItem>
                                <dx:ListEditItem Text="Nữ" Value="0"></dx:ListEditItem>
                            </Items>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:SqlDataSource runat="server" ID="ContactDataSource" ConflictDetection="CompareAllValues" ConnectionString='<%$ ConnectionStrings:SendMailConnectionString %>' DeleteCommand="DELETE FROM [Contact] WHERE [ContactID] = @original_ContactID AND [Email] = @original_Email AND (([FirstName] = @original_FirstName) OR ([FirstName] IS NULL AND @original_FirstName IS NULL)) AND (([LastName] = @original_LastName) OR ([LastName] IS NULL AND @original_LastName IS NULL)) AND (([FullName] = @original_FullName) OR ([FullName] IS NULL AND @original_FullName IS NULL)) AND (([Gender] = @original_Gender) OR ([Gender] IS NULL AND @original_Gender IS NULL)) AND (([Phone] = @original_Phone) OR ([Phone] IS NULL AND @original_Phone IS NULL)) AND (([Adress] = @original_Adress) OR ([Adress] IS NULL AND @original_Adress IS NULL)) AND (([Birthday] = @original_Birthday) OR ([Birthday] IS NULL AND @original_Birthday IS NULL))" InsertCommand="INSERT INTO [Contact] ([Email], [FirstName], [LastName], [FullName], [Gender], [Phone], [Adress], [Birthday]) VALUES (@Email, @FirstName, @LastName, @FullName, @Gender, @Phone, @Adress, @Birthday)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [ContactID], [Email], [FirstName], [LastName], [FullName], [Gender], [Phone], [Adress], [Birthday] FROM [Contact]" UpdateCommand="UPDATE [Contact] SET [Email] = @Email, [FirstName] = @FirstName, [LastName] = @LastName, [FullName] = @FullName, [Gender] = @Gender, [Phone] = @Phone, [Adress] = @Adress, [Birthday] = @Birthday WHERE [ContactID] = @original_ContactID AND [Email] = @original_Email AND (([FirstName] = @original_FirstName) OR ([FirstName] IS NULL AND @original_FirstName IS NULL)) AND (([LastName] = @original_LastName) OR ([LastName] IS NULL AND @original_LastName IS NULL)) AND (([FullName] = @original_FullName) OR ([FullName] IS NULL AND @original_FullName IS NULL)) AND (([Gender] = @original_Gender) OR ([Gender] IS NULL AND @original_Gender IS NULL)) AND (([Phone] = @original_Phone) OR ([Phone] IS NULL AND @original_Phone IS NULL)) AND (([Adress] = @original_Adress) OR ([Adress] IS NULL AND @original_Adress IS NULL)) AND (([Birthday] = @original_Birthday) OR ([Birthday] IS NULL AND @original_Birthday IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_ContactID" Type="Int64"></asp:Parameter>
                    <asp:Parameter Name="original_Email" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_FirstName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_LastName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_FullName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_Gender" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="original_Phone" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_Adress" Type="String"></asp:Parameter>
                    <asp:Parameter DbType="Date" Name="original_Birthday"></asp:Parameter>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                    <asp:Parameter Name="FirstName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="LastName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="FullName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="Gender" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="Phone" Type="String"></asp:Parameter>
                    <asp:Parameter Name="Adress" Type="String"></asp:Parameter>
                    <asp:Parameter DbType="Date" Name="Birthday"></asp:Parameter>
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                    <asp:Parameter Name="FirstName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="LastName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="FullName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="Gender" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="Phone" Type="String"></asp:Parameter>
                    <asp:Parameter Name="Adress" Type="String"></asp:Parameter>
                    <asp:Parameter DbType="Date" Name="Birthday"></asp:Parameter>
                    <asp:Parameter Name="original_ContactID" Type="Int64"></asp:Parameter>
                    <asp:Parameter Name="original_Email" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_FirstName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_LastName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_FullName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_Gender" Type="Int32"></asp:Parameter>
                    <asp:Parameter Name="original_Phone" Type="String"></asp:Parameter>
                    <asp:Parameter Name="original_Adress" Type="String"></asp:Parameter>
                    <asp:Parameter DbType="Date" Name="original_Birthday"></asp:Parameter>
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
