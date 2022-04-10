using OssPerformanceInstitute.AcademyContext.Domain.Entities;
using OssPerformanceInstitute.AcademyContext.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories;

namespace OssPerformanceInstitute.AcademyContext.Api.Infrastructure
{
    public class TrainerRepository : EfRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(AcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
