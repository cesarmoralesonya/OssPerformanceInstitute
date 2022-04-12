using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssPerformanceInstitute.AcademyContext.Domain.Exceptions
{
    public class TrainerNotFoundExeption : Exception
    {
        public TrainerNotFoundExeption(Guid Id) : base($"the trainer Id {Id} does not match to any record")
        {
        }
    }
}
