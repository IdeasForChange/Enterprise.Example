using Enterprise.Example.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise.Example.Data.Mappings
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Subject).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Header).IsRequired().HasMaxLength(2000);
            builder.Property(p => p.Footer).IsRequired().HasMaxLength(2000);
            builder.Property(p => p.EmailTemplate).IsRequired().HasMaxLength(2000);

            builder.HasKey(x => x.Id);
        }
    }
}
