using System;

namespace Core.Models.Requests.Gerencial
{

    public class CreateAlunoRequest
    {

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Segmento { get; set; }
        public string FotoUrl { get; set; }
        public string Email { get; set; }

    }

}
