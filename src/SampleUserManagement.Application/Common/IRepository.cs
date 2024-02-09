using SampleUserManagement.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> Get(Guid id, CancellationToken cancellationToken);
        IQueryable<TEntity> GetAll();
    }
}
