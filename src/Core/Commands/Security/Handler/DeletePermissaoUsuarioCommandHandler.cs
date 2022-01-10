using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class DeletePermissaoUsuarioCommandHandler : IRequestHandler<DeletePermissaoUsuarioCommand, Result>
    {
        private readonly IPermissaoUsuarioRepository _permissaoUsuarioRepository;
        public DeletePermissaoUsuarioCommandHandler(IPermissaoUsuarioRepository permissaoUsuarioRepository)
        {
            _permissaoUsuarioRepository = permissaoUsuarioRepository;
        }
        public async Task<Result> Handle(DeletePermissaoUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new Result();

            var grupo = await _permissaoUsuarioRepository.GetById(request.Id);
            if (grupo == null)
            {
                result.WithNotFound("Permissão do usuário não encontrada!");
                return result;
            }

            await _permissaoUsuarioRepository.DeleteAsync(grupo);

            return result;
        }
    }
}
