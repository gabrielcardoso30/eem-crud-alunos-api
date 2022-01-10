using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void CreateExecutionStrategy();
        IDbContextTransaction OpenTransaction();
        void RollbackTransaction();
        void CommitTransaction();
        Task<bool> Commit();
    }
}