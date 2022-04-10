using OssPerformanceInstitute.FighterContext.Domain.ValueObjets;

namespace OssPerformanceInstitute.FighterContext.Domain.Services
{
    public interface ICitizenshipService
    {
        FighterCitizenship? Find(string county, string city);
    }
}
