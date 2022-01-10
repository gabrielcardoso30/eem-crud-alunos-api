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

    public class UpdateResponsavelCommand : IRequest<Result<ResponsavelResponse>>
    {

        public Guid Id { get; set; }
        public UpdateResponsavelRequest Request { get; set; }

        public UpdateResponsavelCommand(Guid id, UpdateResponsavelRequest request)
        {
            Id = id;
            Request = request;
        }

    }

}
