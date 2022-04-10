using OssPerformanceInstitute.AcademyContext.Domain.Entities;
using OssPerformanceInstitute.AcademyContext.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories;

namespace OssPerformanceInstitute.AcademyContext.Api.Infrastructure
{
    public class FighterClientRepository : EfRepository<FighterClient>, IFighterClientRepository
    {
        public FighterClientRepository(AcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
