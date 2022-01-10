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
    public class GetGrupoPermissoesQueryHandler : IRequestHandler<GetGrupoPermissoesQuery, Result<IEnumerable<PermissaoResponse>>>
    {
        private readonly IPermissaoGrupoRepository _permissaoGrupoRepository;
        private readonly IMapper _mapper;
        
        public GetGrupoPermissoesQueryHandler(IPermissaoGrupoRepository permissaoGrupoRepository,
            IMapper mapper)
        {
            _permissaoGrupoRepository = permissaoGrupoRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<PermissaoResponse>>> Handle(GetGrupoPermissoesQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<PermissaoResponse>>();
                
            var permissoesGrupo = await _permissaoGrupoRepository.Get(query.Id);

            var permissoes = permissoesGrupo.Select(p => _mapper.Map<PermissaoResponse>(p.Permissao)).ToList();

            result.Value = permissoes;

            return result;
        }
    }
}
