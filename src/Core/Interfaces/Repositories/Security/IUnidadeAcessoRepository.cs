using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{

    public interface IUnidadeAcessoRepository : IBaseRepository<UnidadeAcesso>
    {

        Task<IList<UnidadeAcesso>> Get(IEnumerable<Guid> ids);

        Task<UnidadeAcesso> GetUnidadeAcessoParaTestes();
        
        Task<IList<UnidadeAcesso>> ListByStatus(bool ativo);

        Task<IList<UnidadeAcesso>> Search(string text, int? take, int? offSet);

        Task<AsyncOutResult<IEnumerable<UnidadeAcesso>, int>> SearchAll(string text, int? take, int? offSet);

    }

}
