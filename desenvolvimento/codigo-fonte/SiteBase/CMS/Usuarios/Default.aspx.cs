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
    public partial class Default : BaseWebForm
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
            //cmdCorrigir.Attributes.Add("onclick", "disableSubmitButton(this);" + Page.ClientScript.GetPostBackEventReference((Control)cmdCorrigir, ""));

            // Validar Permissão
            //if (HttpContext.Current.User.IsInRole("Cadastro de Usuários") == false)
            //{
            //    Response.Redirect(UrlBLL.obterAcessoNegado());
            //}

            if (this.Page.IsPostBack == false)
            {
                this.setDataSource();
            }                
        }

        /// <summary>
        /// Ao selecionar algum item da grid.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void gvLista_RowCommand(Object src, GridViewCommandEventArgs e)
        {
            int index = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out index);

            string userName = gvLista.DataKeys[index].Value.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect("editar.aspx?user=" + userName);
            }
            else if (e.CommandName == "Excluir")
            {
                try
                {
                    Membership.DeleteUser(userName, false);
                    this.setDataSource();
                }
                catch (Framework.Erros.Excecao ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                }
            }
        }

        #endregion

        #region "Métodos"

        /// <summary>
        /// Data Source.
        /// </summary>
        protected void setDataSource()
        {
            this.gvLista.DataSource = Membership.GetAllUsers();
            this.gvLista.DataBind();
        }

        #endregion
    }
}