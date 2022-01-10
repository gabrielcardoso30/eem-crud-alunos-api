using System;
using System.Collections.Generic;

namespace Core.Models.Responses.Security
{
    public class GrupoResponse
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Deletado { get; set; }
        public string Nome { get; set; }
        public bool Padrao { get; set; }
        public IEnumerable<PermissaoResponse> Permissoes { get; set; }
        public IEnumerable<UnidadeAcessoResponse> UnidadesAcesso { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Modulos { get; set; }
    }
}
