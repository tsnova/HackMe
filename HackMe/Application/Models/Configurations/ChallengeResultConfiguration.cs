using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackMe.Application.Models.Configurations
{
    public class ChallengeResultConfiguration : IEntityTypeConfiguration<ChallengeResult>
    {
        public void Configure(EntityTypeBuilder<ChallengeResult> builder)
        {
            builder.HasOne(x => x.ChallengeTask)
                .WithMany(x => x.Results)
                .HasForeignKey(x => x.ChallangeTaskId);
        }
    }
}
