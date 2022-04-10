using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.FighterContext.Api.IntegrationEvents
{
    public record FighterFlaggedForFightIntegrationEvent(Guid Id, string Name, string Country, string City, int Sex, DateTime DateOfBirth) : IIntegrationEvent
    {
    }
}
