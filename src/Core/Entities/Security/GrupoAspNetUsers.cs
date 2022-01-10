using System;

namespace Core.Entities.Security
{
    public class GrupoAspNetUsers : BaseEntity
    {
        public Guid AspNetUsersId { get; set; }
        public Guid GrupoId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}
