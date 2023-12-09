using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class EmailConfiguration : IEntityTypeConfiguration<EmailEntity>
    {
        public void Configure(EntityTypeBuilder<EmailEntity> entity)
        {
            entity.ToTable("Email", "provider_adm");
            entity.HasKey(e => e.Id).HasName("PK__Email__3214EC07BB59551F");
            entity.Property(e => e.Id).HasColumnName("Id"); 
        }
    }
}
