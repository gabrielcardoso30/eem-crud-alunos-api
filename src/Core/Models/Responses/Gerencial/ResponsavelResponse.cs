using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Responses.Gerencial
{

    public class ResponsavelResponse
    {

        public string Id { get; set; }
        public string AlunoId { get; set; }
        public string AlunoNome { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string DataNascimentoFormatada { get; set; }
        public string Parentesco { get; set; }
        public string ParentescoDescricao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

    }

}
