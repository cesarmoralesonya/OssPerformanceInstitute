using OssPerformanceInstitute.HospitalBoundary.Api.Commands;
using OssPerformanceInstitute.HospitalBoundary.Domain.Entities;
using OssPerformanceInstitute.HospitalBoundary.Domain.Repositories;
using OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects;

namespace OssPerformanceInstitute.HospitalBoundary.Api.ApplicationServices
{
    public class HospitalApplicationService
    {
        private readonly IPatientAggregateStore _patientAggregateStore;

        public HospitalApplicationService(IPatientAggregateStore patientAggregateStore)
        {
            this._patientAggregateStore = patientAggregateStore;
        }

        public async Task HandleAsync(SetWeightCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(PatientId.Create(command.Id));
            patient.SetWeight(PatientWeight.Create(command.Weight));
            await _patientAggregateStore.SaveAsync(patient);
        }

        public async Task HandleAsync(SetBloodTypeCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(PatientId.Create(command.Id));
            patient.SetBloodType(PatientBloodType.Create(command.BloodType));
            await _patientAggregateStore.SaveAsync(patient);
        }

        public async Task HandleAsync(AdmitPatientCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(PatientId.Create(command.Id));
            patient.AdmitPatient();
            await _patientAggregateStore.SaveAsync(patient);
        }

        public async Task HandleAsync(DischargePatientCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(PatientId.Create(command.Id));
            patient.DischargePatient();
            await _patientAggregateStore.SaveAsync(patient);
        }

        public async Task HandleAsync(AddProcedureCommand command)
        {
            var patient = await _patientAggregateStore.LoadAsync(PatientId.Create(command.Id));
            patient.AddProcedure(Procedure.Create(command.Procedure));
            await _patientAggregateStore.SaveAsync(patient);
        }
    }
}
