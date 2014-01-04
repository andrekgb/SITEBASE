
namespace RegraDeNegocio
{
    partial class BancoDeDadosDataContext
    {
        /// <summary>
        /// </summary>
        public BancoDeDadosDataContext() :
            base(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, mappingSource)
        {
            OnCreated();
        }

    }
}
