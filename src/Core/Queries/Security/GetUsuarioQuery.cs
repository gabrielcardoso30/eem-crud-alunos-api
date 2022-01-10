using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetUsuarioQuery : BaseQueryFilter, IRequest<Result<IEnumerable<UsuarioResponse>>>
    {
        public Guid? GrupoId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Cpf { get; set; }
        public GetUsuarioQuery(Guid? grupoId, string nome, string login, string cpf )
        {
            GrupoId = grupoId;
            Nome = nome; 
            Login = login; 
            Cpf = cpf; 
        }
    }
}
