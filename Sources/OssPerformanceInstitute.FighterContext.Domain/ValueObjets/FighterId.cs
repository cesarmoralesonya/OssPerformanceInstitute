
namespace OssPerformanceInstitute.FighterContext.Domain.ValueObjets
{
    public record FighterId
    {
        public Guid Value { get; init; }
        internal FighterId(Guid value)
        {
            Value = value;
        }

        public static FighterId Create(Guid value)
        {
            Validate(value);
            return new FighterId(value);
        }

        public static implicit operator Guid(FighterId FighterId)
        {
            return FighterId.Value;
        }

        private static void Validate(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Id should not be empty", nameof(value));
            }
        }
    }
}
