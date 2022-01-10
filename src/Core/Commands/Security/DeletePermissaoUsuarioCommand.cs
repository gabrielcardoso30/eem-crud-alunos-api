using System;
using Core.Helpers;
using MediatR;

namespace Core.Commands.Security
{
    public class DeletePermissaoUsuarioCommand : IRequest<Result>
    {
        public Guid Id;
        public DeletePermissaoUsuarioCommand(Guid id)
        {
            Id = id;
        }
    }
}
