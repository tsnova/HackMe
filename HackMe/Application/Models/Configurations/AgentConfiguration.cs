using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackMe.Application.Models.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasKey(x => x.CodeName);

            builder.Property(x => x.CodeName).HasMaxLength(20);
            builder.Property(x => x.Password).HasMaxLength(20);
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.Ranking).HasMaxLength(10);
            builder.Property(x => x.PersonalData).HasMaxLength(2000);
            builder.Property(x => x.ActiveMission).HasMaxLength(200);
        }
    }
}
