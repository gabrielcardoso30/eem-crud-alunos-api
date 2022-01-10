using System;

namespace Core.Models.Responses.Security
{
    public class GrupoAspNetUsersResponse
    {
        public Guid Id { get; set; }
        public GrupoResponse Grupo { get; set; }
    }
}
