using DomainServices.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DomainPractice.Utilities
{
    /// <summary>
    /// It implements IUnitOfWork interface using DbContext from the entity framework world.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class EfUnitOfWork<T> : IUnitOfWork
        where T : DbContext
    {
        private readonly T _dbContext;

        public EfUnitOfWork(T dbContext)
        {
            _dbContext = dbContext;
        }

        async Task IUnitOfWork.BeginTransaction(IsolationLevel isolationLevel, CancellationToken ct)
        {
            await _dbContext.Database.BeginTransactionAsync(isolationLevel, ct);
        }

        async Task IUnitOfWork.CommitTransaction(CancellationToken ct)
        {
            await _dbContext.Database.CommitTransactionAsync(ct);

            if (_dbContext.Database.CurrentTransaction is not null)
            {
                await _dbContext.Database.CurrentTransaction.DisposeAsync();
            }
        }

        async Task IUnitOfWork.RollbackTransaction(CancellationToken ct)
        {
            await _dbContext.Database.RollbackTransactionAsync(ct);

            if (_dbContext.Database.CurrentTransaction is not null)
            {
                await _dbContext.Database.CurrentTransaction.DisposeAsync();
            }
        }

        async Task IUnitOfWork.SaveChanges(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
