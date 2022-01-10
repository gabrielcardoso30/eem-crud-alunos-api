using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Gerencial
{

    public class Aluno : BaseEntity
    {

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Segmento { get; set; }
        public string FotoUrl { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Responsavel> Responsaveis { get; }

    }

}
