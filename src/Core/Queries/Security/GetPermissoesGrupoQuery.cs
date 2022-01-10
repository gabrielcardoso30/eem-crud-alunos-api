using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{

    public class GetPermissoesGrupoQuery : BaseQueryFilter, IRequest<Result<IEnumerable<PermissaoGrupoResponse>>>
    {

        public Guid GrupoId { get; set; }

        public GetPermissoesGrupoQuery(Guid grupoId)
        {
            GrupoId = grupoId;
        }

        public GetPermissoesGrupoQuery()
        {

        }

    }

}
