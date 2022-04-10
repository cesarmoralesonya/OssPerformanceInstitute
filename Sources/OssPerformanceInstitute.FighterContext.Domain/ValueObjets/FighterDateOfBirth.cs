using System;

namespace OssPerformanceInstitute.FighterContext.Domain.ValueObjets
{
    public record FighterDateOfBirth
    {
        public DateTime Value { get; init; }

        internal FighterDateOfBirth(DateTime value)
        {
            Value = value;
        }

        public static FighterDateOfBirth Create(DateTime value)
        {
            Validate(value);
            return new FighterDateOfBirth(value);
        }

        public static implicit operator DateTime(FighterDateOfBirth FighterDateOfBirth)
        {
            return FighterDateOfBirth.Value;
        }

        private static void Validate(DateTime value)
        {
            if (value > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Date must not be greater than today");
            }
        }
    }
}