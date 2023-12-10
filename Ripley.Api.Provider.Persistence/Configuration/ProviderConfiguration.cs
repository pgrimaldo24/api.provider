using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<ProviderEntity>
    {
        public void Configure(EntityTypeBuilder<ProviderEntity> entity)
        {
            entity.ToTable("Provider", "provider_adm");

            entity.HasKey(e => e.Id).HasName("PK__Provider__3214EC07C6BA7D1A");
            entity.Property(e => e.Id).HasColumnName("Id");

        }
    }
}
