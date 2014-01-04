using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using RegraDeNegocio;
using Framework;

namespace CMS.Usuarios
{
    public partial class Editar : BaseWebForm
    {
        #region "Eventos"

        /// <summary>
        /// Ao carregar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.configurarPágina();
        }

        /// <summary>
        /// Ao clicar em voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        /// <summary>
        /// Ao clicar em salvar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            this.salvar();
        }

        #endregion

        #region "Métodos"

        /// <summary>
        /// Configura á página
        /// </summary>
        private void configurarPágina()
        {

            // seta o menu que será marcado como selecionado
            ((user_controls.Topo)this.Master.FindControl("Topo")).Menu = RegraDeNegocio.TypeMenuCMS.Aplicacao;

            // Desabilita o botão ao clicar
            cmdSalvar.Attributes.Add("onclick", "disableSubmitButton(this);" + Page.ClientScript.GetPostBackEventReference((Control)cmdSalvar, ""));

            if (this.Page.IsPostBack == false)
            {
                this.preencherPermissoes();
                this.preencherDados();
            }
        }

        /// <summary>
        /// Preencher as permissões
        /// </summary>
        private void preencherPermissoes()
        {
            this.ckPermissoes.Items.Clear();
            string[] regras = System.Web.Security.Roles.GetAllRoles();

            if (regras != null && regras.Length > 0)
            {
                for (int i = 0; i < regras.Length; i++)
                {
                    this.ckPermissoes.Items.Add(new ListItem(regras[i], regras[i]));
                }
            }
        }

        /// <summary>
        /// Preenche os dados
        /// </summary>
        private void preencherDados()
        {
            if (string.IsNullOrEmpty(Request["user"]) == false)
            {
                MembershipUser usuario = Membership.GetUser(Request["user"]);

                if (usuario != null)
                {
                    ViewState.Add("usuario", usuario.UserName);

                    divSenha1.Visible = false;
                    divSenha2.Visible = false;

                    txtLogin.Text = usuario.UserName;
                    txtLogin.Enabled = false;

                    txtEmail.Text = usuario.Email;
                    ddlisAtivo.SelectedIndex = (usuario.IsApproved ? 0 : 1);

                    /// Passa pelas permissões e ve quais estão marcadas
                    foreach (ListItem item in ckPermissoes.Items)
                    {
                        item.Selected = Roles.IsUserInRole(usuario.UserName, item.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Valida o formulário
        /// </summary>
        private void validar()
        {
            // Valida se selecionou alguma permissão
            bool selecionouPermissao = false;
            foreach (ListItem item in this.ckPermissoes.Items)
            {
                if (item.Selected)
                {
                    selecionouPermissao = true;
                    break;
                }
            }

            if (selecionouPermissao == false)
                this.Page.Validators.Add(new Framework.Validacao.CustomError("Você deve selecionar pelo menos uma permissão para o usuário"));
        }

        /// <summary>
        /// marca o erro como corrigido
        /// </summary>
        private void salvar()
        {
            this.validar();

            if (this.Page.IsValid)
            {
                try
                {
                    MembershipUser usuario = null;
                    MembershipCreateStatus status; // status da criação de usuários
                    bool isAlteracao = false;

                    if (ViewState["usuario"] != null)
                        usuario = Membership.GetUser(ViewState["usuario"].ToString());

                    if (usuario == null)
                    {
                        // Cria o usuário
                        string login = txtLogin.Text.Trim();
                        Membership.CreateUser(login, txtSenha.Text.Trim(), txtEmail.Text.Trim(), ".", ".", ddlisAtivo.SelectedIndex == 0, out status);

                        /// Se criou com sucesso atribui ele na variável
                        if (status == MembershipCreateStatus.Success)
                            usuario = Membership.GetUser(login);
                    }
                    else
                    {
                        isAlteracao = true;
                        usuario.Email = txtEmail.Text.Trim();
                        usuario.IsApproved = ddlisAtivo.SelectedIndex == 0;
                        Membership.UpdateUser(usuario);
                    }

                    // Passa pelas roles e vai incluindo permissão
                    foreach (ListItem item in ckPermissoes.Items)
                    {
                        // Se for alteração, remove a role do usuário antes de inserir
                        if (isAlteracao && Roles.IsUserInRole(usuario.UserName, item.Value))
                        {
                            Roles.RemoveUserFromRole(usuario.UserName, item.Value);
                        }

                        if (item.Selected)
                        {
                            Roles.AddUserToRole(usuario.UserName, item.Value);
                        }
                    }
                    Response.Redirect("Default.aspx");
                }
                catch (Framework.Erros.Excecao ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                }
                catch (System.Configuration.Provider.ProviderException ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError("Já existe outro usuáro com o e-mail informado"));
                }
            }
        }

        #endregion
    }
}