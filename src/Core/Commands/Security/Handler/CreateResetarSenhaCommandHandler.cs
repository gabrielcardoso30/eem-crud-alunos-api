using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Services;
using Core.Models.Responses.Security;
using Core.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Commands.Security.Handler
{
    public class CreateResetarSenhaCommandHandler : IRequestHandler<CreateResetarSenhaCommand, Result<SenhaResponse>>
    {
        private ApplicationUser _applicationUser;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMsSendMail _msSendMail;

        public CreateResetarSenhaCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUsuarioRepository usuarioRepository,
            IMsSendMail msSendMail)
        {
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
            _msSendMail = msSendMail;
        }

        public async Task<Result<SenhaResponse>> Handle(CreateResetarSenhaCommand request,
            CancellationToken cancellationToken)
        {
            var result = new Result<SenhaResponse>();
                
            _applicationUser = await _userManager.FindByNameAsync(request.UserName);
            if (_applicationUser == null)
            {
                result.WithError("O login informado não está cadastrado no sistema, favor verificar.");
                return result;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(_applicationUser);
            var password = AccessManager.GenerateNumericPassword(8);
                
            var identityResult = await _userManager.ResetPasswordAsync(_applicationUser, token, password);

            //Reseta a Quantidade de Login para 0 quando reseta a senha
            var usuario = await _usuarioRepository.GetById(_applicationUser.Id);
            usuario.QuantidadeLogin = 0;
            usuario.PrimeiroLogin = true;
            await _usuarioRepository.UpdateAsync(usuario);
                
            if (!identityResult.Succeeded)
            {
                result.WithError(identityResult.Errors.FirstOrDefault()?.Description);
                return result;
            }
            
            if (_applicationUser.Email != null)
            {
                var message = @"Senha Resetada <br>" +
                                    "Login: " + request.UserName + "<br>" +
                                    "Senha: " + password;
                    
                await _msSendMail.SendMailAsync(_applicationUser.Email, "Senha resetada", message, "Usuário",
                    _applicationUser.Id.ToString(), 0);
            }

            result.Value = new SenhaResponse { UserName = request.UserName, SenhaTemporaria = password};
                
            return result;
        }
    }
}