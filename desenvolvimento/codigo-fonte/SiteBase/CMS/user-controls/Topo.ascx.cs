using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using RegraDeNegocio;

namespace CMS.user_controls
{
    public partial class Topo : System.Web.UI.UserControl
    {
        #region "Propriedades"

        /// <summary>
        /// Define o menu do CMS ao qual a página atual pertence
        /// </summary>
        public RegraDeNegocio.TypeMenuCMS Menu { get; set; }

        #endregion

        #region "Eventos"

        /// <summary>
        /// Ao carregar o controle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.configurar();
        }

        #endregion

        #region "Métodos"

        /// <summary>
        /// Configura o user controle
        /// </summary>
        private void configurar()
        {

            StringBuilder html = new StringBuilder();

            html.AppendLine("<ul class=\"nav\">");
            //================================================
            // Configurar Site            
            html.AppendLine("	<li class=\"dropdown" + (this.Menu == TypeMenuCMS.Congiguracao_Portal ? " active" : "") + "\">");
            html.AppendLine("		<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            html.AppendLine("			<i class=\"icon-cog\"></i>");
            html.AppendLine("			Configurar Portal");
            html.AppendLine("			<b class=\"caret\"></b>");
            html.AppendLine("		</a>");
            html.AppendLine("		<ul class=\"dropdown-menu\">");
            html.AppendLine("			<li class=\"nav-header\">Textos</li>");
            html.AppendLine("			<li><a href=\"" + UrlBLL.obterCMS(false) + "configurar-portal/textos/textos-fixos/\">Textos Fixos</a></li>");
            html.AppendLine("			<li><a href=\"" + UrlBLL.obterCMS(false) + "configurar-portal/textos/arvores/\">Árvores</a></li>");
            html.AppendLine("			<li><a href=\"" + UrlBLL.obterCMS(false) + "faq/\">FAQ</a></li>");
            html.AppendLine("		</ul>");
            html.AppendLine("	</li>      ");


            //================================================
            // Conteúdo
            
            html.AppendLine("	<li class=\"dropdown" + (this.Menu == TypeMenuCMS.Conteudo ? " active" : "") + "\">");
            html.AppendLine("		<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            html.AppendLine("			<i class=\"icon-file\"></i>");
            html.AppendLine("			Conteúdo");
            html.AppendLine("			<b class=\"caret\"></b>");
            html.AppendLine("		</a>");
            html.AppendLine("		<ul class=\"dropdown-menu\">");
            html.AppendLine("			<li><a href=\"#\">Páginas</a></li>");
            html.AppendLine("			<li><a href=\"#\">Notícias</a></li>");
            html.AppendLine("			<li><a href=\"#\">Portfólio</a></li>");
            html.AppendLine("		</ul>");
            html.AppendLine("	</li>");
            //================================================
            // Fale Conosco

                
            html.AppendLine("	<li class=\"dropdown" + (this.Menu == TypeMenuCMS.Fale_Conosco ? " active" : "") + "\">");
            html.AppendLine("		<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            html.AppendLine("			<i class=\"icon-envelope\"></i>");
            html.AppendLine("			Fale Conosco");
            html.AppendLine("			<b class=\"caret\"></b>");
            html.AppendLine("		</a>");
            html.AppendLine("		<ul class=\"dropdown-menu\">");
            html.AppendLine("			<li><a href=\"" + UrlBLL.obterCMS(false) + "FaleConosco/assuntos/\">Assuntos</a></li>");
            html.AppendLine("			<li><a href=\"" + UrlBLL.obterCMS(false) + "FaleConosco/formularios/\">Formulários enviados</a></li>");
            html.AppendLine("		</ul>");
            html.AppendLine("	</li> ");
                          

            //================================================
            // Aplicação            

            html.AppendLine("	<li class=\"dropdown" + (this.Menu == TypeMenuCMS.Aplicacao ? " active" : "") + "\">");
            html.AppendLine("		<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            html.AppendLine("			<i class=\"icon-off\"></i>");
            html.AppendLine("			Aplicação");
            html.AppendLine("			<b class=\"caret\"></b>");
            html.AppendLine("		</a>");
            html.AppendLine("		<ul class=\"dropdown-menu\">");

            html.AppendLine("			<li><a href=\"" + UrlBLL.obterCMS(false) + "Usuarios/\">Cadastro de Usuários</a></li>");
            html.AppendLine("			<li><a href=\"" + UrlBLL.obterCMS(false) + "Erros/\">Log de Erros</a></li>");

            html.AppendLine("		</ul>");
            html.AppendLine("	</li>");
            //================================================

            html.AppendLine("</ul>");

            this.lblMenu.Text = html.ToString();

        }

        #endregion
    }
}