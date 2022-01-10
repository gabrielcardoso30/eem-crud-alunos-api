using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{

    public class CreatePermissaoUsuarioCommand : IRequest<Result<List<PermissaoUsuarioResponse>>>
    {

        public ICollection<Guid> PermissaoId { get; set; }
        public Guid UsuarioId { get; set; }
        
        public CreatePermissaoUsuarioCommand(
            Guid usuarioId, 
            ICollection<Guid> permissaoId
        )
        {
            UsuarioId = usuarioId;
            PermissaoId = permissaoId;
        }

        public CreatePermissaoUsuarioCommand()
        {

        }

    }

}
