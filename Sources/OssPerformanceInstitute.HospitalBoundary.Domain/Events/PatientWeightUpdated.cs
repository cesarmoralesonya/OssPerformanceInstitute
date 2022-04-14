using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Events
{
    public record PatientWeightUpdated(Guid Id, decimal Value) : IDomainEvent { }
}
