using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProductManagementDemo.Models;

public partial class MyStoreContext : DbContext
{
    public MyStoreContext()
    {
    }

    public MyStoreContext(DbContextOptions<MyStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountMember> AccountMembers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountMember>(entity =>
        {
            entity.HasKey(e => e.MemberID).HasName("PK__AccountM__0CF04B38B93AFC52");

            entity.ToTable("AccountMember");

            entity.HasIndex(e => e.EmailAddress, "UQ__AccountM__49A14740AEF323FE").IsUnique();

            entity.HasIndex(e => e.EmailAddress, "UQ__AccountM__49A14740EFC528E7").IsUnique();

            entity.Property(e => e.MemberID)
                .HasMaxLength(20)
                .HasColumnName("MemberID");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(80);
            entity.Property(e => e.MemberPassword).HasMaxLength(80);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryID).HasName("PK__Categori__19093A2B55844320");

            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(15);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductID).HasName("PK__Products__B40CC6ED378361E2");

            entity.Property(e => e.ProductID).HasColumnName("ProductID");
            entity.Property(e => e.CategoryID).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName).HasMaxLength(40);
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryID)
                .HasConstraintName("FK__Products__Catego__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionString"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Tìm file appsettings.json trong project chạy chính
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(basePath, "appsettings.json");

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path, optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration["ConnectionStrings:DefaultConnectionStringDB"];
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
