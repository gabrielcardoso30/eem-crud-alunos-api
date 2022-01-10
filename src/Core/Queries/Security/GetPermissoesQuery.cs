using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetPermissoesQuery : IRequest<Result<IEnumerable<PermissaoResponse>>>
    {
        public GetPermissoesQuery()
        {
        }
    }
}
