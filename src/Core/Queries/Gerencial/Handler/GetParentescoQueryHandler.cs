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

    public class GetParentescoQueryHandler : IRequestHandler<GetParentescoQuery, Result<IEnumerable<KeyValuePair<string, string>>>>
    {

        public GetParentescoQueryHandler()
        {
            
        }

        public async Task<Result<IEnumerable<KeyValuePair<string, string>>>> Handle(GetParentescoQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<KeyValuePair<string, string>>>();
            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumParentesco)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            result.Value = dic.ToArray();
            result.Count = dic.Count;
            result.TableCount = dic.Count;

            return result;

        }

    }

}
