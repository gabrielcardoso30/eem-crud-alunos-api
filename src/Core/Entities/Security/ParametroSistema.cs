using System;
using Core.Enums;

namespace Core.Entities.Security
{
    public class ParametroSistema : BaseEntity
    {
        public EnumTipoParametro? TipoParametro { get; set; }
        public EnumTipoValorParametro? TipoValor { get; set; }
        public bool? ValorBit { get; set; }
        public string ValorTexto { get; set; }
        public Guid? AspNetUsersId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
