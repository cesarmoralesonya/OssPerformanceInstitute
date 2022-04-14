using OssPerformanceInstitute.AcademyBoundary.Domain.Exceptions;

namespace OssPerformanceInstitute.AcademyBoundary.Domain.ValueObjects.TrainerEntity
{
    public record TrainerDisciplinesQuestionnaire
    {
        public bool IsMuayThaiTrainer { get; init; }
        public bool IsBjjTrainner { get; init; }
        public bool IsBoxingTrainner { get; init; }
        public bool IsKickBoxingTrainner { get; init; }
        public bool IsMmaTrainner { get; init; }

        internal TrainerDisciplinesQuestionnaire(bool isMuayThaiTrainer, bool isBjjTrainner, bool isBoxingTrainner, bool isKickBoxingTrainner, bool isMmaTrainner)
        {
            IsMuayThaiTrainer = isMuayThaiTrainer;
            IsBjjTrainner = isBjjTrainner;
            IsBoxingTrainner = isBoxingTrainner;
            IsKickBoxingTrainner = isKickBoxingTrainner;
            IsMmaTrainner = isMmaTrainner;
        }

        public static TrainerDisciplinesQuestionnaire Create(bool isMuayThaiTrainer, bool isBjjTrainner, bool isBoxingTrainner, bool isKickBoxingTrainner, bool isMmaTrainner)
        {
            Validate(isMuayThaiTrainer, isBjjTrainner, isBoxingTrainner, isKickBoxingTrainner, isMmaTrainner);
            return new TrainerDisciplinesQuestionnaire(isMuayThaiTrainer, isBjjTrainner, isBoxingTrainner, isKickBoxingTrainner, isMmaTrainner);
        }

        private static void Validate(bool isMuayThaiTrainer, bool isBjjTrainner, bool isBoxingTrainner, bool isKickBoxingTrainner, bool isMmaTrainner)
        {
            if(isMmaTrainner)
            {
                if (!((isMmaTrainner || isKickBoxingTrainner) && isBjjTrainner))
                    throw new CannotBeMmaTrainnerException();
            }
        }
    }
}
