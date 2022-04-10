
namespace OssPerformanceInstitute.FighterContext.Domain.ValueObjets
{
    public record SexOfFighter
    {
        public SexesOfFighters Value { get; init; }

        internal SexOfFighter(SexesOfFighters value)
        {
            Value = value;
        }

        public static SexOfFighter Create(SexesOfFighters value)
        {
            return new SexOfFighter(value);
        }

        public static implicit operator int (SexOfFighter sexOfFighter)
        {
            return (int)sexOfFighter.Value;
        }

        public static SexOfFighter Male = new(SexesOfFighters.Male);
        public static SexOfFighter Female = new(SexesOfFighters.Female);
    }

    public enum SexesOfFighters
    {
        Male,
        Female
    }
}
