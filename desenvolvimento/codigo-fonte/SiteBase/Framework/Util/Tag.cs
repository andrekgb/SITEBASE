using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Util
{
    /// <summary>
    /// Gera tags.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Retorna a tag de flash.
        /// </summary>
        /// <returns></returns>
        public static string obterFlash(string arquivo, int width, int height)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" ");
            html.Append("codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" ");
            html.Append("width=\"" + width.ToString() + "\" height=\"" + height.ToString() +"\">");
            html.Append("<param name=\"movie\" value=\"" + arquivo + "\">");
            html.Append("<param name=\"quality\" value=\"high\">");
            html.Append("<embed src=\"" + arquivo +"\" quality=\"high\" width=\"" + width.ToString() + "\" ");
            html.Append("height=\"" + height.ToString() + "\" type=\"application/x-shockwave-flash\" ");
            html.Append("pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed></object>");

            return html.ToString();
        }
    }
}