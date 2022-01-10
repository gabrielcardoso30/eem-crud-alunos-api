using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{

    public interface IGrupoModuloRepository : IBaseRepository<GrupoModulo>
    {

        Task<IList<GrupoModulo>> Get(IEnumerable<Guid> ids);

        Task<IList<GrupoModulo>> GetByGruposId(IEnumerable<Guid> ids);

        Task<IList<GrupoModulo>> GetByGrupoId(Guid id);

    }

}
