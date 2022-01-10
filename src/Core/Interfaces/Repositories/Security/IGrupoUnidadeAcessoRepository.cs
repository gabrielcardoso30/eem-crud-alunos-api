using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{

    public interface IGrupoUnidadeAcessoRepository : IBaseRepository<GrupoUnidadeAcesso>
    {

        Task<IList<GrupoUnidadeAcesso>> Get(IEnumerable<Guid> ids);

        Task<IList<GrupoUnidadeAcesso>> GetByGruposId(IEnumerable<Guid> ids);

        Task<IList<GrupoUnidadeAcesso>> GetByGrupoId(Guid id);

    }

}
