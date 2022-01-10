using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetParametroSistemaByIdQuery : IRequest<Result<ParametroSistemaResponse>>
    {
        public Guid Id;
        public GetParametroSistemaByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
