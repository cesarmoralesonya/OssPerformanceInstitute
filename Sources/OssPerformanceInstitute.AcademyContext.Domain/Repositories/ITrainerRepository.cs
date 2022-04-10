﻿using OssPerformanceInstitute.AcademyContext.Domain.Entities;
using OssPerformanceInstitute.SharedKernel.Domain.Repositories;

namespace OssPerformanceInstitute.AcademyContext.Domain.Repositories
{
    public interface ITrainerRepository : IAsyncRepository<Trainer>
    {
    }
}