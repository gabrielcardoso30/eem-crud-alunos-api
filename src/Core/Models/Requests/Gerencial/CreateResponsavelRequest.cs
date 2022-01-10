using System;

namespace Core.Models.Requests.Gerencial
{

    public class CreateResponsavelRequest
    {

        
        public Guid AlunoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Parentesco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

    }

}
