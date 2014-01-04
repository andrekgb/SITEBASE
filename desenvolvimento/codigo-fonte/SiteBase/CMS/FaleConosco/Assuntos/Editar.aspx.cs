using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework;
using RegraDeNegocio;
using RegraDeNegocio.FaleConosco;

namespace CMS.FaleConosco.Assuntos
{
    public partial class Editar : BaseWebForm
    {
        #region Propriedades

        /// <summary>
        /// Emails vinculados ao assunto
        /// </summary>
        List<EmailDTO> Emails
        {
            get
            {
                if (ViewState["Emails"] != null)
                    return (List<EmailDTO>)ViewState["Emails"];

                return null;
            }
            set
            {
                ViewState["Emails"] = value;
            }
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Carregar a página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // seta o menu que será marcado como selecionado
            ((user_controls.Topo)this.Master.FindControl("Topo")).Menu = RegraDeNegocio.TypeMenuCMS.Fale_Conosco;

            this.configurarPagina();
        }

        /// <summary>
        /// Clicar no botão salvar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            this.salvar();
        }

        /// <summary>
        /// Ao clicar em salvar email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdInserirEmail_Click(object sender, EventArgs e)
        {
            this.adicionarEmail();
        }

        /// <summary>
        ///  Ao clicar no botão escluir da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            string email = ((Button)sender).CommandArgument.ToString();
            this.exlcuirEmailsLista(email);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Configurar página
        /// </summary>
        private void configurarPagina()
        {
            if (this.Page.IsPostBack == false)
            {
                int assuntoID = 0;
                int.TryParse(Request["id"], out assuntoID);

                if (assuntoID > 0)
                {
                    AssuntoBLL bll = new AssuntoBLL();
                    Fale_Conosco_Assunto reg = bll.obter(assuntoID);

                    if (reg != null)
                    {
                        ViewState.Add("assuntoID", assuntoID);
                        txtNome.Text = reg.Nome;
                        ddlIsativo.SelectedIndex = (reg.IsAtivo ? 1 : 0);

                        this.Emails = bll.obterEmails(assuntoID);
                        this.carregarListaEmails();
                    }
                }
            }
        }

        /// <summary>
        /// Retorna os dados do assunto preenchido no formulario
        /// </summary>
        /// <returns></returns>
        private Fale_Conosco_Assunto obterRegistro()
        {
            Fale_Conosco_Assunto reg = new Fale_Conosco_Assunto();

            if (ViewState["assuntoID"] != null)
            {
                reg.AssuntoID = Convert.ToInt32(ViewState["assuntoID"]);
            }

            reg.Nome = txtNome.Text.Trim();
            reg.IsAtivo = ddlIsativo.SelectedIndex == 1;

            if (this.Emails != null && this.Emails.Count > 0)
            {
                System.Data.Linq.EntitySet<Fale_Conosco_Email> emails = new System.Data.Linq.EntitySet<Fale_Conosco_Email>();

                foreach (EmailDTO item in this.Emails)
                {
                    emails.Add(new Fale_Conosco_Email { Nome = item.Nome, Email = item.Email});
                }

                reg.Fale_Conosco_Emails = emails;
            }
            return reg;
        }

        /// <summary>
        /// Salvar no banco
        /// </summary>
        private void salvar()
        {
            if (this.Page.IsValid)
            {
                try
                {
                    Fale_Conosco_Assunto reg = this.obterRegistro();

                    if (reg.AssuntoID == 0)
                    {
                        new AssuntoBLL().salvar(reg);
                    }
                    else
                    {
                        new AssuntoBLL().alterar(reg);
                    }
                    Response.Redirect("Default.aspx");
                }
                catch (Framework.Erros.Excecao ex)
                {
                    Framework.Validacao.CustomError err = new Framework.Validacao.CustomError(ex.Message);
                    err.ValidationGroup = "valAssunto";
                    this.Page.Validators.Add(err);
                }              
            }
        }

        /// <summary>
        /// Valida se o e-mail ja existe na lista
        /// </summary>
        /// <param name="email"></param>
        private void validarEmail(string email)
        {
            if (string.IsNullOrEmpty(email) == false && this.Emails != null)
            {
                EmailDTO existente = (from p in this.Emails where p.Email == email select p).FirstOrDefault();

                if (existente != null)
                {
                    Framework.Validacao.CustomError err = new Framework.Validacao.CustomError("O e-mail informado já está vinculado ao assunto");
                    err.ValidationGroup = "grpEmail";
                    this.Page.Validators.Add(err);
                }
            }
        }

        /// <summary>
        /// Adicionar um email na lista
        /// </summary>
        private void adicionarEmail()
        {
            this.validarEmail(txtEmail.Text.Trim());
            if (this.Page.IsValid)
            {
                if (this.Emails == null)
                    this.Emails = new List<EmailDTO>();

                this.Emails.Add(new EmailDTO { Nome = txtNomeEmail.Text.Trim(), Email = txtEmail.Text.Trim()});

                txtNomeEmail.Text = "";
                txtEmail.Text = "";

                this.carregarListaEmails();
            }
        }

        /// <summary>
        /// Carrega a lista de emails no repeater
        /// </summary>
        private void carregarListaEmails()
        {
            if (this.Emails == null || this.Emails.Count == 0)
            {
                this.phEOF.Visible = true;
                this.phLista.Visible = false;
            }
            else
            {
                this.phEOF.Visible = false;
                this.phLista.Visible = true;

                this.rptEmails.DataSource = this.Emails;
                this.rptEmails.DataBind();
            }
        }

        /// <summary>
        /// Excluir e-mail da lista para envio
        /// </summary>
        private void exlcuirEmailsLista(string email)
        {
            if (string.IsNullOrEmpty(email) == false)
            {
                EmailDTO convite = (from p in this.Emails where p.Email == email select p).FirstOrDefault();
                if (convite != null)
                {
                    this.Emails.Remove(convite);
                    this.carregarListaEmails();
                }
            }
        }

        #endregion
    }
}