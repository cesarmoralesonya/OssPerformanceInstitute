using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Events
{
    public record PatientProcedureAdded(Guid PatientId, Guid Id, string ProcedureName) : IDomainEvent { }
}
