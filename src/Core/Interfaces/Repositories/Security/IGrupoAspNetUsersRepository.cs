using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;

namespace Core.Interfaces.Repositories.Security
{
    public interface IGrupoAspNetUsersRepository : IBaseRepository<GrupoAspNetUsers>
    {
        Task<IEnumerable<GrupoAspNetUsers>> GetGruposUsuario(Guid aspNetUsersId);
        Task<bool> DeleteByGrupoId(Guid id);
    }
}
