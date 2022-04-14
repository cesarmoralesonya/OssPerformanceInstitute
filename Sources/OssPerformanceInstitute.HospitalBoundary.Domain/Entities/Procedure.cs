using OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Entities
{
    public class Procedure
    {
        public Guid Id { get; init; }
        public ProcedureName Name { get; private set; }

        internal Procedure(Guid id, string name)
        {
            Id = id;
            Name = ProcedureName.Create(name);
        }
#nullable disable
        protected Procedure()
        {
        }
#nullable disable

        public static Procedure Create(string name)
        {
            return new Procedure(Guid.NewGuid(), name);
        }
    }
}
