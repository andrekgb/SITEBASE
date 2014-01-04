using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Framework.Util;

namespace RegraDeNegocio
{
    /// <summary>
    /// Classe que trata regra de negócio da URL.
    /// </summary>
    public static class UrlBLL
    {
        #region "CMS"
        /// Url do CMS.
        /// </summary>
        public static string obterCMS(bool isCaminhoFisico)
        {
            if (isCaminhoFisico)
                return ConfigurationManager.AppSettings["PHYSICAL_PATH_CMS"];
            else
                return ConfigurationManager.AppSettings["PATH_CMS"];
        }

        /// <summary>
        /// Url fisico [temporaria] do CMS.
        /// </summary>
        public static string obterCMSTemp(bool isCaminhoFisico)
        {
            return obterCMS(isCaminhoFisico) + "temp/";
        }

        /// <summary>
        /// Url absoluta [temporaria] do CMS.
        /// </summary>
        public static string obterCMSTempFisicoAbsolute()
        {
            return VirtualPathUtility.ToAbsolute("~/temp/");
        }

        /// <summary>
        /// Url absoluta [temporaria] do Site.
        /// </summary>
        public static string obterSiteTempFisicoAbsolute()
        {
            return VirtualPathUtility.ToAbsolute("~/midia/temp/");
        }

        /// <summary>
        /// Url fisico [temporaria] do CMS.
        /// </summary>
        public static string obterCMSTempFisico()
        {
            return obterCMS(true) + "temp/";
        }


        #endregion
    }
}
