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

    public class DeleteAlunoCommand : IRequest<Result>
    {

        public DeleteAlunoRequest Request { get; set; }

        public DeleteAlunoCommand(DeleteAlunoRequest request)
        {
            Request = request;
        }

    }

}
