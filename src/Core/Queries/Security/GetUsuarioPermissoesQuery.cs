using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetUsuarioPermissoesQuery : IRequest<Result<IEnumerable<PermissaoResponse>>>
    {
        public Guid Id;
        public GetUsuarioPermissoesQuery(Guid id)
        {
            Id = id;
        }
    }
}
