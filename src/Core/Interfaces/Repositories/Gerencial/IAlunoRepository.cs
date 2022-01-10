using Core.Entities.Gerencial;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Gerencial
{

    public interface IAlunoRepository : IBaseRepository<Aluno>
    {

        Task<IList<Aluno>> Get(IEnumerable<Guid> ids);

        Task<IList<Aluno>> Search(string text, int? take, int? offSet);

        Task<AsyncOutResult<IEnumerable<Aluno>, int>> SearchAll(string text, int? take, int? offSet, string tableFilter);

    }

}
