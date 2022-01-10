using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Commands.Security
{

    public class CreateUsuarioCommand : IRequest<Result<UsuarioResponse>>
    {

        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int TipoUsuario { get; set; }
        public IFormFile File { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public string Chave { get; set; }
        public bool RealizaContagem { get; set; }

        public CreateUsuarioCommand(
            string nome,
            string login, 
            string email, 
            string telefone, 
            int tipoUsuario, 
            IFormFile file, 
            string cpf,
            string matricula,
            bool realizaContagem,
            string chave
        )
        {
            Nome = nome;
            Login = login;
            Email = email;
            Telefone = telefone;
            TipoUsuario = tipoUsuario;
            File = file;
            Cpf = cpf;
            Matricula = matricula;
            RealizaContagem = realizaContagem;
            Chave = chave;
        }

        public CreateUsuarioCommand()
        {

        }

    }    
    
}
