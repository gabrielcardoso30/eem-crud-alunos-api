using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{
    public class CreateAlterarSenhaCommand : IRequest<Result<SenhaResponse>>
    {
        public string UserName { get; set; }
        public string SenhaAntiga { get; set; }
        public string SenhaNova { get; set; }

        public CreateAlterarSenhaCommand(string userName, string senhaAntiga, string senhaNova)
        {
            UserName = userName;
            SenhaAntiga = senhaAntiga;
            SenhaNova = senhaNova;
        }
    }
}
