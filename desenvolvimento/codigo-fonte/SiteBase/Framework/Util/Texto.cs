using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

namespace Framework.Util
{
    /// <summary>
    /// Texto.
    /// </summary>
    public class Texto
    {
        #region "Metodos"

        /// <summary>
        /// Remove tags HTML.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static String RemoveHtml(String html)
        {
            if (html == null || html.Equals(string.Empty))
                return string.Empty;
            else
                return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
        }

        /// <summary>
        /// Truncate do texto [sem quebrar palavras].
        /// </summary>
        /// <param name="input"></param>
        /// <param name="characterLimit"></param>
        /// <returns></returns>
        public static String Truncate(String input, int characterLimit)
        {
            String output = input;

            //Check if the string is longer than the allowed amount otherwise do nothing
            if (output.Length > characterLimit && characterLimit > 0)
            {
                //cut the string down to the maximum number of characters
                output = output.Substring(0, characterLimit);

                //Check if the character right after the truncate point was a space
                //if not, we are in the middle of a word and need to remove the rest of it
                if (input.Substring(output.Length, 1).Equals(" ") == false)
                {
                    int LastSpace = output.LastIndexOf(" ");

                    //if we found a space then, cut back to that space
                    if (LastSpace != -1)
                        output = output.Substring(0, LastSpace);
                }

                output += "...";
            }

            return output;
        }

        /// <summary>
        /// Função para remover acentos de uma string.
        /// </summary>
        /// <param name="_texto">Texto a ser tratado.</param>
        /// <returns>Texto sem acentuação.</returns>
        /// <remarks></remarks>
        public String removeAcentos(string _texto)
        {
            string ComAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string SemAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            StringBuilder retorno = new StringBuilder();
            int i = 0;

            for (i = 0; i <= _texto.Length - 1; i++)
            {
                if (ComAcentos.IndexOf(Convert.ToChar(_texto.Substring(i, 1))) >= 0)
                {
                    retorno.Append(SemAcentos.Substring(ComAcentos.IndexOf(Convert.ToChar(_texto.Substring(i, 1))), 1));
                }
                else
                {
                    retorno.Append(_texto.Substring(i, 1));
                }
            }

            return retorno.ToString();
        }

        /// <summary>
        /// Limpa uma String para ser passada na URL.
        /// </summary>
        public static String CleanString(String str)
        {
            StringBuilder sb = new StringBuilder();

            //trim
            str = HttpContext.Current.Server.HtmlDecode(str.Trim());

            //fix ampersand...
            str = str.Replace("&", "e");

            //normalize the Unicode
            str = str.Normalize(NormalizationForm.FormKD);

            for (int i = 0; i <= str.Length - 1; i++)
            {
                if ((char.IsWhiteSpace(str[i])))
                {
                    sb.Append("-");
                }
                else if (char.GetUnicodeCategory(str[i]) != UnicodeCategory.NonSpacingMark && !char.IsPunctuation(str[i]) && !char.IsSymbol(str[i]))
                {
                    sb.Append(str[i]);

                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Limpa uma String para ser utilizada em nome de arquivos.
        /// </summary>
        public static String CleanStringFileName(String str)
        {
            StringBuilder sb = new StringBuilder();

            //trim
            str = HttpContext.Current.Server.HtmlDecode(str.Trim());

            //fix ampersand...
            str = str.Replace("&", "e");

            //normalize the Unicode
            str = str.Normalize(NormalizationForm.FormKD);

            for (int i = 0; i <= str.Length - 1; i++)
            {
                if ((char.IsWhiteSpace(str[i]) || str[i].ToString().Equals("-")))
                {
                    sb.Append("-");
                }
                else if (char.GetUnicodeCategory(str[i]) != UnicodeCategory.NonSpacingMark && !char.IsPunctuation(str[i]) && !char.IsSymbol(str[i]))
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
