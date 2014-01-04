using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RegraDeNegocio;
using Framework;

namespace CMS.Erros
{
    public partial class Visualizar : BaseWebForm
    {
        #region "Eventos"

        /// <summary>
        /// Ao carregar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            // seta o menu que será marcado como selecionado
            ((user_controls.Topo)this.Master.FindControl("Topo")).Menu = RegraDeNegocio.TypeMenuCMS.Aplicacao;

            // Desabilita o botão ao clicar
            cmdCorrigir.Attributes.Add("onclick", "disableSubmitButton(this);" + Page.ClientScript.GetPostBackEventReference((Control)cmdCorrigir, ""));

            if (this.Page.IsPostBack == false)
                this.preencherDados();
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
        /// Ao clicar em corrigir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdCorrigir_Click(object sender, EventArgs e)
        {
            this.corrigir();
        }

        #endregion

        #region "Métodos"

        /// <summary>
        /// Preenche os dados
        /// </summary>
        private void preencherDados()
        {
            int codErro = 0;
            int.TryParse(Request["id"], out codErro);

            Erro erro = new ErroBLL().obter(codErro);

            if (erro == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            ViewState.Add("CodErro", codErro);


            if (erro.DataCadastro != null) this.lblData.Text = ((DateTime)erro.DataCadastro).ToString("dd/MM/yyyy HH:mm");
            this.lblIP.Text = erro.IP;
            if (string.IsNullOrEmpty(erro.Mensagem) == false) this.lblMensagem.Text = erro.Mensagem.Replace("\n", "<br/>");
            this.txtPagina.Text = erro.Pagina;
            if (string.IsNullOrEmpty(erro.StackTrace) == false) this.lblStackTrace.Text = erro.StackTrace.Replace("\n", "<br/>");

        }

        /// <summary>
        /// marca o erro como corrigido
        /// </summary>
        private void corrigir()
        {

            try
            {
                int codErro = 0;
                int.TryParse(ViewState["CodErro"].ToString(), out codErro);

                new ErroBLL().corrigir(codErro);
                Response.Redirect("Default.aspx");
            }
            catch (Framework.Erros.Excecao ex)
            {
                this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
            }

        }

        #endregion
    }
}