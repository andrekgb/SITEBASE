using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Framework.Util
{
    /// <summary>
    /// Arquivo.
    /// </summary>
    public class Arquivo
    {

        /// <summary>
        /// Exclui os arquivos mais antigos que a data do parâmetro.
        /// </summary>
        /// <param name="pasta"></param>
        public static void limparPasta(string pasta, DateTime data)
        {
            if (System.IO.Directory.Exists(pasta))
            {
                string[] arquivos = System.IO.Directory.GetFiles(pasta);

                foreach (string arquivo in arquivos)
                {
                    if (System.IO.File.Exists(arquivo))
                    {
                        DateTime creation = System.IO.File.GetCreationTime(arquivo);
                        TimeSpan ts = data.Subtract(creation);

                        if (ts.Days > 0)
                        {
                            try
                            {
                                System.IO.File.Delete(arquivo);
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retorna um nome SEO friendly para o nome físico de um arquivo.
        /// Se o titulo sugerido for nulo retorno o nome atual do arquivo.
        /// </summary>
        /// <param name="tituloSugerido">Texto que será usado para montar o nome do arquivo</param>
        /// <param name="enderecoFisico">Endereço físico de onde o arquivo será salvo no HD do servidor</param>
        /// <param name="nomeAtual">Nome atual do arquivo</param>
        /// <returns>Nome SEO friendly para o nome físico de um arquivo</returns>
        public String gerarNomeAmigavel(String tituloSugerido, String enderecoFisico, String nomeAtual)
        {
            return gerarNomeAmigavel(tituloSugerido, enderecoFisico, nomeAtual, false);
        }

        /// <summary>
        /// Retorna um nome SEO friendly para o nome físico de um arquivo.
        /// Se o titulo sugerido for nulo retorno o nome atual do arquivo.
        /// </summary>
        /// <param name="tituloSugerido">Texto que será usado para montar o nome do arquivo</param>
        /// <param name="enderecoFisico">Endereço físico de onde o arquivo será salvo no HD do servidor</param>
        /// <param name="nomeAtual">Nome atual do arquivo</param>
        /// <returns>Nome SEO friendly para o nome físico de um arquivo</returns>
        public String gerarNomeAmigavel(String tituloSugerido, String enderecoFisico, String nomeAtual, bool validarThumb)
        {
            int max_length_nome_OS = 188;

            String retorno = null;

            // extensao
            string extensao = new System.IO.FileInfo(nomeAtual).Extension;

            // max length do nome (sem extensao)
            int max_length_nome = max_length_nome_OS - extensao.Length;

            // se nao houver titulo sugerido
            if (string.IsNullOrEmpty(tituloSugerido))
                tituloSugerido = nomeAtual.Replace(extensao, "");

            if (tituloSugerido.Length > max_length_nome)
                tituloSugerido = tituloSugerido.Substring(0, max_length_nome);

            string nome_original = Framework.Util.Texto.CleanStringFileName(tituloSugerido);
            string nome_temp = nome_original;

            int contador = 0;
            while (File.Exists(enderecoFisico + nome_temp + extensao) || (validarThumb && File.Exists(enderecoFisico + "thumb-" + nome_temp + extensao)))
            {
                contador++;

                if (nome_temp.Length + contador.ToString().Length >= max_length_nome)
                    nome_temp = nome_original.Substring(0, max_length_nome - contador.ToString().Length) + contador.ToString();
                else
                    nome_temp = nome_original + contador.ToString();

            }
            retorno = nome_temp + extensao;

            // retorno
            return retorno.ToLower();
        }

        /// <summary>
        /// Recebe por referencia a URL e URL físca e preenche com os caminhos no formado url/ano/abreviacao do mesmo.
        /// Se o caminho físico não existir ele é criado
        /// Exemplo: 
        /// url = http://puc-campinas.edu.br/midia/imagens/noticia/2011/Nov/
        /// urlFisico = C:\Site\midia\imagens\noticia\2011\Nov\
        /// 
        /// Cria na data ATUAL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlFisico"></param>
        public void gerarCaminhoMesAno(ref string url, ref string urlFisico)
        {
            this.gerarCaminhoMesAno(ref url, ref urlFisico, DateTime.Now);
        }

        /// <summary>
        /// Recebe por referencia a URL e URL físca e preenche com os caminhos no formado url/ano/abreviacao do mesmo.
        /// Se o caminho físico não existir ele é criado
        /// Exemplo: 
        /// url = http://puc-campinas.edu.br/midia/imagens/noticia/2011/Nov/
        /// urlFisico = C:\Site\midia\imagens\noticia\2011\Nov\
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlFisico"></param>
        /// <param name="data"></param>
        public void gerarCaminhoMesAno(ref string url, ref string urlFisico, DateTime data)
        {
            // Caminhos será ano\Nome-do-mês-abreviado. Ex.: 2011\Nov\
            urlFisico = urlFisico + data.Year.ToString() + "\\" + Mes.obter(data.Month, true) + "\\";
            url = url + data.Year.ToString() + "/" + Mes.obter(data.Month, true) + "/";

            // Verificar se a pasta já existe
            if (System.IO.Directory.Exists(urlFisico) == false)
                System.IO.Directory.CreateDirectory(urlFisico);
        }

    }
}
