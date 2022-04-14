#nullable disable
using Microsoft.EntityFrameworkCore;
using OssPerformanceInstitute.AcademyBoundary.Api.IntegrationEvents;
using OssPerformanceInstitute.AcademyBoundary.Domain.Entities;

namespace OssPerformanceInstitute.AcademyBoundary.Api.Infrastructure
{
    public class AcademyDbContext : DbContext
    {
        public AcademyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<FighterClient> FighterClients { get; set; }
        public DbSet<FighterFlaggedForTrainIntegrationEvent> FighterClientsMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trainer>().ToTable("t_trainer");
            modelBuilder.Entity<Trainer>().HasKey(t => t.Id);
            modelBuilder.Entity<Trainer>().Property(t => t.Id)
                                            .ValueGeneratedOnAdd()
                                            .HasColumnName("pk_trainer_id");
            modelBuilder.Entity<Trainer>().OwnsOne(t => t.Name, add =>
            {
                add.Property(p => p.Value).HasColumnName("name");
            });
            modelBuilder.Entity<Trainer>().OwnsOne(t => t.DisciplinesQuestionnaire, add =>
            {
                add.Property(p => p.IsMuayThaiTrainer).HasColumnName("muay_thai");
                add.Property(p => p.IsBoxingTrainner).HasColumnName("boxing");
                add.Property(p => p.IsMmaTrainner).HasColumnName("mma");
                add.Property(p => p.IsBjjTrainner).HasColumnName("bjj");
                add.Property(p => p.IsKickBoxingTrainner).HasColumnName("kick_boxing");
            });
            
            modelBuilder.Entity<FighterClient>().ToTable("t_fighter_client");
            modelBuilder.Entity<FighterClient>().HasKey(t => t.Id);
            modelBuilder.Entity<FighterClient>().Property(t => t.Id)
                                                    .HasColumnName("pk_fighter_client_id");
            modelBuilder.Entity<FighterClient>().OwnsOne(t => t.TrainerId, add =>
            {
                add.Property(p => p.Value).HasColumnName("trainer_id");
            });

            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().ToTable("t_fighter_client_metadata");
            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().HasKey(t => t.Id);
            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().Property(t => t.Id)
                                                                            .HasColumnName("fighter_client_id");
            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().Property(t => t.Name)
                                                                            .HasColumnName("name");
            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().Property(t => t.Sex)
                                                                            .HasColumnName("sex");
            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().Property(t => t.Country)
                                                                            .HasColumnName("country");
            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().Property(t => t.City)
                                                                            .HasColumnName("city");
            modelBuilder.Entity<FighterFlaggedForTrainIntegrationEvent>().Property(t => t.DateOfBirth)
                                                                            .HasColumnName("date_birth");
        }
    }
}
