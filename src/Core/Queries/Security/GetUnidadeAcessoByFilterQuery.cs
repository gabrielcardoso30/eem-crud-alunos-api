using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Filters;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{

    public class GetUnidadeAcessoByFilterQuery : BaseQueryFilter, IRequest<Result<IEnumerable<UnidadeAcessoResponse>>>
    {

        public BaseRequestFilter Filter { get; set; }

        public GetUnidadeAcessoByFilterQuery(BaseRequestFilter filter)
        {
            Filter = filter;
        }

    }

}
