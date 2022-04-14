using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Events
{
    public record PatientAdmitted(Guid Id) : IDomainEvent { }
}
