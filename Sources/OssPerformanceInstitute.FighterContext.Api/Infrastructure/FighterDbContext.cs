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

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.Name);

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.SexOfFighter);

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.Citizenship);

            modelBuilder.Entity<Fighter>().OwnsOne(x => x.DateOfBirth);
        }
    }
}
