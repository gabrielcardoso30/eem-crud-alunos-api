using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class CreateResetarSenhaCommand : IRequest<Result<SenhaResponse>>
    {
        public string UserName { get; set; }

        public CreateResetarSenhaCommand(string userName)
        {
            UserName = userName;
        }
    }
}
