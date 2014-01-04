<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Topo.ascx.cs" Inherits="CMS.user_controls.Topo" %>
<div class="navbar navbar-fixed-top">
    <div class="navbar-inner">
        <div class="container-fluid">
            <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </a>
            <a class="brand" href="#">Admin Panel</a>
            <div class="nav-collapse collapse">
                <ul class="nav pull-right">
                    <li class="dropdown">
                        <a href="#" role="button" class="dropdown-toggle" data-toggle="dropdown">
                            <i class="icon-user"></i>
                            <asp:LoginName ID="LoginName1" runat="server" />
                            <i class="caret"></i>
                        </a>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:HyperLink ID="linkAlterarSenha" runat="server" TabIndex="-1" NavigateUrl="~/Account/Manage">Alterar senha</asp:HyperLink>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <asp:LoginStatus ID="LoginStatus1" runat="server" TabIndex="-1" LogoutText='<i class="icon-off"></i>&nbsp;&nbsp;Sair' />
                            </li>
                        </ul>
                    </li>
                </ul>
                <asp:Literal ID="lblMenu" runat="server"></asp:Literal>
            </div>
            <!--/.nav-collapse -->
        </div>
    </div>
</div>
