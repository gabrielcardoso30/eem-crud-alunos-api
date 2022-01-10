using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Gerencial
{

    public class Responsavel : BaseEntity
    {

        public Guid AlunoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Parentesco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public virtual Aluno Aluno { get; set; }

    }

}
