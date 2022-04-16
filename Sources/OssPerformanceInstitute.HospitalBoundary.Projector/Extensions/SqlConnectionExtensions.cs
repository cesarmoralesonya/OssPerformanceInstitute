using Dapper;
using Microsoft.Data.SqlClient;
using OssPerformanceInstitute.HospitalBoundary.Domain.Entities;

namespace OssPerformanceInstitute.HospitalBoundary.Projector.Extensions
{
    public static class SqlConnectionExtensions
    {
        public static void EnsurePatientsTable(this SqlConnection conn)
        {
            var query = @"
                IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 't_patient'))
	                CREATE TABLE [dbo].[t_patient](
	                [patient_id] [uniqueidentifier] NOT NULL,
	                [blood_type] [nvarchar](max) NULL,
	                [weight] [decimal](18, 2) NULL,
	                [status] [nvarchar](max) NOT NULL,
                    [updated_on] [datetime] NOT NULL
                 CONSTRAINT [PK_patient] PRIMARY KEY CLUSTERED ([patient_id] ASC))";

            conn.Execute(query);
        }

        public static void InsertPatient(this SqlConnection conn, Patient patient)
        {
            conn.Execute(@"DELETE FROM t_patient WHERE patient_id = @Id 
                           INSERT INTO t_patient (patient_id, blood_type, weight, status, updated_on) VALUES (@Id, @BloodType, @Weight, @Status, GETUTCDATE())",
                           new { Id = patient.Id, BloodType = patient.BloodType?.Value, Weight = patient.Weight?.Value, Status = Enum.GetName(patient.Status) });
        }
    }
}
