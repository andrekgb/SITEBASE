<%@ Page Title="Log in" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CMS.Account.Login" %>

<!DOCTYPE html>
<html>
  <head>
    <title>Autenticação</title>
    <!-- Bootstrap -->

    <link href="<%=ResolveUrl("~/Content/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" media="screen">
    <link href="<%=ResolveUrl("~/Content/bootstrap/css/bootstrap-responsive.min.css") %>" rel="stylesheet" media="screen">
    <link href="<%=ResolveUrl("~/Content/assets/styles.css") %>" rel="stylesheet" media="screen">
    <link href="<%=ResolveUrl("~/Content/Custom.css") %>" rel="stylesheet" media="screen">

     <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script src="<%=ResolveUrl("~/Content/vendors/modernizr-2.6.2-respond-1.1.0.min.js") %>"></script>
  </head>
  <body id="login">
    <div class="container">        
        <form runat="server" class="form-signin">
        <asp:Login ID="Login1" runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
            <LayoutTemplate>

                <fieldset>
                    <legend>Autenticação</legend>

                    <asp:ValidationSummary ID="validation" runat="server" CssClass="alert alert-danger" />

                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>

                    <asp:TextBox runat="server" ID="UserName" CssClass="input-block-level" Placeholder="Digite seu login" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="Preencha seu login." Display="None" />

                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="input-block-level" Placeholder="Digite sua senha" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="Preencha sua senha." Display="None" />

                    <label class="checkbox">
                        <asp:CheckBox runat="server" ID="RememberMe" Text="Manter logado" />                        
                    </label>
                    <asp:Button ID="Button1" runat="server" CommandName="Login" Text="Log in" CssClass="btn btn-primary btn-large btn-block" />
                </fieldset>
            </LayoutTemplate>
        </asp:Login>
        </form>
    </div> <!-- /container -->
      <script src="<%=ResolveUrl("~/Content/vendors/jquery-1.9.1.js") %>"></script>
      <script src="<%=ResolveUrl("~/Content/bootstrap/js/bootstrap.min.js") %>"></script>
  </body>
</html>