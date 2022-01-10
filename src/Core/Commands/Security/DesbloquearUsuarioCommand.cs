using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class DesbloquearUsuarioCommand : IRequest<Result<UsuarioResponse>>
    {
        public Guid Id;
        public DesbloquearUsuarioCommand(Guid id)
        {
            Id = id;
        }
    }
}
