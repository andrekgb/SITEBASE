using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Framework.Util
{
    /// <summary>
    /// Classe que efetua o tratamento dos parâmetros de uma URL.
    /// </summary>
    public class UrlParametersParser
    {
        #region "Propriedades"

        string _urlParameters = "";
        string _urlAnchor = "";
        NameValueCollection _nameValues = new NameValueCollection();

        public string Anchor
        {
            get { return this._urlAnchor; }
        }

        public bool HasAnchor
        {
            get { return (!string.IsNullOrEmpty(this._urlAnchor)); }
        }

        public NameValueCollection Parameters
        {
            get { return _nameValues; }
        }

        public int Count
        {
            get { return _nameValues.Count; }
        }

        public string getParameter(String name)
        {
            return _nameValues[name];
        }

        public string getParameter(int index)
        {
            return _nameValues[index];
        }

        #endregion

        #region "Propriedades"

        public UrlParametersParser()
        {
        }
        public UrlParametersParser(String urlParameters)
        {
            this._urlParameters = urlParameters;
            this.ParseURLParameters();
        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// Gera uma lista com os parametros de uma querystring.
        /// </summary>
        /// <remarks></remarks>
        public void ParseURLParameters()
        {
            string urlTemp = this._urlParameters;
            int index = 0;

            //get the url end anchor (#blah) if there is one...
            _urlAnchor = "";
            index = urlTemp.LastIndexOf("#");

            if ((index > 0))
            {
                //there's an anchor
                _urlAnchor = urlTemp.Substring(index + 1);

                //remove the anchor from the url...
                urlTemp = urlTemp.Remove(index);
            }

            _nameValues.Clear();
            string[] arrayPairs = urlTemp.Split(new char[] { '&' });

            foreach (var tValue in arrayPairs)
            {
                if ((tValue.Trim().Length > 0))
                {
                    //parse...
                    string[] nvalue = tValue.Trim().Split(new char[] { '=' });

                    if ((nvalue.Length.Equals(1)))
                    {
                        _nameValues.Add(nvalue[0], string.Empty);
                    }
                    else if ((nvalue.Length > 1))
                    {
                        _nameValues.Add(nvalue[0], nvalue[1]);
                    }
                }
            }
        }

        /// <summary>
        /// Cria querystring com os parametros, com a possibilidade de se omitir os parametros de "excludeValues".
        /// </summary>
        /// <param name="excludeValues">Parametros a se omitir.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CreateQueryString(string[] excludeValues)
        {
            StringBuilder queryString = new StringBuilder("");
            bool bFirst = true;

            for (int i = 0; i <= _nameValues.Count - 1; i++)
            {
                string key = _nameValues.Keys[i].ToLower();
                string value = _nameValues[i];

                if ((!KeyInsideArray(excludeValues, key)))
                {
                    if ((bFirst))
                    {
                        bFirst = false;
                    }
                    else
                    {
                        queryString.Append("&");
                    }
                    queryString.Append(key + "=" + value);
                }
            }

            return queryString.ToString();
        }

        private bool KeyInsideArray(string[] array, string key)
        {
            foreach (var tmp in array)
            {
                if ((tmp.Equals(key)))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

    }
}