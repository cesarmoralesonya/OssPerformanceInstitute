
namespace OssPerformanceInstitute.AcademyContext.Domain.ValueObjects.TrainerEntity
{
    public record TrainerId
    {
        public Guid Value { get; init; }
        internal TrainerId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(TrainerId id)
        {
            return id.Value;
        }

        public static TrainerId Create(Guid value)
        {
            return new TrainerId(value);
        }
    }
}
