using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class DeletePermissaoGrupoCommandHandler : IRequestHandler<DeletePermissaoGrupoCommand, Result>
    {
        private readonly IPermissaoGrupoRepository _permissaoGrupoRepository;
        public DeletePermissaoGrupoCommandHandler(IPermissaoGrupoRepository permissaoGrupoRepository)
        {
            _permissaoGrupoRepository = permissaoGrupoRepository;
        }
        public async Task<Result> Handle(DeletePermissaoGrupoCommand request, CancellationToken cancellationToken)
        {
            var result = new Result();

            var grupo = await _permissaoGrupoRepository.GetById(request.Id);
            if (grupo == null)
            {
                result.WithNotFound("Permissão do grupo não encontrada!");
                return result;
            }

            await _permissaoGrupoRepository.DeleteAsync(grupo);

            return result;
        }
    }
}
