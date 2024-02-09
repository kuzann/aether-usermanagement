using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IContext _context;
        private readonly DbSet<TEntity> _dbSet;

        private const string DEFAULT_USER = "SYSTEM";

        public Repository(IContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            entity.CreatedBy = DEFAULT_USER;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedBy = DEFAULT_USER;
            entity.ModifiedAt = DateTimeOffset.UtcNow;
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.DeletedBy = DEFAULT_USER;
            entity.DeletedAt = DateTimeOffset.UtcNow;
            _dbSet.Update(entity);
        }

        public async Task<TEntity?> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.DeletedAt == null, cancellationToken);
        }

        // TO DO : change approach to not use ToList directly
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.Where(e => e.DeletedAt == null);
        }

    }
}
