using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.FighterContext.Domain.Events
{
    public static class DomainEvents
    {
        public static DomainEvent<FighterFlaggedForFight> FighterFlaggedForFight = new();
        public static DomainEvent<FighterTransferredToHospital> FighterTransferredToHospital = new();
    }
}
