using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;

namespace Infra.Data.Repositories.Security
{
    public class UsuarioRepository : BaseRepository<AspNetUsers>, IUsuarioRepository
    {

        private readonly AppDbContext _dbContext;

        public UsuarioRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Deactivated(AspNetUsers entity, bool toogle)
        {
            entity.Deletado = toogle;
            _dbContext.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<AsyncOutResult<IEnumerable<AspNetUsers>, int>> Get(Guid? grupoId, string nome, string login, string cpf, int take, int offset, string sortingProp, bool asc)
        {
            var query = _dbContext.AspNetUsers
                .Include(i => i.GrupoAspNetUsers).ThenInclude(i => i.Grupo)
                .AsQueryable();

            if (grupoId != null && grupoId != Guid.Empty)
                query = query.Where(p => p.GrupoAspNetUsers.Any(a => a.GrupoId == grupoId));
            if (!string.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.Contains(nome));
            if (!string.IsNullOrEmpty(login))
                query = query.Where(p => p.UserName == login);
            if (!string.IsNullOrEmpty(cpf))
                query = query.Where(p => p.UserName == cpf);

            if (DataHelpers.CheckExistingProperty<AspNetUsers>(sortingProp))
                query = query.OrderByDynamic(sortingProp, asc);

            return new AsyncOutResult<IEnumerable<AspNetUsers>, int>(await query.Skip(offset).Take(take).ToListAsync(), await query.CountAsync());

        }

        public async Task<IList<AspNetUsers>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.AspNetUsers
                .Include(i => i.GrupoAspNetUsers).ThenInclude(i => i.Grupo)
                .AsQueryable();

            if (ids != null)
                query = query.Where(gc => ids.Contains(gc.Id));

            return await query.ToListAsync();

        }

        public override async Task<AspNetUsers> GetById(Guid id)
        {
            return await _dbContext.AspNetUsers
                .Include(i => i.GrupoAspNetUsers)
                .ThenInclude(x => x.Grupo)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<AspNetUsers>> UsuariosBloqueados(DateTime data)
        {
            return await _dbContext.AspNetUsers
                .Where(w => w.DataBloqueioPrimeiroAcesso < data ||
                            w.QuantidadeLogin >= 5 ||
                            w.QuantidadePrimeiroAcesso >= 5).ToListAsync();
        }

        public async Task<AspNetUsers> GetByIdWithPermission(Guid id)
        {
            return await _dbContext.AspNetUsers
                .Include(i => i.GrupoAspNetUsers).ThenInclude(i => i.Grupo).ThenInclude(i => i.PermissaoGrupo).ThenInclude(i => i.Permissao)
                .Where(w => w.Deletado == false)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<AspNetUsers> GetByLogin(string userName)
        {
            return await _dbContext.AspNetUsers.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Deletado == false && w.UserName == userName);
        }

        public async Task<AspNetUsersRefreshToken> GetRefreshTokenByAspNetUsersId(Guid aspNetUsersId)
        {
            return await _dbContext.AspNetUsersRefreshToken
                .FirstOrDefaultAsync(p => p.AspNetUsersId == aspNetUsersId);
        }

        public async Task<AspNetUsersRefreshToken> AddRefreshToken(AspNetUsersRefreshToken entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<AspNetUsersRefreshToken> UpdateRefreshToken(AspNetUsersRefreshToken entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteRefreshToken(Guid? AspNetUsersId)
        {
            var token = _dbContext.AspNetUsersRefreshToken.FirstOrDefault(f => f.AspNetUsersId == AspNetUsersId);

            if (token != null)
            {
                _dbContext.AspNetUsersRefreshToken.Remove(token);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> ExistsEmail(Guid id, string email)
        {
            return await _dbContext.AspNetUsers.AnyAsync(x => x.Id != id && x.Email == email && !x.Deletado);
        }

        public async Task<bool> ExistsEmail(string matriculaSap, string email)
        {
            return await _dbContext.AspNetUsers.AnyAsync(x => x.UserName != matriculaSap && x.Email == email && !x.Deletado);
        }

        public async Task<bool> ExistsEmail(string email)
        {
            return await _dbContext.AspNetUsers.AnyAsync(x => x.Email == email && !x.Deletado);
        }

        public async Task<bool> UpdateAllTermo(bool flag)
        {
            var dados = await _dbContext.Database.ExecuteSqlRawAsync("UPDATE AspNetUsers Set TermoUso = 0");

            return dados > 0;
        }

        public async Task<IList<AspNetUsers>> Search(string text, int? take, int? offSet)
        {

            var query = _dbContext.AspNetUsers.AsQueryable();
            query = query.Where(gc =>
            gc.Nome.Contains(text)
            || gc.CPF.Contains(text)
            || gc.Email.Contains(text)
            || gc.TelefoneResidencial.Contains(text)
            );

            if (take != null && offSet != null) return await query.Skip((int)offSet).Take((int)take).ToListAsync();
            else return await query.ToListAsync();

        }

        public async Task<AsyncOutResult<IEnumerable<AspNetUsers>, int>> SearchAll(string text, int? take, int? offSet)
        {

            var query = _dbContext.AspNetUsers.AsQueryable();
            query = query.Where(gc =>
            gc.Nome.Contains(text)
            || gc.CPF.Contains(text)
            || gc.Email.Contains(text)
            || gc.TelefoneResidencial.Contains(text)
            );
            int totalCount = await query.CountAsync();

            if (take != null && offSet != null)
                return new AsyncOutResult<IEnumerable<AspNetUsers>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), totalCount);
            else
                return new AsyncOutResult<IEnumerable<AspNetUsers>, int>(await query.ToListAsync(), totalCount);

        }

    }

}
