using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SampleUserManagement.Base.EntityFramework
{
    public interface IBaseContext : IDisposable, IAsyncDisposable
    {
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DatabaseFacade Database { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
