using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class CreatePermissaoGrupoCommand : IRequest<Result<List<PermissaoGrupoResponse>>>
    {

        public ICollection<Guid> PermissaoId { get; set; }
        public Guid GrupoId { get; set; }

        public CreatePermissaoGrupoCommand(Guid grupoId, ICollection<Guid> permissaoId)
        {
            GrupoId = grupoId;
            PermissaoId = permissaoId;
        }

        public CreatePermissaoGrupoCommand()
        {

        }

    }
}
