using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.FighterContext.Domain.Events
{
    public static class DomainEvents
    {
        public static readonly DomainEvent<FighterFlaggedForTrain> FighterFlaggedForTrain = new();
        public static readonly DomainEvent<FighterTransferredToHospital> FighterTransferredToHospital = new();
    }
}
