using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.FighterContext.Domain.Events
{
    public record FighterFlaggedForFight(Guid Id, string Name, string Country, string City, int Sex, DateTime DateOfBirth) : IDomainEvent { }
}
