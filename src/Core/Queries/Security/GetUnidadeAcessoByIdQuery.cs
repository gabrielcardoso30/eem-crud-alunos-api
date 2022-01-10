using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetUnidadeAcessoByIdQuery : IRequest<Result<UnidadeAcessoResponse>>
    {
        public Guid Id;

        public GetUnidadeAcessoByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}