namespace OssPerformanceInstitute.AcademyContext.Api.Commands
{
    public record CreateTrainerCommand(string Name, Questionnaire Questionnaire);
    public record Questionnaire(bool IsMuayThaiTrainer, bool IsBjjTrainner, bool IsBoxingTrainner, bool IsKickBoxingTrainner, bool IsMmaTrainner);
}
