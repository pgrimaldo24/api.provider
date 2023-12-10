using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> entity)
        {
            entity.ToTable("Product", "provider_adm");

            entity.HasKey(e => e.Id)
               .HasName("PK__Product__3214EC070920BDD5");

            entity.Property(e => e.Id).HasColumnName("Id");

            entity.HasOne(d => d.Category)
                     .WithMany(p => p.Products)
                     .HasForeignKey(d => d.CategoryId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK__Product__Categor__5535A963");

            entity.HasOne(d => d.Merchant)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.MerchantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_MerchantId");

            entity.HasOne(d => d.Sucursal)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Sucursa__5629CD9C");
        }
    }
}
