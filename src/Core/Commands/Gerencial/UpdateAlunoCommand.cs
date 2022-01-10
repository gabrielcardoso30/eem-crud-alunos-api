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

    public class UpdateAlunoCommand : IRequest<Result<AlunoResponse>>
    {

        public Guid Id { get; set; }
        public UpdateAlunoRequest Request { get; set; }

        public UpdateAlunoCommand(Guid id, UpdateAlunoRequest request)
        {
            Id = id;
            Request = request;
        }

    }

}
