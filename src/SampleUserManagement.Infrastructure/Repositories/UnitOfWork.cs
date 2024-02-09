using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleUserManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(IContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }
    }
}
