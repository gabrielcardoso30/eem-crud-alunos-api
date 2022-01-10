using Core.Enums;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class CreateParametroSistemaCommand : IRequest<Result<ParametroSistemaResponse>>
    {
        public EnumTipoParametro? TipoParametro { get; set; }
        public EnumTipoValorParametro? TipoValor { get; set; }
        public bool? ValorBit { get; set; }
        public string ValorTexto { get; set; }

        public CreateParametroSistemaCommand(
            EnumTipoParametro? tipoParametro,
            EnumTipoValorParametro? tipoValor,
            bool? valorBit, string valorTexto)
        {
            TipoParametro = tipoParametro;
            TipoValor = tipoValor;
            ValorBit = valorBit;
            ValorTexto = valorTexto;
        }
    }
}