using Core.Helpers;
using Core.Models.Filters;
using Core.Models.Responses.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries.Gerencial
{

    public class GetResponsavelByIdQuery : IRequest<Result<ResponsavelResponse>>
    {

        public Guid Id { get; set; }

        public GetResponsavelByIdQuery(Guid id)
        {
            Id = id;
        }

    }

}
