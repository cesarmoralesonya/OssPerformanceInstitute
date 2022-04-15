using OssPerformanceInstitute.HospitalBoundary.Domain.Entities;
using OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Repositories
{
    public interface IPatientAggregateStore
    {
        Task SaveAsync(Patient patient);
        Task<Patient> LoadAsync(PatientId patient);
    }
}
