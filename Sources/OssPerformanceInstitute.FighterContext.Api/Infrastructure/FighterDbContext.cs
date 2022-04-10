#nullable disable
using Microsoft.EntityFrameworkCore;
using OssPerformanceInstitute.FighterContext.Domain.Entities;

namespace OssPerformanceInstitute.FighterContext.Api.Infrastructure
{
    public class FighterDbContext : DbContext
    {
        public FighterDbContext(DbContextOptions<FighterDbContext> options) : base(options)
        {
        }

        public DbSet<Fighter> Fighters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fighter>().ToTable("t_fighter");
            modelBuilder.Entity<Fighter>().HasKey(x => x.Id);
            modelBuilder.Entity<Fighter>().Property(x => x.Id)
                                            .ValueGeneratedOnAdd()
                                            .HasColumnName("pk_fighter_id");

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.Name)
                                            .Property(p=>p.Value).HasColumnName("name");

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.SexOfFighter)
                                            .Property(t=>t.Value).HasColumnName("sex");

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.Citizenship, add =>
            {
                add.Property(p => p.Country).HasColumnName("country");
                add.Property(p => p.City).HasColumnName("city");
            });

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.DateOfBirth)
                                            .Property(p => p.Value)
                                            .HasColumnName("date_birth");
        }
    }
}
