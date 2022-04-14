
namespace OssPerformanceInstitute.AcademyBoundary.Domain.ValueObjects.FighterClientEntity
{
    public record FighterClientId
    {
        public Guid Value { get; init; }
        internal FighterClientId(Guid value)
        {
            Value = value;
        }

        public static FighterClientId Create(Guid value)
        {
            Validate(value);
            return new FighterClientId(value);
        }

        public static implicit operator Guid(FighterClientId petId)
        {
            return petId.Value;
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
