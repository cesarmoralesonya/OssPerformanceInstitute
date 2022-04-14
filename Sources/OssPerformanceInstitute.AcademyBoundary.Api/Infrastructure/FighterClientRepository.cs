using OssPerformanceInstitute.AcademyBoundary.Domain.Entities;
using OssPerformanceInstitute.AcademyBoundary.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories;

namespace OssPerformanceInstitute.AcademyBoundary.Api.Infrastructure
{
    public class FighterClientRepository : EfRepository<FighterClient>, IFighterClientRepository
    {
        public FighterClientRepository(AcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
