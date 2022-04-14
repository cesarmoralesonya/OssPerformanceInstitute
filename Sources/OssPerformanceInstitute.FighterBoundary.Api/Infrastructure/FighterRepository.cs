using OssPerformanceInstitute.FighterBoundary.Domain.Entities;
using OssPerformanceInstitute.FighterBoundary.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories;

namespace OssPerformanceInstitute.FighterBoundary.Api.Infrastructure
{
    public class FighterRepository : EfRepository<Fighter>, IFighterRepository
    {
        public FighterRepository(FighterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
