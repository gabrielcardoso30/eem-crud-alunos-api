using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetGrupoPermissoesQuery : IRequest<Result<IEnumerable<PermissaoResponse>>>
    {
        public Guid Id;
        public GetGrupoPermissoesQuery(Guid id)
        {
            Id = id;
        }
    }
}
