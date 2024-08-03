using Clean.Architecture.And.DDD.Template.Application.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure.IntergrationEvents
{
    internal class IntegrationEventEntityTypeConfiguration : IEntityTypeConfiguration<IntegrationEvent>
    {
        public void Configure(EntityTypeBuilder<IntegrationEvent> builder)
        {
            builder.HasKey(x => x.IntergrationEventId);
            builder.Property(x => x.OccuredAt).HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.Type).HasMaxLength(500);
            builder.Property(x => x.Payload);
            builder.Property(x => x.PublishedAt);
        }
    }
}
