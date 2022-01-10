using Core.Entities.Gerencial;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Gerencial
{

    public interface IResponsavelRepository : IBaseRepository<Responsavel>
    {

        Task<IList<Responsavel>> Get(IEnumerable<Guid> ids);

        Task<IList<Responsavel>> GetByAlunoId(Guid id);

        Task<IList<Responsavel>> GetByAlunosId(IEnumerable<Guid> ids);

        Task<IList<Responsavel>> Search(string text, int? take, int? offSet);

        Task<AsyncOutResult<IEnumerable<Responsavel>, int>> SearchAll(string text, int? take, int? offSet, string tableFilter);

    }

}
