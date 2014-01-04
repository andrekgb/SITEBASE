using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.Linq;

namespace RegraDeNegocio
{
    /// <summary>
    /// Regra de negócios de erros
    /// </summary>
    public class ErroBLL : BaseBLL
    {
        #region "Métodos"

        /// <summary>
        /// Retorna A listagem de erros não corrigidos
        /// </summary>
        /// <param name="orderByColumnName"></param>
        /// <param name="orderByDescending"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<Erro> obter(string orderByColumnName, bool orderByDescending, int skip, int take)
        {
            var query = from p in this.ctx.Erros select p;
            query = query.Where(ad => ad.Resolvido == false);

            if (!string.IsNullOrEmpty(orderByColumnName))
                query = query.OrderBy(orderByColumnName, orderByDescending);
            else
                query = query.OrderBy("DataCadastro", true);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return new List<Erro>(query);
        }

        /// <summary>
        /// Conta o total de registros
        /// </summary>
        /// <returns></returns>
        public int obterTotalRowCount()
        {
            var query = from p in this.ctx.Erros select p;
            query = query.Where(ad => ad.Resolvido == false);
            return query.Count();
        }

        /// <summary>
        /// Salva um erro
        /// </summary>
        public void salvar(Erro erro)
        {
            try
            {
                // salva
                erro.DataCadastro = DateTime.Now;
                this.ctx.Erros.InsertOnSubmit(erro);
                this.ctx.SubmitChanges();
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw ex;
            }
        }

        /// <summary>
        /// Salva um erro.
        /// </summary>
        /// <param name="ex">Excecao</param>
        /// <param name="context">Contexto</param>
        public void salvar(Exception ex, System.Web.HttpContext context)
        {
            if (context == null)
                context = HttpContext.Current;

            Erro erro = new Erro();

            erro.DataCadastro = DateTime.Now;
            erro.Mensagem = ex.Message.ToString();
            erro.StackTrace = ex.StackTrace.ToString();

            if (context != null)
            {
                erro.Pagina = context.Request.Url.ToString();

                if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Length > 0)
                    erro.IP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                else
                    erro.IP = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            this.salvar(erro);
        }

        /// <summary>
        /// Retorna um erro por ID
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Erro obter(long codigo)
        {
            return (from p in this.ctx.Erros where p.CodErro == codigo select p).FirstOrDefault();
        }

        /// <summary>
        /// Marca o erro como corrigido
        /// </summary>
        /// <param name="codErro"></param>
        public void corrigir(long codErro)
        {
            // altera
            Erro erro = (from p in this.ctx.Erros
                         where p.CodErro == codErro
                         select p).FirstOrDefault();

            erro.Resolvido = true;
            this.ctx.SubmitChanges();
        }

        #endregion
    }
}
