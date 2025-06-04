using System;
using System.Collections.Generic;
using InterviewProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Data;

public partial class InterviewContext : DbContext
{
    private readonly IConfiguration configuration;

    public InterviewContext(DbContextOptions<InterviewContext> options, IConfiguration configuration)
        : base(options)
    {
        this.configuration = configuration;
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PurchaseCommission> PurchaseCommissions { get; set; }

    public virtual DbSet<PurchaseCommissionCustomer> PurchaseCommissionCustomers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        =>
        optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("DbConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.OrderNo).HasColumnName("OrderNO");
        });

        modelBuilder.Entity<PurchaseCommission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PurchaseCommision");

            entity.ToTable("PurchaseCommission");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ProductDescription).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.PurchaseCommissions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseCommission_Order");
        });

        modelBuilder.Entity<PurchaseCommissionCustomer>(entity =>
        {
            entity.ToTable("PurchaseCommissionCustomer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Price)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProducerName).HasMaxLength(50);
            entity.Property(e => e.PurchaseCondition).HasMaxLength(50);
            entity.Property(e => e.TotalPrice)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PurchaseCommission).WithMany(p => p.PurchaseCommissionCustomers)
                .HasForeignKey(d => d.PurchaseCommissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseCommissionCustomer_PurchaseCommission");

            entity.HasOne(d => d.Seller).WithMany(p => p.PurchaseCommissionCustomers)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseCommissionCustomer_Customer");
        });

        #region Seed
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = Guid.NewGuid(),
                Name = "شرکت شماره یک"
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Name = "شرکت شماره دو"
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Name = "شرکت شماره سه"
            }
        );



        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = Guid.NewGuid(),
                OrderNo = 5678
            },
            new Order
            {
                Id = Guid.NewGuid(),
                OrderNo = 1234
            }
        );
        #endregion

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
