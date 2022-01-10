using System.Collections.Generic;
using Core.Enums;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetParametroSistemaQuery : BaseQueryFilter, IRequest<Result<IEnumerable<ParametroSistemaResponse>>>
    {
        public EnumTipoParametro? TipoParametro { get; set; }  
        public GetParametroSistemaQuery(EnumTipoParametro? tipoParametro)
        {
            TipoParametro = tipoParametro;
        }
    }
}
