
namespace OssPerformanceInstitute.FighterContext.Domain.Expections
{
    public class FighterNotFoundException : Exception
    {
        public FighterNotFoundException(Guid Id) : base($"the fighter Id {Id} does not match to any record")
        {
        }
    }
}
