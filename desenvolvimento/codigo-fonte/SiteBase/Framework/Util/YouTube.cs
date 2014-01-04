using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Util
{
    /// <summary>
    /// Classe para gerar o HTML Embeded dos vídeos do youtube
    /// </summary>
    public static class YouTube
    {
        /// <summary>
        /// Retorna a string para inclusão no HTML
        /// </summary>
        /// <param name="urlVideo">URL do vídeo do youtube</param>
        /// <returns></returns>
        public static string gerarHTMlVideo(string urlVideo)
        {
            return gerarHTMlVideo(urlVideo, 0, 0);
        }

        /// <summary>
        /// Retorna a url da imagem do video do YouTube.
        /// </summary>
        /// <param name="video">Url do YouTube.</param>
        /// <param name="isHD">Alta resolução?</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string getYouTubeImage(string video, bool isHD)
        {
            UrlParametersParser parametrosParser = new UrlParametersParser("");

            if (video.Contains("?"))
            {
                string[] url_split = video.Split('?');
                if (url_split.Length > 1)
                {
                    parametrosParser = new UrlParametersParser(url_split[1]);
                }

                if (isHD)
                {
                    return "http://i3.ytimg.com/vi/" + parametrosParser.Parameters["v"] + "/hqdefault.jpg";
                }
                else
                {
                    return "http://i3.ytimg.com/vi/" + parametrosParser.Parameters["v"] + "/default.jpg";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Retorna a string para inclusão no HTML
        /// </summary>
        /// <param name="urlVideo">URL do vídeo do youtube</param>
        /// <param name="width">Largura do palyer</param>
        /// <param name="height">Altura do player</param>
        /// <returns></returns>
        public static string gerarHTMlVideo(string urlVideo, int width, int height)
        {
            // tratamento das variaveis
            if (width == 0)
                width = 480;

            if (height == 0)
                height = 385;

            StringBuilder html = new StringBuilder();

            UrlParametersParser parametrosParser = new UrlParametersParser("");

            // id do video
            String idYouTube = null;
            if (urlVideo != null && urlVideo.Length > 0)
            {
                if (urlVideo.Contains("?"))
                {
                    string[] url_split = urlVideo.Split('?');
                    if (url_split.Length > 1)
                    {
                        parametrosParser = new UrlParametersParser(url_split[1]);
                        idYouTube = parametrosParser.Parameters["v"].ToString();
                    }
                }
            }

            // renderiza
            html.Append("<object width=\"" + width.ToString() + "\" height=\"" + height.ToString() + "\"> ");
            html.Append("<param name=\"movie\" value=\"http://www.youtube.com/v/" + idYouTube + "?fs=1&amp;hl=pt_BR&amp;rel=0\"></param> ");
            html.Append("<param name=\"allowFullScreen\" value=\"true\"></param> ");
            html.Append("<param name=\"allowscriptaccess\" value=\"always\"></param> ");
            html.Append("<embed src=\"http://www.youtube.com/v/" + idYouTube + "?fs=1&amp;hl=pt_BR&amp;rel=0\"  ");
            html.Append("    type=\"application/x-shockwave-flash\"  ");
            html.Append("    allowscriptaccess=\"always\"  ");
            html.Append("    allowfullscreen=\"true\"  ");
            html.Append("    width=\"" + width.ToString() + "\"  ");
            html.Append("    height=\"" + height.ToString() + "\"> ");
            html.Append(" </embed> ");
            html.Append(" </object> ");

            // retorno
            return html.ToString();
        }
    }
}
