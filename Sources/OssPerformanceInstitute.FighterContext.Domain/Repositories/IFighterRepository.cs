using OssPerformanceInstitute.FighterContext.Domain.Entities;
using OssPerformanceInstitute.SharedKernel.Domain.Repositories;

namespace OssPerformanceInstitute.FighterContext.Domain.Repositories
{
    public interface IFighterRepository : IAsyncRepository<Fighter>
    {
    }
}
