using SampleUserManagement.Domain.Entities.Common;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}
