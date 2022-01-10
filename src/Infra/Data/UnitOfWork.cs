using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Core.Interfaces;

namespace Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void CreateExecutionStrategy()
        {
            _appDbContext.Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction OpenTransaction()
        {
            return _appDbContext.Database.BeginTransaction();
        }
        
        public void RollbackTransaction()
        {
            _appDbContext.Database.RollbackTransaction();
        }
        
        public void CommitTransaction()
        {
            _appDbContext.Database.CommitTransaction();
        }
        
        public async Task<bool> Commit()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}