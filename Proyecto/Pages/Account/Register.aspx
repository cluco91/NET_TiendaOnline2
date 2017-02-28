<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Pages_Account_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>Registrar un Nuevo Usuario</h4>
    <hr />
    <p>
        <asp:Literal runat="server" ID="litStatusMessage" />
    </p>

    Nombre de Usuario:<br />
    <asp:TextBox runat="server" ID="txtUserName" CssClass="inputs" /><br />

    Contraseña:
    <br />
    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="inputs" /><br />

    Confirmar Contraseña:
    <br />
    <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="inputs" /><br />
    
    Nombre:<br />
    <asp:TextBox runat="server" ID="txtFirstName" CssClass="inputs" /><br />
    
    Apellido:<br />
    <asp:TextBox runat="server" ID="txtLastName" CssClass="inputs" /><br />
    
    Direccion:<br />
    <asp:TextBox runat="server" ID="txtAddress" CssClass="inputs" /><br />
    
    Codigo Postal:<br />
    <asp:TextBox runat="server" ID="txtPostalCode" CssClass="inputs" /><br />

    <p>
        <asp:Button ID="btnRegister" runat="server" Text="Registrarse" OnClick="btnRegister_Click" CssClass="button" Width="150px" />
    </p>
</asp:Content>

