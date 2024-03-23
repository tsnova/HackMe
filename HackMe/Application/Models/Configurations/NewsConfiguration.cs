using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackMe.Application.Models.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.UrlKey).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(2000);
        }
    }
}
