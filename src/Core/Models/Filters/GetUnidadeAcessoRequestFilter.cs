using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Filters
{

    public class GetUnidadeAcessoRequestFilter : BaseRequestFilter
    {

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public bool ModuloInventario { get; set; }
        public bool ModuloRdo { get; set; }
        public bool ModuloNm { get; set; }
        public bool ModuloContetores { get; set; }
        public bool ModuloFrotas { get; set; }
        public bool ModuloPreservacao { get; set; }

    }

}
