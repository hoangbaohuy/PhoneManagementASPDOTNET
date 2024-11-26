using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public partial class PhoneDbContext : DbContext
{
    public PhoneDbContext()
    {
    }

    public PhoneDbContext(DbContextOptions<PhoneDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneItem> PhoneItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=1234567890;Database=PhoneDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__DAD4F05E278BEC23");

            entity.ToTable("Brand");

            entity.HasIndex(e => e.BrandName, "UQ__Brand__2206CE9B484EF6F3").IsUnique();

            entity.Property(e => e.BrandName).HasMaxLength(255);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Model__E8D7A12C1534C53A");

            entity.ToTable("Model");

            entity.HasIndex(e => e.BrandId, "IDX_Model_BrandId");

            entity.Property(e => e.ModelName).HasMaxLength(255);
            entity.Property(e => e.OperatingSystem).HasMaxLength(255);
            entity.Property(e => e.Ram).HasColumnName("RAM");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Model_Brand");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF0D0D3870");

            entity.ToTable("Order");

            entity.HasIndex(e => e.UserId, "IDX_Order_UserId");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36C0D255425");

            entity.ToTable("OrderDetail");

            entity.HasIndex(e => e.OrderId, "IDX_OrderDetail_OrderId");

            entity.HasIndex(e => e.PhoneId, "IDX_OrderDetail_PhoneId");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Phone).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.PhoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Phone");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38290F0B00");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.OrderId, "UQ__Payment__C3905BCE2B39BDC0").IsUnique();

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Order");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.PhoneId).HasName("PK__Phone__F3EE4BB00FF1CB31");

            entity.ToTable("Phone");

            entity.HasIndex(e => e.ModelId, "IDX_Phone_ModelId");

            entity.Property(e => e.Chipset).HasMaxLength(255);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Gpu)
                .HasMaxLength(255)
                .HasColumnName("GPU");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Model).WithMany(p => p.Phones)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phone_Model");
        });

        modelBuilder.Entity<PhoneItem>(entity =>
        {
            entity.HasKey(e => e.PhoneItemId).HasName("PK__PhoneIte__68AAD368F940B074");

            entity.ToTable("PhoneItem");

            entity.HasIndex(e => e.OrderDetailId, "IDX_PhoneItem_OrderDetailId");

            entity.HasIndex(e => e.PhoneId, "IDX_PhoneItem_PhoneId");

            entity.HasIndex(e => e.SerialNumber, "UQ__PhoneIte__048A000879D246D5").IsUnique();

            entity.Property(e => e.DateImported)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DatePurchased).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.SerialNumber).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Available");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.PhoneItems)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("FK_PhoneItem_OrderDetail");

            entity.HasOne(d => d.Phone).WithMany(p => p.PhoneItems)
                .HasForeignKey(d => d.PhoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhoneItem_Phone");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD494544C");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E3821027").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456948D54E9").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("Customer");
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
