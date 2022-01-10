using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Helpers;

namespace Core.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        Task<Guid> GetSelectedAccessUnitIdAsync();
        Guid GetSelectedAccessUnitId();
        Task<TEntity> GetById(Guid id);
        Task<AsyncOutResult<IEnumerable<TEntity>,int>> GetAll(int? take, int? offSet, string sortingProp, bool? asc);
        Task<AsyncOutResult<IEnumerable<TEntity>, int>> GetAllByPredicate(Expression<Func<TEntity, bool>> predicate, int? take, int? offSet, string sortingProp, bool? asc);
        Task<IEnumerable<TEntity>> Get(int? take, int? offSet, string sortingProp, bool? asc);
        Task<IEnumerable<TEntity>> GetByPredicate(Expression<Func<TEntity, bool>> predicate, int? take, int? offSet, string sortingProp, bool? asc);
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> listEntity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> listEntity);
        Task<bool> SoftDeleteAsync(TEntity entity);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<TEntity> listEntity);
        Task<bool> BulkInsert(IEnumerable<TEntity> listEntity);
        Task<bool> BulkUpdate(IEnumerable<TEntity> listEntity);
        Task<bool> Truncate(string table);
        Task<int> CountTable();

    }
}
