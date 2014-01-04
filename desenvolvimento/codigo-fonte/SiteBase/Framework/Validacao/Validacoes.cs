using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Framework.Util;

namespace Framework.Validacao
{
    /// <summary>
    /// Classe genérica de validação
    /// </summary>
    public static class Validacoes
    {
        #region "Métodos"
        /// <summary>
        /// Validação de e-mails
        /// </summary>
        /// <param name="email">e-mail a ser validado</param>
        /// <returns>Indicação se o e-mail é válido</returns>
        public static bool emailValido(string email)
        {

            if (email == "")
                return false;

            // Expressão regular que vai validar os e-mails
            string emailRegex = @"^(([^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|"
            + @"(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

            // Instância da classe Regex, passando como 
            // argumento sua Expressão Regular 
            Regex rx = new Regex(emailRegex);

            // Método IsMatch da classe Regex que retorna
            // verdadeiro caso o e-mail passado estiver
            // dentro das regras da sua regex.
            return rx.IsMatch(email);
        }
        //-------------------------------------------------------------------------
        /// <summary>
        /// Validação de CPF
        /// </summary>
        /// <param name="cpf">CPF a ser validado. A string pode conter "." ou "-" que a função retira antes de fazer os calculos</param>
        /// <returns>Indicação se o CPF é válido</returns>
        public static bool cpfValido(string cpf)
        {

            if (cpf == "")
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            else if (cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999" || cpf == "00000000000")
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);

        }
        //-------------------------------------------------------------------------
        /// <summary>
        /// Validação de CNPJ
        /// </summary>
        /// <param name="vrCNPJ">CNPJ a ser validado</param>
        /// <returns>Indicação se o CNPJ é válido</returns>
        public static bool cnpjValido(string vrCNPJ)
        {

            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";

            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;
            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }
                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
        //-------------------------------------------------------------------------
        /// <summary>
        /// Validação de URL
        /// </summary>
        /// <param name="url">URL a ser validada. Pode, ou não, conter "http://"</param>
        /// <returns>Indicação se a URL é válida</returns>
        public static bool urlValida(string url)
        {
            Regex RgxUrl = new Regex("(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?");
            return RgxUrl.IsMatch(url);
        }

        /// <summary>
        /// Valida se a URL do YouTube é valida.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool isUrlYouTubeValida(string url)
        {
            StringBuilder html = new StringBuilder();
            UrlParametersParser parametrosParser = new UrlParametersParser("");

            // valida url
            if (url.Contains("youtube.com") == false)
                return false;

            // valida se a url possui o ID do video
            if (url != null && url.Length > 0)
            {
                if (url.Contains("?"))
                {
                    string[] url_split = url.Split('?');
                    if (url_split.Length > 1)
                    {
                        parametrosParser = new UrlParametersParser(url_split[1]);
                        if (parametrosParser.Parameters["v"] != null)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
        #endregion
    }
}