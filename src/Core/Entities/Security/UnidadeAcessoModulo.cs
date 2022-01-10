using System;

namespace Core.Entities.Security
{

    public class UnidadeAcessoModulo
    {

        public Guid Id { get; set; }
        public Guid UnidadeAcessoId { get; set; }                        
        public string Modulo { get; set; }                        
        public DateTime DataCriacao { get; set; }

        public virtual UnidadeAcesso UnidadeAcesso { get; set; }

        public UnidadeAcessoModulo()
        {

            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;

        }

    }

}
