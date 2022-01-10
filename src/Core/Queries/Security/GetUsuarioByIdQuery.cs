using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetUsuarioByIdQuery : IRequest<Result<UsuarioResponse>>
    {
        public Guid Id;

        public GetUsuarioByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}