<%@ Page Title="Assunto" Language="C#" MasterPageFile="~/master-pages/MasterComBox.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="CMS.FaleConosco.Assuntos.Editar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumb" runat="server">

    <ul class="breadcrumb">
        <li><a href="#">Home</a> <span class="divider">/</span></li>
        <li><a href="#">Fale Conosco</a> <span class="divider">/</span></li>
        <li class="active"><a href="Default.aspx">Assuntos</a></li>
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TituloBox" runat="server">
    Dados do assunto
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Conteudo" runat="server">

    <asp:Panel ID="panelForm" runat="server" DefaultButton="cmdSalvar">
    <div class="row-fluid">
    <fieldset class="span6">
        <legend>Assunto</legend>

        <asp:ValidationSummary ID="valAssunto" runat="server" ValidationGroup="valAssunto" />

        <label>Assunto *</label>
        <asp:TextBox ID="txtNome" runat="server" MaxLength="50" placeholder="Digite o nome do assunto" ValidationGroup="valAssunto"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Preencha o nome do assunto" ValidationGroup="valAssunto"></asp:RequiredFieldValidator>

        <label>Status *</label>
        <asp:DropDownList ID="ddlIsativo" runat="server">
            <asp:ListItem>INATIVO. Não disponível para seleção no site </asp:ListItem>
            <asp:ListItem>ATIVO. Disponível para seleção no site </asp:ListItem>
        </asp:DropDownList>

    </fieldset>

    <fieldset class="span6">
        <legend>E-mails</legend>
        <p>Informe abaixo os e-mails que vão receber os formulários enviadso pelo site para o assunto acima for selecionado.<br />É necessário ter pelo menos um e-mail vinculado ao assunto</p>
        
        <asp:UpdatePanel ID="ajaxEmail" runat="server">
        <ContentTemplate>

            <asp:Panel ID="panelEmail" runat="server" DefaultButton="cmdInserirEmail">

                <asp:ValidationSummary ID="valEmail" runat="server" ValidationGroup="grpEmail" />
                <div class="form-inline">
                    <asp:TextBox ID="txtNomeEmail" runat="server" MaxLength="50" placeholder="Digite o nome" ValidationGroup="grpEmail"></asp:TextBox>      
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="70" placeholder="Digite o e-mail" ValidationGroup="grpEmail"></asp:TextBox>
                    <asp:Button ID="cmdInserirEmail" runat="server" Text="Inserir e-mail" ValidationGroup="grpEmail" CssClass="btn btn-primary" OnClick="cmdInserirEmail_Click" />
                </div>
                <asp:RequiredFieldValidator ID="reqtxtNomeEmail" runat="server" ControlToValidate="txtNomeEmail" ErrorMessage="Informe o nome" ValidationGroup="grpEmail"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Informe o e-mail" ValidationGroup="grpEmail"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID ="valEml" runat ="server" ErrorMessage ="O e-mail informado é inválido" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="grpEmail"></asp:RegularExpressionValidator>

            </asp:Panel>

            <asp:PlaceHolder ID="phEOF" runat="server">
                <p>&nbsp;</p>
                <p class="alert">Não há e-mails vinculados ao assunto.</p>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phLista" runat="server" Visible="false">

                <p>&nbsp;</p>
                <p><b>Abaixo estão listados os e-mails para os quais serão enviados os convites.</b></p>
                <table class="table">
                    <thead>
                        <th>Nome</th>
                        <th>e-mail</th>
                        <th>&nbsp;</th>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptEmails" runat="server" >
                        <ItemTemplate>
                        <tr>
                            <td><%#Eval("Nome") %></td>
                            <td><%#Eval("Email") %></td>
                            <td align="center"><asp:Button ID="cmdExcluir" runat="server" CssClass="btn btn-mini btn-danger" CommandName="Excluir" Text="Excluir" CommandArgument='<%#Eval("Email") %>' CausesValidation="false" OnClick="cmdExcluir_Click" /></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

            </asp:PlaceHolder>

        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="loadingEmail" runat="server" AssociatedUpdatePanelID="ajaxEmail">
        <ProgressTemplate>

            <div class="loading">
                <div class="content">
                    <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Content/images/ajax-loader-branco.gif" /> Processando...
                </div>
            </div>            

        </ProgressTemplate>
        </asp:UpdateProgress>

    </fieldset>
    </div>

    <div class="row-fluid">
        <div class="form-actions">
            <a href="Default.aspx" class="btn btn-large">Voltar</a>
            <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" OnClick="cmdSalvar_Click" ValidationGroup="valAssunto" CssClass="btn btn-large btn-primary" />
        </div>
    </div>            

    </asp:Panel>

</asp:Content>
