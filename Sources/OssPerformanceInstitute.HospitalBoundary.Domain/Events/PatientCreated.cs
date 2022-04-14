using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Events
{
    public record PatientCreated(Guid Id) : IDomainEvent { }
}
