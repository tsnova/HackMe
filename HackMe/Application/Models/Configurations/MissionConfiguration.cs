using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackMe.Application.Models.Configurations
{
    public class MissionConfiguration : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.UrlKey).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(2000);
        }
    }
}
