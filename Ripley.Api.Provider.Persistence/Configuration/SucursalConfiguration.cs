using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    internal class SucursalConfiguration : IEntityTypeConfiguration<SucursalEntity>
    {
        public void Configure(EntityTypeBuilder<SucursalEntity> entity)
        {
            entity.ToTable("Sucursal", "provider_adm");
              
            entity.HasKey(e => e.Id).HasName("PK__Sucursal__3214EC075A660D5B");
            entity.Property(e => e.Id).HasColumnName("Id");

        }
    }
}
