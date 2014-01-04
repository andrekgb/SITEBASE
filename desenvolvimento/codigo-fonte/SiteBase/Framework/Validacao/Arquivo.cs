using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Validacao
{
    /// <summary>
    /// Arquivo.
    /// </summary>
    public class Arquivo
    {
        #region "Propriedades"

        private String arquivo;

        #endregion

        #region "Construtores"

        /// <summary>
        /// Construtor com o nome do arquivo.
        /// </summary>
        /// <param name="arquivo_"></param>
        public Arquivo(String arquivo_)
        {
            this.arquivo = arquivo_;
        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// Valida se é uma imagem.
        /// </summary>
        /// <returns></returns>
        public Boolean isImagem()
        {
            if (arquivo.ToLower().EndsWith(".gif") || arquivo.ToLower().EndsWith(".jpg") || arquivo.ToLower().EndsWith(".jpeg") || arquivo.ToLower().EndsWith(".png"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Valida se é um arquivo de retorno.
        /// </summary>
        /// <returns></returns>
        public Boolean isArquivoRetorno()
        {
            if (arquivo.ToLower().EndsWith(".txt") || arquivo.ToLower().EndsWith(".ret"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Valida se é um arquivo de flash.
        /// </summary>
        /// <returns></returns>
        public Boolean isFlash()
        {
            if (arquivo.ToLower().EndsWith(".swf"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Valida se é um arquivo válido para anúncio.
        /// </summary>
        /// <returns></returns>
        public int isAnuncio()
        {
            if (arquivo.ToLower().EndsWith(".gif") || arquivo.ToLower().EndsWith(".jpg") || 
                arquivo.ToLower().EndsWith(".jpeg") || arquivo.ToLower().EndsWith(".png"))
            {
                return 1;
            }
            else if (isFlash())
            {
                return 2;
            }

            return 0;
        }

        #endregion

    }
}
