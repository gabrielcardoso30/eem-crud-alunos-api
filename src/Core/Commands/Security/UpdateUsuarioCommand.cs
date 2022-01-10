using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Commands.Security
{
    public class UpdateUsuarioCommand : IRequest<Result<UsuarioResponse>>
    {
        public Guid Id;
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string TelefoneResidencial { get; set; }
        public int TipoUsuario { get; set; } = 0;
        public string Cpf { get; set; }
        public string Host { get; set; }
        public string PlayerId { get; set; }
        public string Matricula { get; set; }
        public string Chave { get; set; }
        public bool RealizaContagem { get; set; }
        public bool Ativo { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirmacao { get; set; }

        public UpdateUsuarioCommand(
            Guid id, 
            string nome,
            string login, 
            string email, 
            string telefone, 
            string telefoneResidencial,
            int tipoUsuario, 
            string cpf, 
            string host, 
            string playerId,
            string matricula,
            bool realizaContagem,
            bool ativo,
            string senha,
            string senhaConfirmacao,
            string chave
        )
        {
            Id = id;
            Nome = nome;
            Login = login;
            Email = email;
            Telefone = telefone;
            TelefoneResidencial = telefoneResidencial;
            TipoUsuario = tipoUsuario;
            Cpf = cpf;
            Host = host;
            PlayerId = playerId;
            Matricula = matricula;
            RealizaContagem = realizaContagem;
            Ativo = ativo;
            Senha = senha;
            SenhaConfirmacao = senhaConfirmacao;
            Chave = chave;
        }

        public UpdateUsuarioCommand()
        {

        }

    }
}