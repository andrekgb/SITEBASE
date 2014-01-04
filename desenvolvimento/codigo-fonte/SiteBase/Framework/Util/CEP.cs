using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Util
{
    /// <summary>
    /// Classe de utilidade usada para consulta de CEP no site dos Correios
    /// Referência: http://coisaserradas.com.br/consultar-cep-com-o-webservice-dos-correios-em-asp-net-c/
    /// </summary>
    public class CEP
    {
        #region "Métodos"

        /// <summary>
        /// Faz a busca do CEP consultado o site dos correios.
        /// Esta função faz a requisição e busca os valores no meio do HTML.
        /// Quando não encontra o CEP, seta o TipoRetorno com erro e preenche a propriedade MensagemErro.
        /// O Objeto de retorno sempre é preenchido. 
        /// </summary>
        /// <param name="cep">CEP. Pode ser informado com ou sem marcara. Antes de fazer a consulta a string é limpa para ficar apenas os caracteres numericos.</param>
        /// <returns></returns>
        public CEPTO BuscarCEP(string cep)
        {
            CEPTO retorno = new CEPTO();

            if(string.IsNullOrEmpty(cep) == false)
                cep = Regex.Replace(cep, @"[^\d]", "");

            //* Verifica se o cep digitado é válido.
            Match regex = Regex.Match(cep, "^[0-9]{8}$");

            if (regex.Success)
            {
                try
                {

                    string parametros = "cepEntrada=" + cep.Replace("-", "").Trim() + "&tipoCep=&cepTemp=&metodo=buscarCep";

                    WebRequest request = WebRequest.Create("http://m.correios.com.br/movel/buscaCepConfirma.do?" + parametros);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));

                    string dados = stream.ReadToEnd();
                    int count = 0;
                    string ExpressaoRegular = "<span class=\"respostadestaque\">(.*?)</span>";
                    MatchCollection endereco = Regex.Matches(dados, ExpressaoRegular, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (endereco.Count == 0)
                    {
                        retorno.MensagemErro = "CEP Não Encontrado";
                        retorno.TipoRetorno = TipoRetorno.CEP_Nao_Encontrado;
                    }
                    else
                    {
                        retorno.TipoRetorno = TipoRetorno.Sucesso;

                        foreach (Match resultado in endereco)
                        {
                            count++;
                            switch (count) //Preencho um resultado por vez
                            {
                                case 1: // Nesse caso eu separei "Rua João da Silva" para o Rua ser carregado do dropdown e o resto de endereço em uma TextBox
                                    retorno.Logradouro = resultado.Groups[1].Value.Trim();
                                    break;
                                case 2:
                                    retorno.Bairro = resultado.Groups[1].Value.Trim();
                                    break;
                                case 3: //Aqui tem uma particularidade como eu disse que é algumas consultas em banco, e fiquei com preguiça de filtar, mas sei que você irá entender
                                    try
                                    {
                                        retorno.Estado = RemoverCaracteres(resultado.Groups[1].Value.Trim().Split('/')[1]).Trim();
                                        retorno.Cidade = RemoverCaracteres(resultado.Groups[1].Value.Trim().Split('/')[0]).Trim();
                                    }
                                    catch (Exception)
                                    {
                                        retorno.MensagemErro = "CEP Não Encontrado";
                                        retorno.TipoRetorno = TipoRetorno.CEP_Nao_Encontrado;
                                    }
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    retorno.MensagemErro = "Não foi possível fazer a consulta por CEP.";
                    retorno.TipoRetorno = TipoRetorno.CEP_Invalido;
                }
            }
            else
            {
                retorno.MensagemErro = "O CEP Informado é inválido.";
                retorno.TipoRetorno = TipoRetorno.CEP_Invalido;
            }

            return retorno;
        }

        /// <summary>
        /// Função auxiliar para limpar strings
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private string RemoverCaracteres(string texto)
        {
            string resultado = texto;

            resultado = resultado.Replace("\n", "");
            resultado = resultado.Replace("\r", "");
            resultado = resultado.Replace("\t", "");
            resultado = resultado.Trim();

            return resultado;
        }  

        #endregion
    }

    /// <summary>
    /// Classe TO para transportar os valores do endereço encontrado
    /// </summary>
    public class CEPTO
    {

        #region "Propriedades"

        
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string MensagemErro { get; set; }
        public TipoRetorno TipoRetorno { get; set; }

        #endregion

        #region "Construtores"

        /// <summary>
        /// Construtor vazio
        /// </summary>
        public CEPTO() { }

        #endregion 

    }

    /// <summary>
    /// Enum para retornar erros
    /// </summary>
    public enum TipoRetorno { 
        Sucesso,
        CEP_Nao_Encontrado,
        CEP_Invalido
    }
}
