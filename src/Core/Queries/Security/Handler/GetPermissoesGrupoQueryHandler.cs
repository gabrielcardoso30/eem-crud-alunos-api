using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{
    public class GetPermissoesGrupoQueryHandler : IRequestHandler<GetPermissoesGrupoQuery, Result<IEnumerable<PermissaoGrupoResponse>>>
    {
        private readonly IPermissaoGrupoRepository _permissaoGrupoRepository;
        private readonly IMapper _mapper;
        public GetPermissoesGrupoQueryHandler(IPermissaoGrupoRepository permissaoGrupoRepository, IMapper mapper)
        {
            _permissaoGrupoRepository = permissaoGrupoRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<PermissaoGrupoResponse>>> Handle(GetPermissoesGrupoQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<PermissaoGrupoResponse>>();

            var perfis = await _permissaoGrupoRepository.Get(query.GrupoId);
            result.Value = perfis.Select(p => _mapper.Map<PermissaoGrupoResponse>(p));

            return result;
        }
    }
}
