using SampleUserManagement.Base.Entities;

namespace SampleUserManagement.Base.EntityFramework
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}
