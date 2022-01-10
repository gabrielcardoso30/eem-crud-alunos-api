using System;

namespace Core.Entities.Security
{
    public class PermissaoGrupo
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid GrupoId { get; set; }
        public Guid PermissaoId { get; set; }

        public virtual Grupo Grupo { get; set; }
        public virtual Permissao Permissao { get; set; }
    }
}
