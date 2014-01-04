<%@ Page Title="" Language="C#" MasterPageFile="~/master-pages/MasterComBox.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="CMS.Usuarios.Editar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumb" runat="server">
    <ul class="breadcrumb">
      <li><a href="/">Home</a> <span class="divider">/</span></li>
      <li><a href="#">Aplicação</a> <span class="divider">/</span></li>
      <li class="active"><a href="Default.aspx">Cadastro de Usuários</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TituloBox" runat="server">
    Dados do Usuário
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Conteudo" runat="server">

    <div class="row-fluid">

        <div class="span12">
            <asp:ValidationSummary ID="val" runat="server" />
        </div>

        <div class="span6">                   

            <fieldset>
                <legend>Dados do usuário</legend>

            <div class="control-group">
                <label class="control-label">Login</label>
                <div class="controls">
                    <asp:TextBox ID="txtLogin" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqLogin" runat="server" ControlToValidate="txtLogin" ErrorMessage="Informe o login" Display="None"></asp:RequiredFieldValidator>
                </div>            
            </div>

            <div class="control-group">
                <label class="control-label">E-mail</label>
                <div class="controls">
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>      
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Informe o e-mail" Display="None"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="O e-mail informado é inválido" ControlToValidate="txtEmail"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None"></asp:RegularExpressionValidator>
                </div>            
            </div>

            <div class="control-group" id="divSenha1" runat="server">
                <label class="control-label">Senha</label>
                <div class="controls">
                    <asp:TextBox ID="txtSenha" runat="server" EnableTheming="True" MaxLength="20" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="Informe a senha do usuário" EnableClientScript="True" Display="None"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regSenha" runat="server" ValidationExpression="^.*(?=.{6,})(?=.*[@#$%^&+=]).*$" ControlToValidate="txtSenha" ErrorMessage="A senha deve possuir no mínimo 6 caracteres e incluir no mínimo um caracter especial (@#$%^&+=)" Display="None"></asp:RegularExpressionValidator>
                </div>            
            </div>

            <div class="control-group"  id="divSenha2" runat="server">
                <label class="control-label">Confirmação de senha</label>
                <div class="controls">
                    <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                    <asp:CompareValidator ID="invConfirmarSenha" runat="server" ControlToCompare="txtConfirmarSenha" ControlToValidate="txtSenha" 
                        ErrorMessage="A senha não confere com a confirmação da senha" Display="None"></asp:CompareValidator>
                </div>            
            </div>

            <div class="control-group">
                <label class="control-label">Esta ativo?</label>
                <div class="controls">
                    <asp:DropDownList ID="ddlisAtivo" runat="server">
                        <asp:ListItem>SIM</asp:ListItem>
                        <asp:ListItem>NÃO</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            </fieldset>

        </div>

        <div class="span6">
            <fieldset>
                <legend>Permissões de acesso</legend>

                <div class="control-group">
                    <div class="controls">
                        <asp:CheckBoxList ID="ckPermissoes" runat="server"></asp:CheckBoxList>
                    </div>
                </div>

            </fieldset>
        </div>

        <div class="span12">
            <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" CssClass="btn" OnClick="cmdVoltar_Click" CausesValidation="false" />
            <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="cmdSalvar_Click" />        
        </div>

    </div>

</asp:Content>
