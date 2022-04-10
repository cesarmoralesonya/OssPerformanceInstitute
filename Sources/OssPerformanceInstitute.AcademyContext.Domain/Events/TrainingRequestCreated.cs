using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.AcademyContext.Domain.Events
{
    public record TrainingRequestCreated(Guid FighterClientId, Guid TrainerId) : IDomainEvent { }
}
