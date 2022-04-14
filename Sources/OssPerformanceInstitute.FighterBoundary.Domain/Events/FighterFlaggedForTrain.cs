using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.FighterBoundary.Domain.Events
{
    public record FighterFlaggedForTrain(Guid Id, string Name, string Country, string City, int Sex, DateTime DateOfBirth) : IDomainEvent { }
}
