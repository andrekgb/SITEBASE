<%@ Page Title="Listagem de usuários com acesso ao CMS" Language="C#" MasterPageFile="~/master-pages/MasterComBox.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMS.Usuarios.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function excluir() {
            return confirm('Tem certeza que deseja excluir este usuário?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumb" runat="server">
    <ul class="breadcrumb">
      <li><a href="/">Home</a> <span class="divider">/</span></li>
      <li><a href="#">Aplicação</a> <span class="divider">/</span></li>
      <li class="active">Cadastro de Usuários</li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TituloBox" runat="server">
    Lista de usuários cadastrados
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Conteudo" runat="server">

    <div class="row-fluid">

        <div class="span12"">
        
        <a href="Editar.aspx" class="btn btn-primary">
            <i class="icon-plus-sign icon-white"></i> Adicionar usuário
        </a>

        <br /><br />

        <asp:ValidationSummary ID="val" runat="server" />

        <Wizard:WizardGridView ID="gvLista" runat="server"
            DataKeyNames="UserName" OnRowCommand="gvLista_RowCommand"
            EmptyDataText="Não há usuários cadastrados.">
            <Columns>
                <Wizard:WizardBoundField DataField="UserName" HeaderText="Login">
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </Wizard:WizardBoundField>

                <Wizard:WizardBoundField DataField="Email" HeaderText="E-mail">
                    <ItemStyle HorizontalAlign="Left" Width="50%" />
                    <HeaderStyle HorizontalAlign="Left" />
                </Wizard:WizardBoundField>

                <Wizard:WizardBoundField DataField="CreationDate" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Data de Criação">
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </Wizard:WizardBoundField>

                <asp:TemplateField HeaderText="Opções" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEditar" runat="server" CausesValidation="false" CommandName="Editar"
                            CommandArgument="<%# Container.DisplayIndex %>" ToolTip="Editar">
                            <i class="icon-edit"></i>
                        </asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="linkExcluir" runat="server" CausesValidation="False" CommandName="Excluir" OnClientClick="return excluir();"
                            CommandArgument="<%# Container.DisplayIndex %>" ToolTip="Excluir">      
                            <i class="icon-remove-sign"></i>                      
                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>
        </Wizard:WizardGridView>       

        </div>
    </div>

</asp:Content>
