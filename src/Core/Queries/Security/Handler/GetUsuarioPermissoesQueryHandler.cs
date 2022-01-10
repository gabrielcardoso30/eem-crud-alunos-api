using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{
    public class GetUsuarioPermissoesQueryHandler : IRequestHandler<GetUsuarioPermissoesQuery, Result<IEnumerable<PermissaoResponse>>>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IPermissaoGrupoRepository _permissaoGrupoRepository;
        private readonly IPermissaoUsuarioRepository _permissaoUsuarioRepository;
        private readonly IMapper _mapper;
        
        public GetUsuarioPermissoesQueryHandler(IUsuarioRepository repository, 
            IPermissaoUsuarioRepository permissaoUsuarioRepository,
            IPermissaoGrupoRepository permissaoGrupoRepository,
            IMapper mapper)
        {
            _repository = repository;
            _permissaoUsuarioRepository = permissaoUsuarioRepository;
            _permissaoGrupoRepository = permissaoGrupoRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<PermissaoResponse>>> Handle(GetUsuarioPermissoesQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<PermissaoResponse>>();
                
            var usuario = await _repository.GetByIdWithPermission(query.Id);
            if (usuario == null)
            {
                result.WithNotFound("Usuário não encontrada!");
                return result;
            }
                
            var permissoesUsuario = await _permissaoUsuarioRepository.Get(query.Id);
            var permissoesGrupo = new List<PermissaoGrupo>();

            foreach (var grupo in usuario.GrupoAspNetUsers)
            {
                permissoesGrupo.AddRange(grupo.Grupo.PermissaoGrupo);
            }
              
            var permissoes = permissoesUsuario.Select(p => _mapper.Map<PermissaoResponse>(p.Permissao)).ToList();
            permissoes.AddRange(permissoesGrupo.Select(p => _mapper.Map<PermissaoResponse>(p.Permissao)).ToList());

            result.Value = permissoes.Distinct();
            result.Count = result.Value.Count();

            return result;
        }
    }
}
