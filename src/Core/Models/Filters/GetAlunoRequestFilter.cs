using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Filters
{

    public class GetAlunoRequestFilter : BaseRequestFilter
    {

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Segmento { get; set; }
        public string Email { get; set; }

    }

}
