using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Api.IntegrationEvents
{
    public record FighterTransferredToHospitalIntegrationEvent(Guid Id, 
                                                                string Name,
                                                                int Sex, 
                                                                DateTime DateOfBirth): IIntegrationEvent { }
}