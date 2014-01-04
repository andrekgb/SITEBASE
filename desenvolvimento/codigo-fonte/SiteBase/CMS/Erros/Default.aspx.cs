using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework;
using RegraDeNegocio;

namespace CMS.Erros
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

            if (e.CommandName == "Visualizar")
            {
                int id_ = 0;
                Int32.TryParse(gvLista.DataKeys[index].Value.ToString(), out id_);

                Response.Redirect("Visualizar.aspx?id=" + id_.ToString());
            }
            else if (e.CommandName == "Corrigir")
            {
                try
                {
                    int id_ = 0;
                    Int32.TryParse(gvLista.DataKeys[index].Value.ToString(), out id_);

                    new ErroBLL().corrigir(id_);
                    this.setDataSource();
                }
                catch (Framework.Erros.Excecao ex)
                {
                    this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                }
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

            ErroBLL erroBLL = new ErroBLL();
            e.Arguments.StartRowIndex = 0;
            e.Arguments.MaximumRows = pageSize;
            e.Arguments.TotalRowCount = erroBLL.obterTotalRowCount();
            e.Result = erroBLL.obter(this.gvLista.SortExpression, (this.gvLista.SortDirection == SortDirection.Descending), skip, take);
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