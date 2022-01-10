using System;
using System.Collections.Generic;

namespace Core.Models.Requests.Gerencial
{

    public class DeleteResponsavelRequest
    {

        public ICollection<Guid> Ids { get; set; }

    }

}
