using OssPerformanceInstitute.FighterContext.Api.Commands;
using OssPerformanceInstitute.FighterContext.Api.IntegrationEvents;
using OssPerformanceInstitute.FighterContext.Domain.Entities;
using OssPerformanceInstitute.FighterContext.Domain.Events;
using OssPerformanceInstitute.FighterContext.Domain.Expections;
using OssPerformanceInstitute.FighterContext.Domain.Repositories;
using OssPerformanceInstitute.FighterContext.Domain.Services;
using OssPerformanceInstitute.FighterContext.Domain.ValueObjets;
using OssPerformanceInstitute.SharedKernel.Common.Application;
using System.Reflection;

namespace OssPerformanceInstitute.FighterContext.Api.Application
{
    public class FighterApplicationService : ApplicationServiceBase
    {
        private readonly IFighterRepository _fighterRepository;
        private readonly ICitizenshipService _citizenshipService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FighterApplicationService> _logger;

        public FighterApplicationService(IFighterRepository fighterRepository,
                                            ICitizenshipService citizenshipService,
                                            IConfiguration configuration,
                                            ILogger<FighterApplicationService> logger): base(logger)
        {
            this._fighterRepository = fighterRepository ?? throw new ArgumentNullException(nameof(fighterRepository));
            this._citizenshipService = citizenshipService ?? throw new ArgumentNullException(nameof(citizenshipService));
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));

            DomainEvents.FighterFlaggedForTrain.Register(async fighter =>
            {
                var integrationEvent = new FighterFlaggedForTrainIntegrationEvent(fighter.Id,
                                                                                    fighter.Name,
                                                                                    fighter.Country,
                                                                                    fighter.City,
                                                                                    fighter.Sex,
                                                                                    fighter.DateOfBirth);

                await PublishIntegrationEventAsync(integrationEvent, configuration["ServicesBus:ConnectionString"], configuration["ServicesBus:Train:TopicName"]);
            });
        }

        public async Task<Fighter> HandleCommandAsync(CreateFighterCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var fighter = new Fighter();
                fighter.SetName(FighterName.Create(command.Name));
                fighter.SetCitizenship(FighterCitizenship.Create(command.Country, command.City, _citizenshipService));
                fighter.SetSexOfFighter(SexOfFighter.Create((SexesOfFighters)command.Sex));
                fighter.SetDateOfBirth(FighterDateOfBirth.Create(command.DateOfBirth));

                return await _fighterRepository.AddAsync(fighter, cancellationToken);
            }
            catch (Exception ex)
            {
                 var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                throw;
            }
        }

        public async Task HandleCommandAsync(FlagForTrainCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var fighter = await _fighterRepository.GetByIdAsync(command.Id, cancellationToken);
                
                if (fighter == null)
                    throw new FighterNotFoundException(command.Id);

                fighter?.FlagForTrain();
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                throw;
            }
        }
    }
}
