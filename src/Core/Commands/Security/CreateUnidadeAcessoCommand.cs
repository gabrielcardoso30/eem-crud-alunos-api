using Core.Helpers;
using Core.Models.Responses.Security;
using Core.Models.Request.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Security
{

    public class CreateUnidadeAcessoCommand : IRequest<Result<UnidadeAcessoResponse>>
    {

        public CreateUnidadeAcessoRequest Request;

        public CreateUnidadeAcessoCommand(CreateUnidadeAcessoRequest request)
        {
            Request = request;
        }

    }

}
