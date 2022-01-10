using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{

    public class GetModulosQuery : BaseQueryFilter, IRequest<Result<IEnumerable<KeyValuePair<string, string>>>>
    {

        public GetModulosQuery()
        {
            
        }

    }

}
