using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Enums;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{
    public interface IAuditoriaRepository : IBaseRepository<Auditoria>
    {
        Task<AsyncOutResult<IEnumerable<Auditoria>,int>> Get(DateTime? dataEventoInicio ,DateTime? dataEventoFim , 
            EnumEntidade? entidade,  Guid usuarioId , EnumAcao? acao, string entidadeId, 
            int take, int offset, string sortingProp, bool asc);
    }
}