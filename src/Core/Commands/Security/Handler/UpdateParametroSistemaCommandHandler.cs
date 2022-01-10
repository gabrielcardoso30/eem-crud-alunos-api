using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class UpdateParametroSistemaCommandHandler : IRequestHandler<UpdateParametroSistemaCommand, Result<ParametroSistemaResponse>>
    {
        private readonly IParametroSistemaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
        public UpdateParametroSistemaCommandHandler(IParametroSistemaRepository repository, IMapper mapper, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }
        public async Task<Result<ParametroSistemaResponse>> Handle(UpdateParametroSistemaCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<ParametroSistemaResponse>();

            var parametroSistema = await _repository.GetByIdWithUserId(request.Id, _authenticatedUser.GuidLogin());
            if (parametroSistema == null)
            {
                result.WithNotFound("Parametro do sistema não encontrado!");
                return result;
            }

            parametroSistema = _mapper.Map(request, parametroSistema);
                
            if (await _repository.UpdateAsync(parametroSistema))
                result.Value = _mapper.Map<ParametroSistemaResponse>(parametroSistema);

            return result;
        }
    }
}
