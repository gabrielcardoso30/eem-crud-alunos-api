using System;
using Core.Helpers;
using MediatR;

namespace Core.Commands.Security
{
    public class DeleteParametroSistemaCommand : IRequest<Result>
    {
        public Guid Id;
        public DeleteParametroSistemaCommand(Guid id)
        {
            Id = id;
        }
    }
}
