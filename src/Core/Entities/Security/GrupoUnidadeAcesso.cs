using System;

namespace Core.Entities.Security
{

    public class GrupoUnidadeAcesso
    {

        public Guid Id { get; set; }
        public Guid GrupoId { get; set; }                        
        public Guid UnidadeAcessoId { get; set; }                        
        public DateTime DataCriacao { get; set; }

        public virtual UnidadeAcesso UnidadeAcesso { get; set; }

        public GrupoUnidadeAcesso()
        {

            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;

        }

    }

}
