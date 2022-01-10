using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Enums;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{
    public interface IParametroSistemaRepository : IBaseRepository<ParametroSistema>
    {
        Task<AsyncOutResult<IEnumerable<ParametroSistema>,int>> Get(Guid usuarioId, EnumTipoParametro? tipoParametro, int take, int offset, string sortingProp, bool asc);
        Task<ParametroSistema> GetByIdWithUserId(Guid id, Guid usuarioId);
    }
}
