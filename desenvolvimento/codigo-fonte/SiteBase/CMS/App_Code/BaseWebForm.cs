using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using RegraDeNegocio;

namespace CMS
{
    /// <summary>
    /// Classe base para as páginas.
    /// </summary>
    public class BaseWebForm : System.Web.UI.Page
    {
        #region "Eventos"

        /// <summary>
        /// Ao ocorrer um erro durante a execução de uma página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            //tenta fazer o log do erro no banco de dados
            try
            {

                Erro erro = new Erro();

                if (User.Identity.IsAuthenticated)
                    erro.UserId = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
                erro.DataCadastro = DateTime.Now;
                erro.Resolvido = false;
                erro.Mensagem = ex.Message.ToString();
                erro.StackTrace = ex.StackTrace.ToString();
                erro.Pagina = Request.Url.ToString();
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Length > 0)
                    erro.IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                else
                    erro.IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                new ErroBLL().salvar(erro);
            }
            catch (Exception)
            {
            }

            Server.Transfer(ResolveUrl("~/500.aspx"), false);
        }

        #endregion

    }
}