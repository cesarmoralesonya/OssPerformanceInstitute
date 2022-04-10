using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OssPerformanceInstitute.SharedKernel.Domain.Repositories;

namespace OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories
{
    public class EfRepository<TEntity> : RepositoryBase<TEntity>,
                                    IAsyncReadRepository<TEntity>,
                                    IAsyncRepository<TEntity> 
                                    where TEntity : class
    {
        public EfRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
