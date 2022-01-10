using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{

    public interface IGrupoRepository : IBaseRepository<Grupo>
    {

        Task<AsyncOutResult<IEnumerable<Grupo>,int>> Get(int take, int offset, string sortingPro, bool asc);
        Task<IList<Grupo>> Get(IEnumerable<Guid> ids);
        Task<Grupo> GetByName(string nome);
        Task<bool> Deactivated(Grupo entity);
        Task<IEnumerable<Grupo>> GetGrupoPadrao();
        Task<IList<Grupo>> Search(string text, int? take, int? offSet);
        Task<AsyncOutResult<IEnumerable<Grupo>, int>> SearchAll(string text, int? take, int? offSet);

    }

}
