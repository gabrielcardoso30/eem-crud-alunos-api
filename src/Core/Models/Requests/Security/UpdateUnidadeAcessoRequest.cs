using System.Collections.Generic;

namespace Core.Models.Request.Security
{

    public class UpdateUnidadeAcessoRequest
    {

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public ICollection<string> Modulos { get; set; }

    }

}
