using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class CreateLogoutCommand : IRequest<Result<LogoutResponse>>
    {
    }
}
