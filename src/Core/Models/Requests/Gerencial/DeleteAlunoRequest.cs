using System;
using System.Collections.Generic;

namespace Core.Models.Requests.Gerencial
{

    public class DeleteAlunoRequest
    {

        public ICollection<Guid> Ids { get; set; }

    }

}
