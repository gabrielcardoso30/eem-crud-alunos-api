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
    public class CreatePermissaoGrupoCommandHandler : IRequestHandler<CreatePermissaoGrupoCommand, Result<List<PermissaoGrupoResponse>>>
    {
        private readonly IPermissaoGrupoRepository _permissaoGrupoRepository;
        private readonly IMapper _mapper;
        public CreatePermissaoGrupoCommandHandler(IPermissaoGrupoRepository permissaoGrupoRepository, IMapper mapper)
        {
            _permissaoGrupoRepository = permissaoGrupoRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<PermissaoGrupoResponse>>> Handle(CreatePermissaoGrupoCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<List<PermissaoGrupoResponse>>();
            var lista = new List<PermissaoGrupoResponse>();
                
            foreach (var item in request.PermissaoId)
            {
                var permissaoGrupoNew = new PermissaoGrupo()
                {
                    GrupoId = request.GrupoId,
                    PermissaoId = item
                };
                var permissaoGrupo = await _permissaoGrupoRepository.AddAsync(permissaoGrupoNew);
                lista.Add(_mapper.Map<PermissaoGrupoResponse>(permissaoGrupo));

            }

            result.Value = lista;

            return result;
        }
    }
}
