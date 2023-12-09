using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entity)
        {
            entity.ToTable("User", "provider_adm");
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC076C39ED64");
            entity.Property(e => e.Id).HasColumnName("Id");
 
            entity.HasOne(d => d.Rol)
                  .WithMany(p => p.Users)
                  .HasForeignKey(d => d.RolId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__User__RolId__44FF419A");
        }
    }
}
