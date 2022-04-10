
namespace OssPerformanceInstitute.FighterContext.Api.Commands
{
    public record CreateFighterCommand (string Name, string Country, string City, int Sex, DateTime DateOfBirth);
}
