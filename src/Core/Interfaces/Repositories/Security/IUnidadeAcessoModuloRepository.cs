using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{

    public interface IUnidadeAcessoModuloRepository : IBaseRepository<UnidadeAcessoModulo>
    {

        Task<IList<UnidadeAcessoModulo>> Get(IEnumerable<Guid> ids);

        Task<IList<UnidadeAcessoModulo>> GetByUnidadesAcessoId(IEnumerable<Guid> ids);

        Task<IList<UnidadeAcessoModulo>> GetByUnidadeAcessoId(Guid id);

    }

}
