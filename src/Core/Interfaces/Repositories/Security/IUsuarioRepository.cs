using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;

namespace Core.Interfaces.Repositories.Security
{
    public interface IUsuarioRepository : IBaseRepository<AspNetUsers>
    {
        Task<AspNetUsers> GetByIdWithPermission(Guid id);
        Task<AsyncOutResult<IEnumerable<AspNetUsers>,int>> Get(Guid? grupoId, string nome, string login, string cpf, int take, int offset, string sortingProp, bool asc);
        Task<IList<AspNetUsers>> Get(IEnumerable<Guid> ids);
        Task<AspNetUsers> GetByLogin(string userName);
        Task<bool> Deactivated(AspNetUsers entity, bool toogle);
        Task<AspNetUsersRefreshToken> GetRefreshTokenByAspNetUsersId(Guid aspNetUsersId);
        Task<AspNetUsersRefreshToken> AddRefreshToken(AspNetUsersRefreshToken entity);
        Task<AspNetUsersRefreshToken> UpdateRefreshToken(AspNetUsersRefreshToken entity);
        Task<bool> ExistsEmail(Guid id, string email);
        Task<bool> ExistsEmail(string matriculaSap, string email);
        Task<bool> ExistsEmail(string email);
        Task<bool> UpdateAllTermo(bool flag);
        Task<IEnumerable<AspNetUsers>> UsuariosBloqueados(DateTime data);
        Task<bool> DeleteRefreshToken(Guid? AspNetUsersId);
        Task<IList<AspNetUsers>> Search(string text, int? take, int? offSet);
        Task<AsyncOutResult<IEnumerable<AspNetUsers>, int>> SearchAll(string text, int? take, int? offSet);
    }
}
