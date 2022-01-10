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
    public class DesbloquearUsuarioCommandHandler : IRequestHandler<DesbloquearUsuarioCommand, Result<UsuarioResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
        public DesbloquearUsuarioCommandHandler(IUsuarioRepository usuarioRepository, 
            IMapper mapper,
            IAuthenticatedUser authenticatedUser)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }
        public async Task<Result<UsuarioResponse>> Handle(DesbloquearUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<UsuarioResponse>();

            if (_authenticatedUser.GuidLogin() == request.Id)
            {
                result.WithException("Não é possível o próprio usuário se desbloquear!");
                return result;
            }
                
            var usuarioUpdate = await _usuarioRepository.GetById(request.Id);
            if (usuarioUpdate == null)
            {
                result.WithNotFound("Usuario não encontrado!");
                return result;
            }

            usuarioUpdate.QuantidadeLogin = 0;
            usuarioUpdate.QuantidadePrimeiroAcesso = 0;
            usuarioUpdate.DataBloqueioPrimeiroAcesso= null;
            await _usuarioRepository.UpdateAsync(usuarioUpdate);
                
            result.Value = _mapper.Map<UsuarioResponse>(usuarioUpdate);

            return result;
        }
    }
}
