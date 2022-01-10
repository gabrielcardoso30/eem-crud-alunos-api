using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class UpdatePermissaoGrupoCommand : IRequest<Result<List<PermissaoGrupoResponse>>>
    {
        public ICollection<Guid> PermissaoId { get; set; }
        public Guid GrupoId { get; set; }
        
        public UpdatePermissaoGrupoCommand(Guid grupoId, ICollection<Guid> permissaoId)
        {
            GrupoId = grupoId;
            PermissaoId = permissaoId;
        }

        public UpdatePermissaoGrupoCommand()
        {
            
        }
    }
}
