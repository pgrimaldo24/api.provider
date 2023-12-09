using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> entity)
        { 
            entity.ToTable("Order", "provider_adm");
            entity.HasKey(e => e.OrderGroup)
               .HasName("PK__Order__03A47D089EEA6603");
            entity.Property(e => e.OrderGroup).HasColumnName("OrderGroup");

            entity.Property(e => e.OrderGroup)
                  .HasMaxLength(100)
                  .IsUnicode(false)
                  .IsFixedLength(); 

            
        }
    }
}
