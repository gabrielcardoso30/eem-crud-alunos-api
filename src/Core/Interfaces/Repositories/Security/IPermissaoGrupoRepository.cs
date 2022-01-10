using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;

namespace Core.Interfaces.Repositories.Security
{
    public interface IPermissaoGrupoRepository : IBaseRepository<PermissaoGrupo>
    {
        Task<IEnumerable<PermissaoGrupo>> Get(Guid grupoId);
        Task<IList<PermissaoGrupo>> Get(IEnumerable<Guid> ids);
    }
}
