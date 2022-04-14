
namespace OssPerformanceInstitute.FighterBoundary.Domain.Expections
{
    public class InvalidFighterStateException : Exception
    {
        public InvalidFighterStateException(string? message) : base(message)
        {
        }
    }
}
