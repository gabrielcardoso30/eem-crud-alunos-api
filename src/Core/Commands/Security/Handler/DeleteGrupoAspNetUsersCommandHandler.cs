using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class DeleteGrupoAspNetUsersCommandHandler : IRequestHandler<DeleteGrupoAspNetUsersCommand, Result>
    {
        private readonly IGrupoAspNetUsersRepository _repository;
        public DeleteGrupoAspNetUsersCommandHandler(IGrupoAspNetUsersRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result> Handle(DeleteGrupoAspNetUsersCommand request, CancellationToken cancellationToken)
        {
            var result = new Result();

            var grupo = await _repository.GetById(request.Id);
            if (grupo == null)
            {
                result.WithNotFound("Grupo de associação não encontrado!");
                return result;
            }
                
            await _repository.DeleteAsync(grupo);

            return result;
        }
    }
}
