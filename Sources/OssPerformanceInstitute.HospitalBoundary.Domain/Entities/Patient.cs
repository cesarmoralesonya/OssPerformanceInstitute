using OssPerformanceInstitute.HospitalBoundary.Domain.Events;
using OssPerformanceInstitute.HospitalBoundary.Domain.Exceptions;
using OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects;
using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Domain.Entities
{
    public class Patient : AggregateRoot
    {
        private readonly List<Procedure> _procedures;

        public Guid Id { get; private set; }
        public PatientBloodType BloodType { get; private set; }
        public PatientWeight Weight { get; private set; }
        public PatientStatus Status { get; private set; }

        public IReadOnlyCollection<Procedure> Procedures => _procedures;

#nullable disable
        public Patient(PatientId patientId)
        {
            _procedures = new();
            ApplyDomainEvent(new PatientCreated(patientId));
        }

        public Patient()
        {
            _procedures = new();
        }
#nullable disable

        public void SetBloodType(PatientBloodType bloodType)
        {
            ApplyDomainEvent(new PatientBloodTypeUpdated(Id, bloodType.Value));
        }

        public void SetWeight(PatientWeight weight)
        {
            ApplyDomainEvent(new PatientWeightUpdated(Id, weight.Value));
        }

        public void AddProcedure(Procedure procedure)
        {
            ApplyDomainEvent(new PatientProcedureAdded(Id, procedure.Id, procedure.Name.Value));
        }

        public void AdmitPatient()
        {
            ApplyDomainEvent(new PatientAdmitted(Id));
        }

        public void DischargePatient()
        {
            ApplyDomainEvent(new PatientDischarged(Id));
        }

        protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case PatientCreated e:
                    Id = e.Id;
                    Status = PatientStatus.Pending;
                    break;
                case PatientBloodTypeUpdated e:
                    BloodType = new PatientBloodType(e.BloodType);
                    break;
                case PatientWeightUpdated e:
                    Weight = new PatientWeight(e.Value);
                    break;
                case PatientAdmitted:
                    Status = PatientStatus.Admitted;
                    break;
                case PatientDischarged:
                    Status = PatientStatus.Discharged;
                    break;
                case PatientProcedureAdded e:
                    var newProcedure = new Procedure(e.Id, e.ProcedureName);
                    _procedures.Add(newProcedure);
                    break;
                default:
                    break;
            }
        }

        protected override void ValidateState()
        {
            var isValid =
               Status switch
               {
                   PatientStatus.Admitted => BloodType != null && Weight != null,
                   _ => true
               };
            if (!isValid)
            {
                throw new InvalidPatientStateException("Invalid patient state");
            }
        }
    }
}
