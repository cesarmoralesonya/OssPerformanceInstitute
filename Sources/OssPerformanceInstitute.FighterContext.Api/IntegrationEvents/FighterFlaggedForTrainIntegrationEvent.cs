using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.FighterContext.Api.IntegrationEvents
{
    public record FighterFlaggedForTrainIntegrationEvent(Guid Id, string Name, string Country, string City, int Sex, DateTime DateOfBirth) : IIntegrationEvent
    {
    }
}
