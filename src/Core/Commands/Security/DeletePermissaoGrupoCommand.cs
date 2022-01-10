using System;
using Core.Helpers;
using MediatR;

namespace Core.Commands.Security
{
    public class DeletePermissaoGrupoCommand : IRequest<Result>
    {
        public Guid Id;
        public DeletePermissaoGrupoCommand(Guid id)
        {
            Id = id;
        }
    }
}
