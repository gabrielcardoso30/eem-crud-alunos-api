using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Responses.Gerencial
{

    public class AlunoResponse
    {

        public string Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string DataNascimentoFormatada { get; set; }
        public string Segmento { get; set; }
        public string SegmentoDescricao { get; set; }
        public string FotoUrl { get; set; }
        public string Email { get; set; }
        public ICollection<ResponsavelResponse> Responsaveis { get; set; }

    }

}
