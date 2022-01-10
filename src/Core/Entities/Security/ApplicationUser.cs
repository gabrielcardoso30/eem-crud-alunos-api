using System;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Security
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Deletado { get; set; }
        public int TipoUsuario { get; set; }
        public int QuantidadeLogin { get; set; }
        public bool PrimeiroLogin { get; set; }
        public bool TermoUso { get; set; }
        public string CaminhoFoto { get; set; }
        public string TelefoneResidencial { get; set; }
        public int QuantidadePrimeiroAcesso { get; set; }
        public DateTime? DataBloqueioPrimeiroAcesso { get; set; }
        public string UrlImagem { get; set; }
        public string CPF { get; set; }
        public string PlayerId { get; set; }
        public string Discriminator { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public Guid? UsuarioIdUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
        public Guid? UnidadeAcessoSelecionada { get; set; }

        public ApplicationUser()
        {
            this.Discriminator = "AspNetUsers";
            Ativo = true;
        }
    }
}