using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{

    public class CreateLoginCommand : IRequest<Result<LoginResponse>>
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string GrantTypes { get; set; } = "password";

        public CreateLoginCommand(
            string userName, 
            string password, 
            string grantTypes
        )
        {
            UserName = userName;
            Password = password;
            GrantTypes = grantTypes;
        }

        public CreateLoginCommand()
        {

        }

    }

}
