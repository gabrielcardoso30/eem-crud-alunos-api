using System;
using System.Collections.Generic;

namespace Core.Models.Responses.Security
{

    public class UsuarioResponse
    {

        public Guid Id { get; set; }        
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Inativo { get; set; }
        public string Telefone { get; set; }
        public int TipoUsuario { get; set; }
        public int QuantidadeLogin { get; set; }
        public int QuantidadePrimeiroAcesso { get; set; }
        public DateTime? DataBloqueioPrimeiroAcesso { get; set; }
        public bool TermoUso { get; set; }
        public string UrlImagem { get; set; }
        public string TelefoneResidencial { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public string Chave { get; set; }
        public bool RealizaContagem { get; set; }
        public string Senha { get; set; }
        public IEnumerable<GrupoAspNetUsersResponse> GrupoAspNetUsers { get; set; }
        public string SelectedAccessUnitId { get; set; }
        public string SelectedAccessUnitName { get; set; }

    }

}
