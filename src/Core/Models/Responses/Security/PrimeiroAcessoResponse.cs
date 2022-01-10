using System;
using System.Collections.Generic;

namespace Core.Models.Responses.Security
{
    public class PrimeiroAcessoResponse
    {
        public bool Termo { get; set; }
        public IEnumerable<PerguntaPrimeiroAcesso> Perguntas { get; set; } 
    }

    public class PerguntaPrimeiroAcesso
    {
        public Guid PerguntaId { get; set; }
        public string Pergunta { get; set; }
    }
}
