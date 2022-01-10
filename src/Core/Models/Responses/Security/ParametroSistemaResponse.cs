using System;
using Core.Enums;

namespace Core.Models.Responses.Security
{
    public class ParametroSistemaResponse
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Deletado { get; set; }
        public EnumTipoParametro? TipoParametro { get; set; }
        public EnumTipoValorParametro? TipoValor { get; set; }
        public bool? ValorBit { get; set; }
        public string ValorTexto { get; set; }
        
    }
}
