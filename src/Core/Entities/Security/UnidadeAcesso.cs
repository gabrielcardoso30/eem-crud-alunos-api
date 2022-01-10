using System;
using System.Collections.Generic;

namespace Core.Entities.Security
{

    public class UnidadeAcesso
    {

        public Guid Id { get; set; }
        public string Nome { get; set; }                        
        public DateTime DataCriacao { get; set; }
        public bool Deletado { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public Guid? UsuarioIdUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<UnidadeAcessoModulo> UnidadeAcessoModulo { get; }
        public virtual ICollection<GrupoUnidadeAcesso> GrupoUnidadeAcesso { get; }

        public UnidadeAcesso()
        {

            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Deletado = false;
            Ativo = true;

        }

    }

}
