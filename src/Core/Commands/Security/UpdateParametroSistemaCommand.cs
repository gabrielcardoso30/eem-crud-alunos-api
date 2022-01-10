using System;
using Core.Enums;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class UpdateParametroSistemaCommand : IRequest<Result<ParametroSistemaResponse>>
    {
        public Guid Id;
        public EnumTipoParametro? TipoParametro { get; set; }
        public EnumTipoValorParametro? TipoValor { get; set; }
        public bool? ValorBit { get; set; }
        public string ValorTexto { get; set; }

        public UpdateParametroSistemaCommand(Guid id, 
            EnumTipoParametro? tipoParametro,
            EnumTipoValorParametro? tipoValor,
            bool? valorBit,
            string valorTexto)
        {
            Id = id;
            TipoParametro = tipoParametro;
            TipoValor = tipoValor;
            ValorBit = valorBit;
            ValorTexto = valorTexto;
        }
    }
}