<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user_notification.aspx.cs" Inherits="pral_work_1.user_notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <article class="module width_full">
        <header>
            <h3>User Notifcation Settings</h3>
        </header>
        <div class="module_content">
            <h3>Check the fields for which you want to receive notifications for:</h3>

            <p>Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras mattis consectetur purus sit amet fermentum. Maecenas faucibus mollis interdum. Maecenas faucibus mollis interdum. Cras justo odio, dapibus ac facilisis in, egestas eget quam.</p>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
           <%-- <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="cblist_datasource" DataTextField="Notification_Description" DataValueField="Notification_Type_ID"></asp:CheckBoxList>
          --%>  <asp:SqlDataSource runat="server" ID="cblist_datasource" ConnectionString='<%$ ConnectionStrings:notifications_db_cs %>' SelectCommand="SELECT * FROM [Notification_Types]"></asp:SqlDataSource>
            <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click" />
            &nbsp;
            &nbsp;
            <asp:Label ID="lblResults" runat="server"></asp:Label>
        </div>

    </article>

</asp:Content>
