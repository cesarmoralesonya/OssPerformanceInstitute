using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Events
{
    public record PatientDischarged(Guid Id) : IDomainEvent { }
}
