using System.Collections.Generic;

namespace Core.Entities.Security
{
    public class Grupo : BaseEntity
    {
        public Grupo()
        {
            PermissaoGrupo = new HashSet<PermissaoGrupo>();
        }
        public string Nome { get; set; }
        public bool Padrao { get; set; }

        public virtual IEnumerable<GrupoAspNetUsers> GrupoAspNetUsers { get; }
        public virtual IEnumerable<PermissaoGrupo> PermissaoGrupo { get; }
        public virtual IEnumerable<GrupoModulo> GrupoModulo { get; }
        public virtual IEnumerable<AspNetUsers> AspNetUsers { get; set; } = new List<AspNetUsers>();
    }
}
