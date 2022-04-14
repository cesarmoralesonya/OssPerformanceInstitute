using OssPerformanceInstitute.AcademyBoundary.Domain.ValueObjects.FighterClientEntity;
using OssPerformanceInstitute.AcademyBoundary.Domain.ValueObjects.TrainerEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OssPerformanceInstitute.AcademyBoundary.Domain.Entities
{
    public class FighterClient
    {
        public Guid Id { get; init; }
        public TrainerId TrainerId { get; private set; }
        public FighterClientTrainingStatus TrainingStatus { get; private set; }

#nullable disable
        public FighterClient(FighterClientId id)
        {
            Id = id;
        }

        protected FighterClient()
        {
        }
#nullable disable

        public void SetTrainingStatus(FighterClientTrainingStatus trainingStatus)
        {
            TrainingStatus = trainingStatus;
        }

        public void RequestToTrain(TrainerId trainerId)
        {
            TrainerId = trainerId;
            TrainingStatus = FighterClientTrainingStatus.Pending;
        }
    }
}
