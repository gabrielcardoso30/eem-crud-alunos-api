using System;
using System.Collections.Generic;

namespace Core.Models.Request.Security
{

    public class DeleteUnidadeAcessoRequest
    {

        public ICollection<Guid> Ids { get; set; }

    }

}
