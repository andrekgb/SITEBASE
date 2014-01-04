using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using RegraDeNegocio;

namespace CMS.js.app
{
    /// <summary>
    /// Summary description for App
    /// </summary>
    public class App : IHttpHandler
    {

        /// <summary>
        /// Gera o javascript.
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("var appUrl_ = \"" + ConfigurationManager.AppSettings["PATH_CMS"].ToString() + "\";");
            context.Response.Write("var appUrlPortal_ = \"" + ConfigurationManager.AppSettings["PATH_SITE"].ToString() + "\";");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}