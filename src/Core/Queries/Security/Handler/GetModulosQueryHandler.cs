using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Enums.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{

    public class GetModulosQueryHandler : IRequestHandler<GetModulosQuery, Result<IEnumerable<KeyValuePair<string, string>>>>
    {

        public GetModulosQueryHandler()
        {
            
        }

        public async Task<Result<IEnumerable<KeyValuePair<string, string>>>> Handle(GetModulosQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<KeyValuePair<string, string>>>();
            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumModulo)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            result.Value = dic.ToArray();
            result.Count = dic.Count;
            result.TableCount = dic.Count;

            return result;

        }

    }

}
