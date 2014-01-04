<%@ Page Title="Detalhes do Erro" Language="C#" MasterPageFile="~/master-pages/MasterComBox.Master" AutoEventWireup="true" CodeBehind="Visualizar.aspx.cs" Inherits="CMS.Erros.Visualizar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumb" runat="server">
    <ul class="breadcrumb">
      <li><a href="/">Home</a> <span class="divider">/</span></li>
      <li><a href="#">Aplicação</a> <span class="divider">/</span></li>
      <li class="active"><a href="Default.aspx">Log de Erros</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TituloBox" runat="server">
    Detalhes do Erro
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Conteudo" runat="server">
        <asp:ValidationSummary ID="val" runat="server" />

        <table class="table table-bordered">
            <tr>
                <td class="label-campo">Usuário</td>
                <td><asp:Literal ID="lblUsuario" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td class="label-campo">Data</td>
                <td><asp:Literal ID="lblData" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td class="label-campo">IP</td>
                <td><asp:Literal ID="lblIP" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td class="label-campo">Página</td>
                <td><asp:TextBox ID="txtPagina" runat="server" Enabled="false" CssClass="span8"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="label-campo">Mensagem</td>
                <td><asp:Literal ID="lblMensagem" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td class="label-campo">Stack Trace</td>
                <td><asp:Literal ID="lblStackTrace" runat="server"></asp:Literal></td>
            </tr>
        </table>

        <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" CssClass="btn" OnClick="cmdVoltar_Click" CausesValidation="false" />
        <asp:Button ID="cmdCorrigir" runat="server" Text="Marcar como resolvido" CssClass="btn btn-success" OnClick="cmdCorrigir_Click" />
</asp:Content>
