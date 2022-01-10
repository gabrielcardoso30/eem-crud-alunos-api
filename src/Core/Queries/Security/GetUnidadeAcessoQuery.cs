using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Filters;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{

    public class GetUnidadeAcessoQuery : BaseQueryFilter, IRequest<Result<IEnumerable<UnidadeAcessoResponse>>>
    {

        public GetUnidadeAcessoRequestFilter Filter;

        public GetUnidadeAcessoQuery(GetUnidadeAcessoRequestFilter filter)
        {
            Filter = filter;
        }

    }

}
