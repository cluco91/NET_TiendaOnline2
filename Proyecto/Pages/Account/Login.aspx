<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Account_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>Iniciar Sesion</h4><hr />
    <asp:Literal runat="server" ID="litErrorMsg" Text="Usuario o Contraseña incorrecta." Visible="false" />
    <asp:Label runat="server" AssociatedControlID="txtUserName">Nombre de Usuario</asp:Label>
    <br />
    <asp:TextBox runat="server" ID="txtUserName" CssClass="inputs" />
    <br />
    <asp:Label runat="server" AssociatedControlID="txtPassword">Contraseña</asp:Label>
    <br />
    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="inputs" />
    <br />
    <asp:Button ID="btnSignIn" runat="server" Text="Iniciar Sesion" OnClick="btnSignIn_OnClick" CssClass="button"/>
    </asp:Content>

