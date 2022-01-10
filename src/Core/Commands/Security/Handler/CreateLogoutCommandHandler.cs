using System.Threading;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Security;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Commands.Security.Handler
{
    public class CreateLogoutCommandHandler : IRequestHandler<CreateLogoutCommand, Result<LogoutResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IAuthenticatedUser _authenticatedUser;

        public CreateLogoutCommandHandler(UserManager<ApplicationUser> userManager,
            IAuthenticatedUser authenticatedUser)
        {
            _userManager = userManager;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<LogoutResponse>> Handle(CreateLogoutCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<LogoutResponse>();
                
            var userIdentity = await _userManager.FindByIdAsync(_authenticatedUser.GuidLogin().ToString());

            // Removendo playerId
            userIdentity.PlayerId = null;

            await _userManager.UpdateAsync(userIdentity);
                
            await _userManager.RemoveAuthenticationTokenAsync(userIdentity,
                "Token",
                "Whitelist");
                
            result.Value = new LogoutResponse { Authenticated = false, Message = "Logout efetuado com sucesso!"};
                
            return result;
        }
    }
}