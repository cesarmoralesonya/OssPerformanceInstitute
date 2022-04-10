using OssPerformanceInstitute.FighterContext.Domain.Services;

namespace OssPerformanceInstitute.FighterContext.Domain.ValueObjets
{
    public record FighterCitizenship
    {
        public string Country { get; init; }
        public string City { get; init; }
        
        internal FighterCitizenship(string country, string city)
        {
            Country = country;
            City = city;
        }

        public static FighterCitizenship Create(string country, string city, ICitizenshipService citizenshipService)
        {
            Validate(country, city, citizenshipService);
            return new FighterCitizenship(country, city);
        }

        public static implicit operator string(FighterCitizenship fighterCitizenship)
        {
            return $"{fighterCitizenship.Country}-{fighterCitizenship.City}";
        }

        private static void Validate(string country, string city, ICitizenshipService citizenshipService)
        {
            if (string.IsNullOrEmpty(country))
                throw new ArgumentException("country cannot be empty or null", nameof(country));

            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("city cannot be empty or null", nameof(city));

            var citizenship = citizenshipService?.Find(country, city);

            if (citizenship == null)
                throw new ArgumentException("citinzenship specified is not valid");
        }
    }
}
