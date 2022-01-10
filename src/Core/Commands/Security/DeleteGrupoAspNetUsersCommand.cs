using System;
using Core.Helpers;
using MediatR;

namespace Core.Commands.Security
{
    public class DeleteGrupoAspNetUsersCommand : IRequest<Result>
    {
        public Guid Id;
        public DeleteGrupoAspNetUsersCommand(Guid id)
        {
            Id = id;
        }
    }
}
