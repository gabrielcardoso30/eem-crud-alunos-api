using System;

namespace Core.Models.Requests.Gerencial
{

    public class UpdateAlunoRequest
    {

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Segmento { get; set; }
        public string FotoBase64 { get; set; }
        public string FotoTipo { get; set; }
        public string Email { get; set; }

    }

}
