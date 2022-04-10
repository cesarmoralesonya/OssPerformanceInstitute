using Ardalis.Specification;

namespace OssPerformanceInstitute.SharedKernel.Domain.Repositories
{
    public interface IAsyncRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
    }
}
