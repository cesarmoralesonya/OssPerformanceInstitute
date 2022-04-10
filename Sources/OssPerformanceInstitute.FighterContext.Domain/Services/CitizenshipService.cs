using OssPerformanceInstitute.FighterContext.Domain.ValueObjets;

namespace OssPerformanceInstitute.FighterContext.Domain.Services
{
    public class CitizenshipService : ICitizenshipService
    {
        private readonly List<FighterCitizenship> _fightersCitizenships = new();
        public CitizenshipService()
        {
            _fightersCitizenships.Add(new FighterCitizenship("Ecuador", "Quito"));
            _fightersCitizenships.Add(new FighterCitizenship("Spain", "Zaragoza"));
        }

        public FighterCitizenship? Find(string county, string city)
        {
            return _fightersCitizenships.FirstOrDefault(b => b.Country == county && b.City == city);
        }
    }
}
