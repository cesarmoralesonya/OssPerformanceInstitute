using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Events
{
    public record PatientBloodTypeUpdated(Guid Id, string BloodType) : IDomainEvent { }
}
