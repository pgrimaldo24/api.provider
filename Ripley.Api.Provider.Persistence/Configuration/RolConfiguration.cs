using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class RolConfiguration : IEntityTypeConfiguration<RolEntity>
    {
        public void Configure(EntityTypeBuilder<RolEntity> entity)
        {
            entity.ToTable("Rol", "provider_adm");

            entity.HasKey(e => e.RolId).HasName("PK__Rol__F92302F1A1BFD832");

            entity.Property(e => e.RolId).HasColumnName("RolId");
        }
    }
}
