using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Core.Helpers;
using Core.Interfaces;
using EFCore.BulkExtensions;
using System.Linq.Expressions;
using Core.Interfaces.Security;
using Core.Security;

namespace Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;

		public BaseRepository(
			AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Guid> GetSelectedAccessUnitIdAsync()
        {

            SecurityHelper securityHelper = new SecurityHelper();
            Guid usuarioAutenticadoId = securityHelper.GuidLogin();
            var user = await _dbContext.AspNetUsers.AsQueryable().Where(gc => gc.Id == usuarioAutenticadoId).FirstOrDefaultAsync();
            return user.UnidadeAcessoSelecionada ?? Guid.Empty;

        }

        public Guid GetSelectedAccessUnitId()
        {

            SecurityHelper securityHelper = new SecurityHelper();
            Guid usuarioAutenticadoId = securityHelper.GuidLogin();
            var user = _dbContext.AspNetUsers.AsQueryable().Where(gc => gc.Id == usuarioAutenticadoId).FirstOrDefault();
            return user.UnidadeAcessoSelecionada ?? Guid.Empty;

        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<AsyncOutResult<IEnumerable<TEntity>,int>> GetAll(int? take, int? offSet, string sortingProp, bool? asc)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(sortingProp) && asc != null)
                if (DataHelpers.CheckExistingProperty<TEntity>(sortingProp))
                    query = query.OrderByDynamic(sortingProp,(bool)asc);
            
            if (take != null && offSet != null)
                return new AsyncOutResult<IEnumerable<TEntity>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), await query.CountAsync());
            
            return new AsyncOutResult<IEnumerable<TEntity>, int>(await query.ToListAsync(), await query.CountAsync());
        }

        public virtual async Task<AsyncOutResult<IEnumerable<TEntity>, int>> GetAllByPredicate(Expression<Func<TEntity, bool>> predicate, int? take, int? offSet, string sortingProp, bool? asc)
        {

            var query = _dbContext.Set<TEntity>().AsQueryable();
            query = query.Where(predicate);

            if (!string.IsNullOrEmpty(sortingProp) && asc != null)
                if (DataHelpers.CheckExistingProperty<TEntity>(sortingProp))
                    query = query.OrderByDynamic(sortingProp, (bool)asc);

            if (take != null && offSet != null)
                return new AsyncOutResult<IEnumerable<TEntity>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), await query.CountAsync());

            return new AsyncOutResult<IEnumerable<TEntity>, int>(await query.ToListAsync(), await query.CountAsync());

        }
        
        public virtual async Task<IEnumerable<TEntity>> Get(int? take, int? offSet, string sortingProp, bool? asc)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            
            if (!string.IsNullOrEmpty(sortingProp) && asc != null)
                if (DataHelpers.CheckExistingProperty<TEntity>(sortingProp))
                    query = query.OrderByDynamic(sortingProp,(bool)asc);
            
            if (take != null && offSet != null)
                return await query.Skip((int)offSet).Take((int)take).ToListAsync();
            
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetByPredicate(Expression<Func<TEntity, bool>> predicate, int? take, int? offSet, string sortingProp, bool? asc)
        {

            var query = _dbContext.Set<TEntity>().AsQueryable();
            query = query.Where(predicate);

            if (!string.IsNullOrEmpty(sortingProp) && asc != null)
                if (DataHelpers.CheckExistingProperty<TEntity>(sortingProp))
                    query = query.OrderByDynamic(sortingProp, (bool)asc);

            if (take != null && offSet != null)
                return await query.Skip((int)offSet).Take((int)take).ToListAsync();

            return await query.ToListAsync();
            
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        
        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity) 
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entity);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> listEntity) 
        {
            try
            {
                _dbContext.Set<TEntity>().RemoveRange(listEntity);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> SoftDeleteAsync(TEntity entity)
        {
            try
            {
                bool setPropertyResult = GenericEntityHelpers.TrySetProperty(entity, "Deletado", true);

                if (!setPropertyResult) return false;

                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> SoftDeleteRangeAsync(IEnumerable<TEntity> listEntity)
        {
            try
            {
                foreach (var entity in listEntity)
                {
                    bool setPropertyResult = GenericEntityHelpers.TrySetProperty(entity, "Deletado", true);
                    if (!setPropertyResult) return false;
                }
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbContext.Update(entity);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public virtual async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> listEntity)
        {
            try
            {
                _dbContext.UpdateRange(listEntity);
                
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> BulkInsert(IEnumerable<TEntity> listEntity)
        {
            try
            {
                await _dbContext.BulkInsertAsync(listEntity.ToList());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> BulkUpdate(IEnumerable<TEntity> listEntity)
        {
            try
            {
                await _dbContext.BulkUpdateAsync(listEntity.ToList());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Truncate(string table)
        {
            try
            {
                string sqlQuery = $"TRUNCATE TABLE {table}";
                await _dbContext.Database.ExecuteSqlRawAsync(sqlQuery);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> CountTable()
        {

            var query = await _dbContext.Set<TEntity>().CountAsync();
            return query;

        }

    }
}
