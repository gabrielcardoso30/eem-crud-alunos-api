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

    public class DeleteUnidadeAcessoCommand : IRequest<Result>
    {

        public DeleteUnidadeAcessoRequest Request { get; set; }

        public DeleteUnidadeAcessoCommand(DeleteUnidadeAcessoRequest request)
        {
            Request = request;
        }

    }

}
