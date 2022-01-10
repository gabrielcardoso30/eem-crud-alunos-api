using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Filters;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{

    public class GetGrupoByFilterQuery : BaseQueryFilter, IRequest<Result<IEnumerable<GrupoResponse>>>
    {

        public BaseRequestFilter Filter { get; set; }

        public GetGrupoByFilterQuery(BaseRequestFilter filter)
        {
            Filter = filter;
        }

    }

}
