using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Enums.Gerencial;
using Core.Helpers;
using MediatR;

namespace Core.Queries.Gerencial.Handler
{

    public class GetSegmentoQueryHandler : IRequestHandler<GetSegmentoQuery, Result<IEnumerable<KeyValuePair<string, string>>>>
    {

        public GetSegmentoQueryHandler()
        {
            
        }

        public async Task<Result<IEnumerable<KeyValuePair<string, string>>>> Handle(GetSegmentoQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<KeyValuePair<string, string>>>();
            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumSegmento)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            result.Value = dic.ToArray();
            result.Count = dic.Count;
            result.TableCount = dic.Count;

            return result;

        }

    }

}
