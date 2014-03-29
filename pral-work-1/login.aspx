<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="pral_work_1.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <article class="module width_full">
        <header>
            <h3>Login</h3>
        </header>
        <div class="module_content">
           <h3>Login Page</h3>
        <table>
           <tr>
              <td>Email:</td>
              <td><input id="txtUserName" type="text" runat="server"/></td>
              <td><asp:RequiredFieldValidator ID="vUserName" ControlToValidate="txtUserName"
                   runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
           </tr>
           <tr>
              <td>Password:</td>
              <td><input id="txtUserPass" type="password" runat="server"/></td>
              <td><asp:RequiredFieldValidator ID="vUserPass" ControlToValidate="txtUserPass"
                   runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
              </td>
           </tr>
           <tr>
              <td>Persistent Cookie:</td>
              <td><ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" /></td>
              <td></td>
           </tr>
        </table>
            <input type="submit" value="Logon" runat="server" id="cmdLogin" /><p></p>
            <asp:Label ID="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
        </div>

    </article>


    


</asp:Content>
