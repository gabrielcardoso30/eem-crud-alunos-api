using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class DeleteParametroSistemaCommandHandler : IRequestHandler<DeleteParametroSistemaCommand, Result>
    {
        private readonly IParametroSistemaRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;
        public DeleteParametroSistemaCommandHandler(IParametroSistemaRepository repository, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
        }
        public async Task<Result> Handle(DeleteParametroSistemaCommand request, CancellationToken cancellationToken)
        {
            var result = new Result();

            var parametroSistema = await _repository.GetByIdWithUserId(request.Id, _authenticatedUser.GuidLogin());
            if (parametroSistema == null)
            {
                result.WithNotFound("Parametro do sistema não encontrado");
                return result;
            }

            await _repository.DeleteAsync(parametroSistema);
                
            return result;
        }
    }
}
