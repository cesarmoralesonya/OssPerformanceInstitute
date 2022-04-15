namespace OssPerformanceInstitute.FighterBoundary.Api.Dtos
{
    public record FighterResponse(Guid FighterId, string Name, string Country, string City, int Sex, DateTime DateOfBirth);
}
