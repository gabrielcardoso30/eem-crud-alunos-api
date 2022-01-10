using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using System;
using System.Linq;

namespace Core.Interfaces.Repositories.Security
{
    public interface IPermissaoRepository : IBaseRepository<Permissao>
    {
        Task<IEnumerable<Permissao>> Get();
        Task<IList<Permissao>> Get(IEnumerable<Guid> ids);
    }
}
