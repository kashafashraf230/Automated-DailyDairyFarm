using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DailyDairyAuto.Models;

public partial class DailyDairyContext : DbContext
{
    public DailyDairyContext()
    {
    }

    public DailyDairyContext(DbContextOptions<DailyDairyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CattleDatum> CattleData { get; set; }

    public virtual DbSet<CowDatum> CowData { get; set; }

    public virtual DbSet<CowHealth> CowHealths { get; set; }


    public virtual DbSet<Lactation> lactationstage { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                => optionsBuilder.UseSqlServer("Data Source=KASHAF\\SQLEXPRESS02;Initial Catalog=daily-dairyfinal;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CattleDatum>(entity =>
        {
            entity.HasKey(e => e.GroupId);

            entity.ToTable("Cattle_Data");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.GrpAvgMilk).HasColumnName("grp_avg_milk");
            entity.Property(e => e.TotalCows).HasColumnName("total_cows");
        });

        modelBuilder.Entity<CowDatum>(entity =>
        {
            entity.HasKey(e => e.CowId);

            entity.ToTable("Cow_Data");

            entity.Property(e => e.CowId).HasColumnName("cow_id");
            entity.Property(e => e.Age)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("age");
            entity.Property(e => e.AvgMilkProd).HasColumnName("avg_milk_prod");
            entity.Property(e => e.Breed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("breed");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Vaccinated)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("vaccinated");

            entity.HasOne(d => d.Group).WithMany(p => p.CowData)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cow_Data__group___4CA06362");
        });

        modelBuilder.Entity<CowHealth>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Cow_Health");

            entity.Property(e => e.CowId).HasColumnName("cow_id");
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("health_status");

            entity.HasOne(d => d.Cow).WithMany()
                .HasForeignKey(d => d.CowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cow_Healt__cow_i__4D94879B");
        });

        modelBuilder.Entity<Lactation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("lactation_stage");

            entity.Property(e => e.CowId).HasColumnName("cow_id");
            entity.Property(e => e.lactation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lactation");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
