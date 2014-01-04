<%@ Page Title="Assuntos para o formulário de contato" Language="C#" MasterPageFile="~/master-pages/MasterComBox.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMS.FaleConosco.Assuntos.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">

        function excluir() {
            return confirm('Tem certeza que deseja exlcuir este assunto?\nTambém serão excluídos os e-mails vinculados ao assunto.');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumb" runat="server">
    <ul class="breadcrumb">
        <li><a href="#">Home</a> <span class="divider">/</span></li>
        <li><a href="#">Fale Conosco</a> <span class="divider">/</span></li>
        <li class="active">Assuntos</li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TituloBox" runat="server">
    Assuntos para o formulário de contato
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Conteudo" runat="server">

        <asp:Hyperlink ID="linkNovo" runat="server" NavigateUrl="editar" CssClass="btn btn-primary"><i class="icon-white icon-plus"></i> Adicionar novo assunto</asp:Hyperlink>
        <br /><br />

        <asp:ValidationSummary ID="val" runat="server" />

        <Wizard:WizardGridView ID="gvLista" runat="server"
            DataSourceID="gvListaDataSource" DataKeyNames="AssuntoID" OnRowCommand="gvLista_RowCommand"  OnRowDataBound="gvLista_RowDataBound"
            WizardSortExpression="Nome" EmptyDataText="Não há assuntos cadastrados.">
            <Columns>

                <Wizard:WizardBoundField SortExpression="Nome" DataField="Nome" HeaderText="Assunto">
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </Wizard:WizardBoundField>

                <asp:TemplateField HeaderText="E-mails" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                    <ItemStyle Width="70" HorizontalAlign="Center" />
                    <ItemTemplate>

                            <asp:Repeater ID="rptEmails" runat="server">
                                <ItemTemplate>
                                    <span class="btn btn-small">
                                        <b><%#Eval("Nome") %></b> <%#Eval("Email") %>
                                    </span>
                                </ItemTemplate>
                            </asp:Repeater>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Opções" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                    <ItemTemplate>

                        <asp:ImageButton ID="cmdAtivar" runat="server" ImageUrl="~/Content/images/icones/ball-red.gif"
                            CommandArgument="<%# Container.DisplayIndex %>" CommandName="Ativar" ToolTip="Inativo. Clique para ativar" Visible="false" />

                        <asp:ImageButton ID="cmdDesativar" runat="server" ImageUrl="~/Content/images/icones/ball-green.gif"
                            CommandArgument="<%# Container.DisplayIndex %>" CommandName="Desativar" ToolTip="Ativo. Clique para desativar" Visible="false" />

                        &nbsp;&nbsp;

                        <asp:LinkButton ID="linkEditar" runat="server" CausesValidation="false" CommandName="Editar"
                            CommandArgument="<%# Container.DisplayIndex %>" ToolTip="Editar">
                            <i class="icon-edit"></i>
                        </asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="linkExcluir" runat="server" CausesValidation="False" CommandName="Excluir" OnClientClick="return excluir();"
                            CommandArgument="<%# Container.DisplayIndex %>" ToolTip="Excluir">      
                            <i class="icon-remove"></i>                      
                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </Wizard:WizardGridView>
    
        <asp:LinqDataSource ID="gvListaDataSource" runat="server" AutoSort="false"
            OnSelecting="gvListaDataSource_Selecting" AutoPage="false">
        </asp:LinqDataSource>

</asp:Content>
