using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{

    public class CreateGrupoAspNetUsersCommand : IRequest<Result<List<GrupoAspNetUsersResponse>>>
    {

        public Guid Id;
        public ICollection<Guid> GrupoId { get; set; }

        public CreateGrupoAspNetUsersCommand(Guid id, ICollection<Guid> grupoId)
        {
            Id = id;
            GrupoId = grupoId;
        }

        public CreateGrupoAspNetUsersCommand()
        {

        }

    }

}
