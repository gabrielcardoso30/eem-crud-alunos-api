using System.Collections.Generic;
using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetModulosByGrupoIdQuery : IRequest<Result<IEnumerable<KeyValuePair<string, string>>>>
    {
        public Guid Id;

        public GetModulosByGrupoIdQuery(Guid id)
        {
            Id = id;
        }
    }
}