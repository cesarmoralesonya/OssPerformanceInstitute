using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.FighterBoundary.Domain.Events
{
    public static class DomainEvents
    {
        public static readonly DomainEvent<FighterFlaggedForTrain> FighterFlaggedForTrain = new();
        public static readonly DomainEvent<FighterTransferredToHospital> FighterTransferredToHospital = new();
    }
}
