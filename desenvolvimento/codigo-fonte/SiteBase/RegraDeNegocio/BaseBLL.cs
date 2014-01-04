using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegraDeNegocio
{
    /// <summary>
    /// Classe de regra de negócios
    /// </summary>
    public class BaseBLL : IDisposable
    {
        #region "Propriedades"

        /// <summary>
        /// Contexto LINQ.
        /// </summary>
        protected BancoDeDadosDataContext ctx = null;

        #endregion

        #region "Construtores"

        /// <summary>
        /// Construtor em branco.
        /// </summary>
        public BaseBLL()
        {
            ctx = new BancoDeDadosDataContext();
        }

        /// <summary>
        /// Construtor com contexto do LINQ.
        /// </summary>
        /// <param name="ctx_"></param>
        public BaseBLL(BancoDeDadosDataContext ctx_)
        {
            this.ctx = ctx_;
        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (ctx != null)
                ctx.Dispose();
        }

        #endregion
    }
}