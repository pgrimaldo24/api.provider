using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class EmailHistoryConfiguration : IEntityTypeConfiguration<EmailHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<EmailHistoryEntity> entity)
        {
            entity.ToTable("EmailHistory", "provider_adm");

            entity.HasKey(e => e.Id).HasName("PK__EmailHis__3214EC07A2AAB61B");
            entity.Property(e => e.Id).HasColumnName("Id");
        }
    }
}
