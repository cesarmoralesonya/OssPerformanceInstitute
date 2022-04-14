using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.AcademyBoundary.Domain.Events
{
    public static class DomainEvents
    {
        public static readonly DomainEvent<TrainingRequestCreated> TrainingRequestCreated = new();
    }
}
