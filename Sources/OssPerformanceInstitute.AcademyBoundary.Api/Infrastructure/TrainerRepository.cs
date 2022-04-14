using OssPerformanceInstitute.AcademyBoundary.Domain.Entities;
using OssPerformanceInstitute.AcademyBoundary.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories;

namespace OssPerformanceInstitute.AcademyBoundary.Api.Infrastructure
{
    public class TrainerRepository : EfRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(AcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
