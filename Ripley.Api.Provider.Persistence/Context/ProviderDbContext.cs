using Microsoft.EntityFrameworkCore;
using Ripley.Api.Provider.Domain.Entities;
using Ripley.Api.Provider.Persistence.Configuration;

namespace Ripley.Api.Provider.Persistence.Context
{
    public class ProviderDbContext : DbContext
    {
        public ProviderDbContext(DbContextOptions<ProviderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new EmailConfiguration());
            modelBuilder.ApplyConfiguration(new EmailHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
            modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new SubOrderConfiguration());
            modelBuilder.ApplyConfiguration(new SucursalConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }

        public virtual DbSet<CategoryEntity> Categories { get; set; } = null!;
        public virtual DbSet<EmailEntity> Emails { get; set; } = null!;
        public virtual DbSet<EmailHistoryEntity> EmailHistories { get; set; } = null!;
        public virtual DbSet<OrderEntity> Orders { get; set; } = null!;
        public virtual DbSet<ProductEntity> Products { get; set; } = null!;
        public virtual DbSet<ProviderEntity> Providers { get; set; } = null!;
        public virtual DbSet<RolEntity> Rols { get; set; } = null!;
        public virtual DbSet<SubOrderEntity> SubOrders { get; set; } = null!;
        public virtual DbSet<SucursalEntity> Sucursals { get; set; } = null!;
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
    }
}
