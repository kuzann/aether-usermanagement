using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SampleUserManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Interfaces
{
    public interface IContext : IDisposable, IAsyncDisposable
    {
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DatabaseFacade Database { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        public DbSet<User> Users { get; }
        public DbSet<Role> Roles { get; }
    }
}
