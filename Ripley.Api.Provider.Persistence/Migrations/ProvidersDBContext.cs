using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class ProvidersDBContext : DbContext
    {
        public ProvidersDBContext()
        {
        }

        public ProvidersDBContext(DbContextOptions<ProvidersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Email> Emails { get; set; } = null!;
        public virtual DbSet<EmailHistory> EmailHistories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Provider> Providers { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<SubOrder> SubOrders { get; set; } = null!;
        public virtual DbSet<Sucursal> Sucursals { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-DQ7EEM30;Initial Catalog=ProvidersDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "provider_adm");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("Email", "provider_adm");

                entity.Property(e => e.Cc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("CC");

                entity.Property(e => e.ConfigContent)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ConfigSubject)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prv_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Email__UserId__66603565");
            });

            modelBuilder.Entity<EmailHistory>(entity =>
            {
                entity.ToTable("EmailHistory", "provider_adm");

                entity.Property(e => e.Cc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("CC");

                entity.Property(e => e.Content)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.EmailHistories)
                    .HasForeignKey(d => d.EmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmailHist__Email__6A30C649");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderGroup)
                    .HasName("PK__Order__03A47D089EEA6603");

                entity.ToTable("Order", "provider_adm");

                entity.Property(e => e.OrderGroup)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cajero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FecTrx).HasColumnType("datetime");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "provider_adm");

                entity.Property(e => e.BrutoVent).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ImpuestoVent).HasColumnType("decimal(18, 4)");

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
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.ToTable("Provider", "provider_adm");

                entity.Property(e => e.CategoryProvider)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.VendorName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VendorNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol", "provider_adm");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SubOrder", "provider_adm");

                entity.Property(e => e.Commission).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CouponCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CouponDiscount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderGroup)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ShippingCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SubOrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

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
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.ToTable("Sucursal", "provider_adm");

                entity.Property(e => e.Active).HasColumnName("ACTIVE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DesSucursual)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DES_SUCURSUAL");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "provider_adm");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.User1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RolId__44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
