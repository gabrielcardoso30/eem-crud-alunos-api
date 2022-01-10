using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetAuditoriaByIdQuery : IRequest<Result<AuditoriaResponse>>
    {
        public Guid Id;
        public GetAuditoriaByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
