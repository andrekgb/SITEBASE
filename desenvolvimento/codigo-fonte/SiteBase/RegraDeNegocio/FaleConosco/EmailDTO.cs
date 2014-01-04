using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegraDeNegocio.FaleConosco
{
    /// <summary>
    /// Classe DTO para carregar os e-mails pelo form e outras funções
    /// </summary>
    [Serializable]
    public class EmailDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
