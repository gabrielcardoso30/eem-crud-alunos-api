using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;

namespace Core.Commands.Security.Handler
{
    public class UpdatePlayerIdCommandHandler : IRequestHandler<UpdatePlayerIdCommand, Result<Result>>
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UpdatePlayerIdCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<Result>> Handle(UpdatePlayerIdCommand request, CancellationToken cancellationToken)
        {
            var retorno = new Result<Result>();

            if (string.IsNullOrEmpty(request.PlayerId))
            {
                retorno.WithError("Informe o playerId");
                return retorno;
            }

            var usuario = await _usuarioRepository.GetById(request.Id);

            usuario.PlayerId = request.PlayerId;

            await _usuarioRepository.UpdateAsync(usuario);

            return retorno;
        }
    }
}
