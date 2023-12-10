using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> entity)
        {
            entity.ToTable("Category", "provider_adm");
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07A4148D40");
            entity.Property(e => e.Id).HasColumnName("Id");
        }
    }
}
