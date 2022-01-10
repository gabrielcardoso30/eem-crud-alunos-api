using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Commands.Security.Handler
{
    public class CreateAlterarSenhaCommandHandler : IRequestHandler<CreateAlterarSenhaCommand, Result<SenhaResponse>>
    {
        private ApplicationUser _applicationUser;
        private UserManager<ApplicationUser> _userManager;

        public CreateAlterarSenhaCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<SenhaResponse>> Handle(CreateAlterarSenhaCommand request,
            CancellationToken cancellationToken)
        {
            var result = new Result<SenhaResponse>();
                
            _applicationUser = await _userManager.FindByNameAsync(request.UserName);
            if (_applicationUser == null)
                return null;
                
            var identityResult = await _userManager.ChangePasswordAsync(_applicationUser,
                request.SenhaAntiga, 
                request.SenhaNova);

            _applicationUser.PrimeiroLogin = false;
            await _userManager.UpdateAsync(_applicationUser);
                
            if (!identityResult.Succeeded)
            {
                var erro = identityResult.Errors.FirstOrDefault();
                if (erro.Code == "PasswordMismatch")
                {
                    result.WithError("Login incorreto.");
                }
                return result;
            }
                
            result.Value = new SenhaResponse{ UserName = request.UserName };
            return result;
        }
    }
}