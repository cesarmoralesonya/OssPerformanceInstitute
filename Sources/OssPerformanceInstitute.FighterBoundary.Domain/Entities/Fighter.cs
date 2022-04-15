using OssPerformanceInstitute.FighterBoundary.Domain.Events;
using OssPerformanceInstitute.FighterBoundary.Domain.Expections;
using OssPerformanceInstitute.FighterBoundary.Domain.ValueObjets;
using System.ComponentModel.DataAnnotations.Schema;

namespace OssPerformanceInstitute.FighterBoundary.Domain.Entities
{
    public class Fighter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public FighterName Name { get; private set; }
        public FighterCitizenship Citizenship { get; private set; }
        public SexOfFighter SexOfFighter { get; private set; }
        public FighterDateOfBirth DateOfBirth { get; private set; }

#nullable disable
        public Fighter() 
        {
        }
#nullable disable

        public void SetName(FighterName name)
        {
            Name = name;
        }

        public void SetCitizenship(FighterCitizenship citizenship)
        {
            Citizenship = citizenship;
        }

        public void SetSexOfFighter(SexOfFighter sexOfFighter)
        {
            SexOfFighter = sexOfFighter;
        }

        public void SetDateOfBirth(FighterDateOfBirth dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public void FlagForTrain()
        {
            ValidateStateForTrain();
            DomainEvents.FighterFlaggedForTrain.Publish(new FighterFlaggedForTrain(Id, Name, Citizenship.Country, Citizenship.City, SexOfFighter, DateOfBirth));
        }

        public void TransferToHospital()
        {
            ValidateStateForTransfer();
            DomainEvents.FighterTransferredToHospital.Publish(new FighterTransferredToHospital(Id, Name, SexOfFighter, DateOfBirth));
        }

        private void ValidateStateForTrain()
        {
            if (Name == null)
                throw new InvalidFighterStateException("Name is missing");

            if (Citizenship == null)
                throw new InvalidFighterStateException("Citizenship is missing");

            if (SexOfFighter == null)
                throw new InvalidFighterStateException("Sex of fighter is missing");

            if (DateOfBirth == null)
                throw new InvalidFighterStateException("Date of birth is missing");            
        }

        private void ValidateStateForTransfer()
        {
            if (Name == null)
                throw new InvalidFighterStateException("Name is missing");

            if (Citizenship == null)
                throw new InvalidFighterStateException("Citizenship is missing");

            if (SexOfFighter == null)
                throw new InvalidFighterStateException("Sex of fighter is missing");

            if (DateOfBirth == null)
                throw new InvalidFighterStateException("Date of birth is missing");
        }
    }
}
