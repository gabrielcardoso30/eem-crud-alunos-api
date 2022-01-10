using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetGrupoByIdQuery : IRequest<Result<GrupoResponse>>
    {
        public Guid Id;
        public GetGrupoByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
