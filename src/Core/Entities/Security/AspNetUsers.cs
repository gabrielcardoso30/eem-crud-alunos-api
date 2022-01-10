using System.Collections.Generic;

namespace Core.Entities.Security
{
    public class AspNetUsers : ApplicationUser
    {
        public virtual ICollection<AspNetUsersRefreshToken> AspNetUsersRefreshToken { get; set; }
        public virtual ICollection<Auditoria> Audit { get; set; }
        public virtual ICollection<GrupoAspNetUsers> GrupoAspNetUsers { get; set; }
        public virtual ICollection<PermissaoUsuario> PermissaoUsuario { get; set; }
        public virtual ICollection<ParametroSistema> ParametroSistema { get; set; }
    }
}