
namespace OssPerformanceInstitute.FighterContext.Domain.ValueObjets
{
    public record FighterName
    {
        public string Value { get; set; }
        internal FighterName(string value)
        {
            Value = value;
        }

        public static FighterName Create(string value)
        {
            Validate(value);
            return new FighterName(value);
        }

        public static implicit operator string(FighterName name)
        {
            return name.Value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Name must not be longer than 20 characters");
            }
        }
    }
}
