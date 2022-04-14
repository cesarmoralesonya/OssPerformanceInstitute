
using System.ComponentModel.DataAnnotations.Schema;

namespace OssPerformanceInstitute.AcademyBoundary.Domain.ValueObjects.TrainerEntity
{
    public record TrainerName
    {
        [Column("name")]
        public string Value { get; init; }

        internal TrainerName(string value)
        {
            Value = value;
        }

        public static TrainerName Create(string value)
        {
            Validate(value);
            return new TrainerName(value);
        }

        private static void Validate(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Name must not be null");
            }

            if (value.Length > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Name must not be longer than 50 characters");
            }
        }
    }
}
