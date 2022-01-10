using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class CreatePermissaoUsuarioCommandHandler : IRequestHandler<CreatePermissaoUsuarioCommand, Result<List<PermissaoUsuarioResponse>>>
    {
        private readonly IPermissaoUsuarioRepository _permissaoUsuarioRepository;
        private readonly IMapper _mapper;
        public CreatePermissaoUsuarioCommandHandler(IPermissaoUsuarioRepository permissaoUsuarioRepository, IMapper mapper)
        {
            _permissaoUsuarioRepository = permissaoUsuarioRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<PermissaoUsuarioResponse>>> Handle(CreatePermissaoUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<List<PermissaoUsuarioResponse>>();

            var lista = new List<PermissaoUsuarioResponse>();
                
            foreach (var item in request.PermissaoId)
            {
                var permissaoGrupoNew = new PermissaoUsuario()
                {
                    AspNetUsersId = request.UsuarioId,
                    PermissaoId = item
                };
                var permissaoGrupo = await _permissaoUsuarioRepository.AddAsync(permissaoGrupoNew);
                lista.Add(_mapper.Map<PermissaoUsuarioResponse>(permissaoGrupo));
            }

            result.Value = lista;
            return result;
        }
    }
}
