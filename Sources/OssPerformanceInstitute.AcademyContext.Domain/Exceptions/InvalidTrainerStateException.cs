
namespace OssPerformanceInstitute.AcademyContext.Domain.Exceptions
{
    public class InvalidTrainerStateException : Exception
    {
        public InvalidTrainerStateException(string? message) : base(message)
        {
        }
    }
}
