using Core.Helpers;
using Core.Models.Responses.Gerencial;
using Core.Models.Requests.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Gerencial
{

    public class CreateAlunoCommand : IRequest<Result<AlunoResponse>>
    {

        public CreateAlunoRequest Request;

        public CreateAlunoCommand(CreateAlunoRequest request)
        {
            Request = request;
        }

    }

}
