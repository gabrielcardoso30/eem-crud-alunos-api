using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Filters;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{

    public class GetUsuarioByFilterQuery : BaseQueryFilter, IRequest<Result<IEnumerable<UsuarioResponse>>>
    {

        public BaseRequestFilter Filter { get; set; }

        public GetUsuarioByFilterQuery(BaseRequestFilter filter)
        {
            Filter = filter;
        }

    }

}
