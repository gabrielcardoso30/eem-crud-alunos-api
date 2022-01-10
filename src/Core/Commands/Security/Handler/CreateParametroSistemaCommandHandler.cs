using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class CreateParametroSistemaCommandHandler : IRequestHandler<CreateParametroSistemaCommand, Result<ParametroSistemaResponse>>
    {
        private readonly IParametroSistemaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IUsuarioRepository _usuarioRepository;
        public CreateParametroSistemaCommandHandler(IParametroSistemaRepository repository, IMapper mapper,
            IAuthenticatedUser authenticatedUser, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Result<ParametroSistemaResponse>> Handle(CreateParametroSistemaCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<ParametroSistemaResponse>();
            var usuarioLogado = await _usuarioRepository.GetById(_authenticatedUser.GuidLogin());

            var parametroExist =
                (await _repository.Get(usuarioLogado.Id, request.TipoParametro, 100, 0, null, true))
                .Result(out var count).FirstOrDefault(f => f.Deletado == false);
                
            if (parametroExist == null)
            {
                var parametroSistemaNew = _mapper.Map<ParametroSistema>(request);
                parametroSistemaNew.AspNetUsers = usuarioLogado;
                
                var parametroSistema = await _repository.AddAsync(parametroSistemaNew);
                result.Value = _mapper.Map<ParametroSistemaResponse>(parametroSistema);
            }
            else
            {
                var parametroSistemaUpdate = _mapper.Map(request, parametroExist);
                await _repository.UpdateAsync(parametroSistemaUpdate);
                result.Value = _mapper.Map<ParametroSistemaResponse>(parametroSistemaUpdate);
            }
               
            return result;
        }
    }
}
