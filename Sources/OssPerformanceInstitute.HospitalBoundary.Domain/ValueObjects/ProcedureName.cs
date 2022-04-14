
namespace OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects
{
    public record ProcedureName
    {
        public string Value { get; set; }

        public ProcedureName(string value)
        {
            Value = value;
        }

        public static ProcedureName Create(string value)
        {
            return new ProcedureName(value);
        }
    }
}
