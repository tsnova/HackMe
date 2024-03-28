using HackMe.Application.Models;
using HackMe.Application.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HackMe.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<Mission> News { get; set; } = null!;
        public DbSet<ChallengeTask> ChallengeTasks { get; set; } = null!;
        public DbSet<ChallengeResult> ChallengeResults { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
