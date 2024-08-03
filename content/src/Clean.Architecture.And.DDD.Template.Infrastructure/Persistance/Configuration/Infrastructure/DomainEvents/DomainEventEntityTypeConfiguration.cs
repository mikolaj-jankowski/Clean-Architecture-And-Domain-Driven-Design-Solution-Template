using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure.DomainEvents
{
    internal class DomainEventEntityTypeConfiguration : IEntityTypeConfiguration<DomainEvent>
    {
        public void Configure(EntityTypeBuilder<DomainEvent> builder)
        {
            builder.HasKey(x => x.DomainEventId);
            builder.Property(x => x.OccuredAt).HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.Type).HasMaxLength(500);
            builder.Property(x => x.Payload);
            builder.Property(x => x.ComplatedAt);
        }
    }
}
