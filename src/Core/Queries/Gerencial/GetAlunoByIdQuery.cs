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

    public class GetAlunoByIdQuery : IRequest<Result<AlunoResponse>>
    {

        public Guid Id { get; set; }

        public GetAlunoByIdQuery(Guid id)
        {
            Id = id;
        }

    }

}
