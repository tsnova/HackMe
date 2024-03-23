using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackMe.Application.Models.Configurations
{
    public class ChallengeTaskConfiguration : IEntityTypeConfiguration<ChallengeTask>
    {
        public void Configure(EntityTypeBuilder<ChallengeTask> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);
        }
    }
}
