using System;
using System.Collections.Generic;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BOs;

public partial class Dbprn221Context : DbContext
{
    public Dbprn221Context()
    {
    }

    public Dbprn221Context(DbContextOptions<Dbprn221Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionStringDB"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC072ED3F6E9");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CateId, "UQ__Category__27638D158BEADB71").IsUnique();

            entity.Property(e => e.CateId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07DDCB7410");

            entity.ToTable("Order");

            entity.HasIndex(e => e.OrderId, "UQ__Order__C3905BCE07D0629F").IsUnique();

            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StoreId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransationId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasPrincipalKey(p => p.ProductId)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Product");

            entity.HasOne(d => d.Transation).WithMany(p => p.Orders)
                .HasPrincipalKey(p => p.TransationId)
                .HasForeignKey(d => d.TransationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Transaction");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0776A2C284");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasPrincipalKey(p => p.OrderId)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasPrincipalKey(p => p.ProductId)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07399F7B25");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductId, "UQ__Product__B40CC6CCB8EA196C").IsUnique();

            entity.Property(e => e.CateId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.CateId)
                .HasForeignKey(d => d.CateId)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC071A12E16D");

            entity.ToTable("Transaction");

            entity.HasIndex(e => e.TransationId, "UQ__Transact__B1E73154FC1E15B6").IsUnique();

            entity.Property(e => e.TransationId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0796CBC932");

            entity.ToTable("User");

            entity.HasIndex(e => e.UserId, "UQ__User__1788CC4D57A57AAF").IsUnique();

            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FullName).IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Phone).IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
