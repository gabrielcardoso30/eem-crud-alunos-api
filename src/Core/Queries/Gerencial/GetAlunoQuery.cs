using Core.Helpers;
using Core.Models.Filters;
using Core.Models.Responses.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;

namespace Core.Queries.Gerencial
{

    public class GetAlunoQuery : IRequest<Result<IEnumerable<AlunoResponse>>>
    {

        public GetAlunoRequestFilter Filter;

        public GetAlunoQuery(GetAlunoRequestFilter filter)
        {
            Filter = filter;
        }

    }

}
