using Core.Helpers;
using Core.Models.Filters;
using Core.Models.Responses.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;

namespace Core.Queries.Gerencial
{

    public class GetResponsavelQuery : IRequest<Result<IEnumerable<ResponsavelResponse>>>
    {

        public GetResponsavelRequestFilter Filter;

        public GetResponsavelQuery(GetResponsavelRequestFilter filter)
        {
            Filter = filter;
        }

    }

}
