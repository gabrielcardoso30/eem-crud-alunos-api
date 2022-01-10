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

    public class UpdateUnidadeAcessoCommand : IRequest<Result<UnidadeAcessoResponse>>
    {

        public Guid Id { get; set; }
        public UpdateUnidadeAcessoRequest Request { get; set; }

        public UpdateUnidadeAcessoCommand(Guid id, UpdateUnidadeAcessoRequest request)
        {
            Id = id;
            Request = request;
        }

    }

}
