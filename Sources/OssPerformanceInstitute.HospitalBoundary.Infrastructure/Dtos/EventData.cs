namespace OssPerformanceInstitute.HospitalBoundary.Infrastructure.Dtos
{
    public record EventData(Guid Id, string AggregateId, string EventName, string Data, string AssemblyQualifiedName);
}