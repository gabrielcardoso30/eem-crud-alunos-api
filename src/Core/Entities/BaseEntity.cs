using System;

namespace Core.Entities
{

    public abstract class BaseEntity
    {

        public Guid Id { get; set; }
        public Guid UnidadeAcessoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Deletado { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public Guid? UsuarioIdUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }

        protected BaseEntity()
        {

            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Deletado = false;
            Ativo = true;

        }

    }

}
