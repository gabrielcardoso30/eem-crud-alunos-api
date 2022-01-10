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

    public class GrupoRepository : BaseRepository<Grupo>, IGrupoRepository
    {

        private readonly AppDbContext _dbContext;

        public GrupoRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Deactivated(Grupo entity)
        {
            entity.Deletado = true;
            _dbContext.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<AsyncOutResult<IEnumerable<Grupo>, int>> Get(int take, int offset, string sortingProp, bool asc)
        {
            var query = _dbContext.Grupo
                .Include(x => x.PermissaoGrupo)
                .ThenInclude(x => x.Permissao)
                .Where(p => p.Deletado == false).AsQueryable();

            if (DataHelpers.CheckExistingProperty<Grupo>(sortingProp))
                query = query.OrderByDynamic(sortingProp, asc);

            return new AsyncOutResult<IEnumerable<Grupo>, int>(await query.Skip(offset).Take(take).ToListAsync(), await query.CountAsync());
        }

        public async Task<IList<Grupo>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.Grupo
                .Include(x => x.PermissaoGrupo)
                .ThenInclude(x => x.Permissao)
                .Where(p => p.Deletado == false).AsQueryable();

            if (ids != null)
                query = query.Where(gc => ids.Contains(gc.Id));

            return await query.ToListAsync();

        }

        public async Task<IEnumerable<Grupo>> GetGrupoPadrao()
        {
            var query = _dbContext.Grupo
                .Where(p => p.Deletado == false && p.Padrao == true).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<Grupo> GetByName(string nome)
        {
            return await _dbContext.Grupo
                .FirstOrDefaultAsync(p => p.Deletado == false && p.Nome.ToLower() == nome.ToLower());
        }

        public override async Task<Grupo> GetById(Guid id)
        {
            return await _dbContext.Grupo.FirstOrDefaultAsync(p => p.Id == id && p.Deletado == false);
        }

        public async Task<bool> ExistsGrupoMesmoNome(Guid id, string nome, Guid moduloId)
        {
            return await _dbContext.Grupo.AnyAsync(x =>
                x.Id != id &&
                x.Nome.ToLower() == nome.ToLower() &&
                x.Deletado == false);
        }

        public async Task<IList<Grupo>> Search(string text, int? take, int? offSet)
        {

            var query = _dbContext.Grupo.AsQueryable();
            query = query.Where(gc => gc.Nome.Contains(text));

            if (take != null && offSet != null) return await query.Skip((int)offSet).Take((int)take).ToListAsync();
            else return await query.ToListAsync();

        }

        public async Task<AsyncOutResult<IEnumerable<Grupo>, int>> SearchAll(string text, int? take, int? offSet)
        {

            var query = _dbContext.Grupo.AsQueryable();
            query = query.Where(gc => gc.Nome.Contains(text));
            int totalCount = await query.CountAsync();

            if (take != null && offSet != null)
                return new AsyncOutResult<IEnumerable<Grupo>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), totalCount);
            else
                return new AsyncOutResult<IEnumerable<Grupo>, int>(await query.ToListAsync(), totalCount);

        }

    }

}