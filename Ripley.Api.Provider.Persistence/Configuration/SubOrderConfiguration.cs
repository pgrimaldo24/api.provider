using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class SubOrderConfiguration : IEntityTypeConfiguration<SubOrderEntity>
    {
        public void Configure(EntityTypeBuilder<SubOrderEntity> entity)
        {
            entity.HasNoKey();

            entity.ToTable("SubOrder", "provider_adm");

            entity.Property(e => e.Commission).HasColumnType("decimal(18, 4)");


            entity.HasOne(d => d.OrderGroupNavigation)
                   .WithMany()
                   .HasForeignKey(d => d.OrderGroup)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK__SubOrder__OrderG__628FA481");

            entity.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubOrder_Prd");
        }
    }
}
