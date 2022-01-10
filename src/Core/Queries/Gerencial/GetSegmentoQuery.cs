using System;
using System.Collections.Generic;
using Core.Helpers;
using MediatR;

namespace Core.Queries.Gerencial
{

    public class GetSegmentoQuery : BaseQueryFilter, IRequest<Result<IEnumerable<KeyValuePair<string, string>>>>
    {

        public GetSegmentoQuery()
        {
            
        }

    }

}
