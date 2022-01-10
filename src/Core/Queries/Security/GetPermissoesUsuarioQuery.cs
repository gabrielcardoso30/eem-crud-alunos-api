using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetPermissoesUsuarioQuery : BaseQueryFilter, IRequest<Result<IEnumerable<PermissaoUsuarioResponse>>>
    {
        public Guid UsuarioId { get; set; }
        public GetPermissoesUsuarioQuery(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
