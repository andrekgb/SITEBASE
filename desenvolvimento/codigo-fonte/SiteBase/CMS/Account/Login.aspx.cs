using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Account
{
    public partial class Login : BaseWebForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
             * */

            System.Text.StringBuilder html = new System.Text.StringBuilder();

            html.AppendLine("<div class=\"alert alert-danger alert-dismissable\">");
            html.AppendLine("    <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>");
            html.AppendLine("    <strong>Falha de autenticação:</strong> seu login ou senha são inválidos");
            html.AppendLine("</div>");

            this.Login1.FailureText = html.ToString();
        }
    }
}