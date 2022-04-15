#nullable disable
using Microsoft.EntityFrameworkCore;
using OssPerformanceInstitute.HospitalBoundary.Api.IntegrationEvents;

namespace OssPerformanceInstitute.HospitalBoundary.Api.Infrastructure
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }

        public DbSet<FighterTransferredToHospitalIntegrationEvent> PatientsMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FighterTransferredToHospitalIntegrationEvent>().ToTable("t_patient_metadata");
            modelBuilder.Entity<FighterTransferredToHospitalIntegrationEvent>().HasKey(t => t.Id);
            modelBuilder.Entity<FighterTransferredToHospitalIntegrationEvent>().Property(t => t.Id)
                                                                            .HasColumnName("patient_id");
            modelBuilder.Entity<FighterTransferredToHospitalIntegrationEvent>().Property(t => t.Name)
                                                                            .HasColumnName("name");
            modelBuilder.Entity<FighterTransferredToHospitalIntegrationEvent>().Property(t => t.Sex)
                                                                            .HasColumnName("sex");
            modelBuilder.Entity<FighterTransferredToHospitalIntegrationEvent>().Property(t => t.DateOfBirth)
                                                                            .HasColumnName("date_birth");
        }
    }
}
