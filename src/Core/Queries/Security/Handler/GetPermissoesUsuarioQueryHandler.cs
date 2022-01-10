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
    public class GetPermissoesUsuarioQueryHandler : IRequestHandler<GetPermissoesUsuarioQuery, Result<IEnumerable<PermissaoUsuarioResponse>>>
    {
        private readonly IPermissaoUsuarioRepository _permissaoUsuarioRepository;
        private readonly IMapper _mapper;
        public GetPermissoesUsuarioQueryHandler(IPermissaoUsuarioRepository permissaoUsuarioRepository, IMapper mapper)
        {
            _permissaoUsuarioRepository = permissaoUsuarioRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<PermissaoUsuarioResponse>>> Handle(GetPermissoesUsuarioQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<PermissaoUsuarioResponse>>();

            var perfis = await _permissaoUsuarioRepository.Get(query.UsuarioId);
            result.Value = perfis.Select(p => _mapper.Map<PermissaoUsuarioResponse>(p));

            return result;
        }
    }
}
