<%@ Page Title="Alteração de senha de acesso" Language="C#" MasterPageFile="~/master-pages/Master.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="CMS.Account.Manage" %>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumb" runat="server">
    <ul class="breadcrumb">
        <li><a href="#">Conta</a> <span class="divider">/</span></li>
        <li class="active">Alteração de senha de acesso</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="Conteudo" runat="server">
    
    <div class="span4 offset4">

    <div class="block">
        <div class="navbar navbar-inner block-header">
            <div class="muted pull-left">Alteração de senha de acesso</div>
        </div>

        <div class="block-content collapse in">
            <section id="passwordForm">
                <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
                    <p class="alert alert-success">Sua senha de acesso foi alterada com sucesso.</p>
                </asp:PlaceHolder>

                <p>Você esta logado como <strong><%: User.Identity.Name %></strong>.</p>

                <asp:PlaceHolder runat="server" ID="setPassword" Visible="false">
                    <p>
                        You do not have a local password for this site. Add a local
                        password so you can log in without an external login.
                    </p>
                    <fieldset>
                        <legend>Set Password Form</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="password" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="password"
                                    CssClass="field-validation-error" ErrorMessage="The password field is required."
                                    Display="Dynamic" ValidationGroup="SetPassword" />
                        
                                <asp:ModelErrorMessage runat="server" ModelStateKey="NewPassword" AssociatedControlID="password"
                                    CssClass="field-validation-error" SetFocusOnError="true" />
                        
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="confirmPassword">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="confirmPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required."
                                    ValidationGroup="SetPassword" />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="confirmPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match."
                                    ValidationGroup="SetPassword" />
                            </li>
                        </ol>
                        <asp:Button runat="server" Text="Set Password" ValidationGroup="SetPassword" OnClick="setPassword_Click" />
                    </fieldset>
                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="changePassword" Visible="false">
                    <asp:ChangePassword runat="server" 
                        CancelDestinationPageUrl="~/" 
                        ViewStateMode="Disabled" 
                        RenderOuterTable="false" 
                        SuccessPageUrl="Manage?m=ChangePwdSuccess"
                        ChangePasswordFailureText=' <div class="alert alert-error">Senha atual inválida ou nova senha inválida. A senha deve ter pelo menos {0} caracteres e pelo menos {1} caracteres especiais.</div>'>
                        <ChangePasswordTemplate>
                            <fieldset>

                                <asp:ValidationSummary ID="valsenha" runat="server" ValidationGroup="ChangePassword" CssClass="alert alert-danger"  />
                                
                                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                
                                <asp:TextBox runat="server" ID="CurrentPassword" TextMode="Password" Placeholder="Senha atual" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
                                    CssClass="field-validation-error" ErrorMessage="Informe a senha atual."
                                    ValidationGroup="ChangePassword" Display="None" />
                                <br />

                                <asp:TextBox runat="server" ID="NewPassword" TextMode="Password" Placeholder="Nova senha" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
                                    CssClass="field-validation-error" ErrorMessage="Informe o nova senha."
                                    ValidationGroup="ChangePassword" Display="None" />
                                <br />
                                
                                <asp:TextBox runat="server" ID="ConfirmNewPassword" TextMode="Password" Placeholder="Confirme a nova senha" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error" ErrorMessage="Confirme a nova senha."
                                    ValidationGroup="ChangePassword" Display="None" />

                                <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error"  Display="None" ErrorMessage="A nova senha e a confirmação são diferentes."
                                    ValidationGroup="ChangePassword" />
                                <br />
                                <asp:Button runat="server" CommandName="ChangePassword" Text="Alterar senha de acesso" ValidationGroup="ChangePassword" CssClass="btn btn-primary" />
                            </fieldset>
                        </ChangePasswordTemplate>
                    </asp:ChangePassword>
                </asp:PlaceHolder>

            </section>
        </div>
    </div>
    </div>      
</asp:Content>
