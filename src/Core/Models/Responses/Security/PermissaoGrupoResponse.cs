using System;

namespace Core.Models.Responses.Security
{
    public class PermissaoGrupoResponse
    {
        public Guid Id { get; set; }
        public PermissaoResponse Permissao { get; set; }
    }
}
