using Ardalis.Specification;

namespace OssPerformanceInstitute.SharedKernel.Domain.Repositories
{
    public interface IAsyncReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
    }
}
