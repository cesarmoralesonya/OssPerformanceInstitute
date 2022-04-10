using OssPerformanceInstitute.FighterContext.Domain.Entities;
using OssPerformanceInstitute.FighterContext.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories;

namespace OssPerformanceInstitute.FighterContext.Api.Infrastructure
{
    public class FighterRepository : EfRepository<Fighter>, IFighterRepository
    {
        public FighterRepository(FighterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
