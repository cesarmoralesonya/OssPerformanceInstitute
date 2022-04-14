using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.AcademyBoundary.Domain.Events
{
    public record TrainingRequestCreated(Guid FighterClientId, Guid TrainerId) : IDomainEvent { }
}
