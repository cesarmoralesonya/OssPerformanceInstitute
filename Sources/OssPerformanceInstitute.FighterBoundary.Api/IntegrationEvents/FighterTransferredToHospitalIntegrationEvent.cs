using OssPerformanceInstitute.SharedKernel.Common;
using System;

namespace OssPerformanceInstitute.FighterBoundary.Api.IntegrationEvents
{
    public record FighterTransferredToHospitalIntegrationEvent(Guid Id, 
                                                                string Name,
                                                                int Sex, 
                                                                DateTime DateOfBirth): IIntegrationEvent { }
}