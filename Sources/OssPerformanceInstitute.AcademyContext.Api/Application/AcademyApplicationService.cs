﻿using OssPerformanceInstitute.AcademyContext.Domain.Repositories;
using OssPerformanceInstitute.AcademyContext.Domain.ValueObjects.FighterClientEntity;
using OssPerformanceInstitute.AcademyContext.Domain.ValueObjects.TrainerEntity;
using OssPerformanceInstitute.AcademyContext.Domain.Events;
using OssPerformanceInstitute.AcademyContext.Api.Commands;
using OssPerformanceInstitute.AcademyContext.Domain.Entities;
using System.Reflection;

namespace OssPerformanceInstitute.AcademyContext.Api.Application
{
    public class AcademyApplicationService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly ILogger<AcademyApplicationService> _logger;
        public AcademyApplicationService(ILogger<AcademyApplicationService> logger,
                                            ITrainerRepository trainerRepository,
                                            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _trainerRepository = trainerRepository ?? throw new ArgumentNullException(nameof(trainerRepository));
            DomainEvents.TrainingRequestCreated.Register(async request =>
            {
                using var scope = serviceScopeFactory.CreateScope();
                var fighterClientRepository = scope.ServiceProvider.GetRequiredService<IFighterClientRepository>();
                var fighterClient = await fighterClientRepository.GetByIdAsync(FighterClientId.Create(request.FighterClientId));
                if(fighterClient!=null)
                {
                    fighterClient.RequestToTrain(TrainerId.Create(request.TrainerId));
                    await fighterClientRepository.UpdateAsync(fighterClient);
                }
            });
        }

        public async Task<Trainer> HandleCommandAsync(CreateTrainerCommand command)
        {
            try
            {
                var trainer = new Trainer();
                trainer.SetName(TrainerName.Create(command.Name));
                trainer.SetDisciplinesQuiestionnaire(TrainerDisciplinesQuestionnaire.Create(
                    command.Questionnaire.IsMuayThaiTrainer,
                    command.Questionnaire.IsBjjTrainner,
                    command.Questionnaire.IsBoxingTrainner,
                    command.Questionnaire.IsKickBoxingTrainner,
                    command.Questionnaire.IsMmaTrainner));

                return await _trainerRepository.AddAsync(trainer);
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