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

    public class DeleteResponsavelCommand : IRequest<Result>
    {

        public DeleteResponsavelRequest Request { get; set; }

        public DeleteResponsavelCommand(DeleteResponsavelRequest request)
        {
            Request = request;
        }

    }

}
