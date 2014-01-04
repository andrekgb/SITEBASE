using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace Framework.Seguranca
{
    /// <summary>
    /// Classe de criptografia.
    /// </summary>
    public class Criptografia
    {
        #region "Construtores"

        /// <summary>
        /// Construtor vazio.
        /// </summary>
        public Criptografia()
        {
        }

        #endregion

        #region "Propriedades"

        // chave [definir para cada projeto]
        private static byte[] key = Encoding.UTF8.GetBytes("Inv&p@r1546%%".Substring(0, 8));
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #endregion

        #region "Metodos"

        /// <summary>
        /// Criptografa uma string.
        /// </summary>
        /// <param name="strText">Texto a ser criptografado.</param>
        /// <returns></returns>
        public static string Encrypt(string strText)
        {
            byte[] inputByteArray;

            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descriptografa uma string.
        /// </summary>
        /// <param name="strText">Texto a ser descriptografado.</param>
        /// <returns></returns>
        public static string Decrypt(string strText)
        {
            byte[] inputByteArray = new byte[strText.Length];

            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
