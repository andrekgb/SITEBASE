using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework;
using RegraDeNegocio;
using RegraDeNegocio.FaleConosco;

namespace CMS.FaleConosco.Assuntos
{
    public partial class Default : BaseWebForm
    {
        #region Eventos

        /// <summary>
        /// Ao carregar a pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // seta o menu que será marcado como selecionado
            ((user_controls.Topo)this.Master.FindControl("Topo")).Menu = RegraDeNegocio.TypeMenuCMS.Fale_Conosco;
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

            int idRegistro = 0;
            Int32.TryParse(gvLista.DataKeys[index].Value.ToString(), out idRegistro);

            if (e.CommandName == "Editar")
            {
                Response.Redirect("editar?id=" + idRegistro.ToString());
            }
            else if (e.CommandName == "Excluir")
            {
                try
                {
                    new AssuntoBLL().excluir(idRegistro);
                    this.setDataSource();
                }
                catch (Framework.Erros.Excecao ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                }
            }
            else if(e.CommandName == "Ativar" || e.CommandName == "Desativar")
            {                
                try
                {
                    bool isAtivo = e.CommandName == "Ativar";
                    new AssuntoBLL().ativar(idRegistro, isAtivo);
                    this.setDataSource();
                }
                catch (Framework.Erros.Excecao ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                }
            }
        }

        /// <summary>
        /// Ao criar as linhas.
        /// </summary>
        protected void gvLista_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Fale_Conosco_Assunto reg = (Fale_Conosco_Assunto)e.Row.DataItem;

                List<EmailDTO> emails = new AssuntoBLL().obterEmails(reg.AssuntoID);

                ((ImageButton)e.Row.FindControl("cmdAtivar")).Visible = reg.IsAtivo == false;
                ((ImageButton)e.Row.FindControl("cmdDesativar")).Visible = reg.IsAtivo;

                // Ordenação
                Repeater rpt = ((Repeater)e.Row.FindControl("rptEmails"));
                rpt.DataSource = emails;
                rpt.DataBind();
                
            }
        }

        /// <summary>
        /// OnSelectiong.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvListaDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            int pageSize = this.gvLista.PageSize;
            int skip = this.gvLista.PageIndex * pageSize;
            int take = pageSize;

            AssuntoBLL bll = new AssuntoBLL();
            e.Arguments.StartRowIndex = 0;
            e.Arguments.MaximumRows = pageSize;
            e.Arguments.TotalRowCount = bll.obterTotalRowCount();
            e.Result = bll.obter(this.gvLista.SortExpression, (this.gvLista.SortDirection == SortDirection.Descending), skip, take);
        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// Data Source.
        /// </summary>
        protected void setDataSource()
        {
            this.gvListaDataSource.SelectParameters.Clear();
        }

        #endregion
    }
}