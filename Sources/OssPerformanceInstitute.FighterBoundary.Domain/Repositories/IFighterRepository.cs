using OssPerformanceInstitute.FighterBoundary.Domain.Entities;
using OssPerformanceInstitute.SharedKernel.Domain.Repositories;

namespace OssPerformanceInstitute.FighterBoundary.Domain.Repositories
{
    public interface IFighterRepository : IAsyncRepository<Fighter>
    {
    }
}
