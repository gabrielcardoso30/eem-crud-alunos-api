using System;

namespace Core.Models.Responses.Security
{
    public class PermissaoUsuarioResponse
    {
        public Guid Id { get; set; }
        public PermissaoResponse Permissao { get; set; }
    }
}
