using System;

namespace Core.Entities.Security
{

    public class GrupoModulo
    {

        public Guid Id { get; set; }
        public Guid GrupoId { get; set; }                        
        public string Modulo { get; set; }                        
        public DateTime DataCriacao { get; set; }

        public virtual Grupo Grupo { get; set; }

        public GrupoModulo()
        {

            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;

        }

    }

}
