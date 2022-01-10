using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{

    public class DeleteUsuarioCommand : IRequest<Result>
    {

        public Guid Id { get; set; }
        public ICollection<Guid> Ids { get; set; }

        public DeleteUsuarioCommand(Guid id, ICollection<Guid> ids)
        {
            Id = id;
            Ids = ids;
        }

        public DeleteUsuarioCommand()
        {
           
        }

    }

}
