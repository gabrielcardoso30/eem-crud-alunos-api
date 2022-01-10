using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetGruposQuery : BaseQueryFilter, IRequest<Result<IEnumerable<GrupoResponse>>>
    {
        
    }
}
