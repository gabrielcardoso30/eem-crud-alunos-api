using System;
using System.Collections.Generic;
using Core.Helpers;
using MediatR;

namespace Core.Commands.Security
{

    public class DeleteGrupoCommand : IRequest<Result>
    {

        public Guid Id { get; set; }
        public ICollection<Guid> Ids { get; set; }

        public DeleteGrupoCommand(Guid id, ICollection<Guid> ids)
        {
            Id = id;
            Ids = ids;
        }

        public DeleteGrupoCommand()
        {

        }

    }

}
