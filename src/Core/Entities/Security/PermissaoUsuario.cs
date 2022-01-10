using System;

namespace Core.Entities.Security
{
    public class PermissaoUsuario
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid AspNetUsersId { get; set; }
        public Guid PermissaoId { get; set; }

        public virtual Permissao Permissao { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
