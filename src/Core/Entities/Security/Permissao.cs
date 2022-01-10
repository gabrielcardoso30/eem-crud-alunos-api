using System.Collections.Generic;

namespace Core.Entities.Security
{
    public class Permissao : BaseEntity
    {
        public Permissao()
        {
            PermissaoGrupo = new HashSet<PermissaoGrupo>();
            PermissaoUsuario = new HashSet<PermissaoUsuario>();
        }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TipoUsuario { get; set; }

        public virtual ICollection<PermissaoGrupo> PermissaoGrupo { get; }
        public virtual ICollection<PermissaoUsuario> PermissaoUsuario { get; }
    }
}
