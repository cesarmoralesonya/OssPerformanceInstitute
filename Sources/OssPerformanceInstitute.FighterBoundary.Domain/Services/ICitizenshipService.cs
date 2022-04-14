using OssPerformanceInstitute.FighterBoundary.Domain.ValueObjets;

namespace OssPerformanceInstitute.FighterBoundary.Domain.Services
{
    public interface ICitizenshipService
    {
        FighterCitizenship? Find(string county, string city);
    }
}
