<%@ Page Title="Log de Erros" Language="C#" MasterPageFile="~/master-pages/MasterComBox.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMS.Erros.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BreadCrumb" runat="server">
    <ul class="breadcrumb">
        <li><a href="/">Home</a> <span class="divider">/</span></li>
        <li><a href="#">Aplicação</a> <span class="divider">/</span></li>
        <li class="active">Log de Erros</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="TituloBox" runat="server">Log de Erros</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Conteudo" runat="server">

    <asp:ValidationSummary ID="val" runat="server" />

    <Wizard:WizardGridView ID="gvLista" runat="server"
        DataSourceID="gvListaDataSource" DataKeyNames="CodErro" OnRowCommand="gvLista_RowCommand"
        WizardSortExpression="DataCadastro" WizardSortDirection="Descending" EmptyDataText="Não há erros sem correção.">
        <Columns>
            <Wizard:WizardTemplateField HeaderText="ID" SortExpression="CodErro">
                <ItemTemplate>
                    <%# Eval("CodErro")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <HeaderStyle HorizontalAlign="Center" />
            </Wizard:WizardTemplateField>
            <Wizard:WizardBoundField SortExpression="IP" DataField="IP" HeaderText="IP">
                <ItemStyle HorizontalAlign="Center" Width="10%" />
                <HeaderStyle HorizontalAlign="Center" />
            </Wizard:WizardBoundField>
            <Wizard:WizardBoundField SortExpression="DataCadastro" DataField="DataCadastro" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Data">
                <ItemStyle HorizontalAlign="Center" Width="15%" />
                <HeaderStyle HorizontalAlign="Center" />
            </Wizard:WizardBoundField>
            <Wizard:WizardBoundField DataField="Mensagem" HeaderText="Mensagem" SortExpression="Mensagem">
                <ItemStyle HorizontalAlign="Left" Width="30%" />
                <HeaderStyle HorizontalAlign="Left" />
            </Wizard:WizardBoundField>
            <Wizard:WizardBoundField DataField="Pagina" HeaderText="Página" SortExpression="Pagina">
                <ItemStyle HorizontalAlign="Left" Width="26%" />
                <HeaderStyle HorizontalAlign="Left" />
            </Wizard:WizardBoundField>
            <asp:TemplateField HeaderText="Opções" ItemStyle-HorizontalAlign="Center" ShowHeader="False">
                <ItemStyle Width="7%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton ID="linkVisualizar" runat="server" CausesValidation="false" CommandName="Visualizar"
                        CommandArgument="<%# Container.DisplayIndex %>" ToolTip="Visualizar">
                <i class="icon-search"></i>
                    </asp:LinkButton>
                    &nbsp;&nbsp;
            <asp:LinkButton ID="linkCorrigir" runat="server" CausesValidation="False" CommandName="Corrigir" OnClientClick="return corrigir();"
                CommandArgument="<%# Container.DisplayIndex %>" ToolTip="Corrigir">      
                <i class="icon-ok"></i>                      
            </asp:LinkButton>

                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
    </Wizard:WizardGridView>

    <asp:LinqDataSource ID="gvListaDataSource" runat="server" AutoSort="false"
        OnSelecting="gvListaDataSource_Selecting" AutoPage="false">
    </asp:LinqDataSource>


</asp:Content>
