using OssPerformanceInstitute.AcademyContext.Domain.Events;
using OssPerformanceInstitute.AcademyContext.Domain.Exceptions;
using OssPerformanceInstitute.AcademyContext.Domain.ValueObjects.FighterClientEntity;
using OssPerformanceInstitute.AcademyContext.Domain.ValueObjects.TrainerEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OssPerformanceInstitute.AcademyContext.Domain.Entities
{
    public class Trainer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public TrainerName Name { get; private set; }
        public TrainerDisciplinesQuestionnaire DisciplinesQuestionnaire { get; set; }

#nullable disable
        public Trainer()
        {
        }
#nullable disable

        public void SetName(TrainerName name)
        {
            Name = name;
        }

        public void SetDisciplinesQuiestionnaire(TrainerDisciplinesQuestionnaire questionnaire)
        {
            DisciplinesQuestionnaire = questionnaire;
        }

        public void RequestToTraining(FighterClientId fighterId)
        {
            ValidateStateForTrain();
            DomainEvents.TrainingRequestCreated.Publish(new TrainingRequestCreated(fighterId, Id));

        }

        private void ValidateStateForTrain()
        {
            if (Name == null)
                throw new InvalidTrainerStateException("Name is missing");
            if (DisciplinesQuestionnaire == null)
                throw new InvalidTrainerStateException("Questionnaire is missing");
        }
    }
}
