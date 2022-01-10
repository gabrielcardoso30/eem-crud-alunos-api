using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{

    public class UpdatePermissaoUsuarioCommand : IRequest<Result<List<PermissaoUsuarioResponse>>>
    {

        public ICollection<Guid> PermissaoId { get; set; }
        public Guid UsuarioId { get; set; }
        
        public UpdatePermissaoUsuarioCommand(Guid usuarioId, ICollection<Guid> permissaoId)
        {
            UsuarioId = usuarioId;
            PermissaoId = permissaoId;
        }

        public UpdatePermissaoUsuarioCommand()
        {
            
        }

    }
    
}
