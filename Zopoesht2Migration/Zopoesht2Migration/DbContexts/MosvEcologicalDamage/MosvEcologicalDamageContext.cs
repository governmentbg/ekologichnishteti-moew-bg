using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage;

public partial class MosvEcologicalDamageContext : DbContext
{
    public MosvEcologicalDamageContext()
    {
    }

    public MosvEcologicalDamageContext(DbContextOptions<MosvEcologicalDamageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<ActivityPart> ActivityParts { get; set; }

    public virtual DbSet<ActivityType> ActivityTypes { get; set; }

    public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }

    public virtual DbSet<LegalAct> LegalActs { get; set; }

    public virtual DbSet<LegalActPart> LegalActParts { get; set; }

    public virtual DbSet<LegalActType> LegalActTypes { get; set; }

    public virtual DbSet<OperatorBasic> OperatorBasics { get; set; }

    public virtual DbSet<OperatorBasicPart> OperatorBasicParts { get; set; }

    public virtual DbSet<OperatorCommit> OperatorCommits { get; set; }

    public virtual DbSet<OperatorCorrespondenceDataPart> OperatorCorrespondenceDataParts { get; set; }

    public virtual DbSet<OperatorCorrespondenceDatum> OperatorCorrespondenceData { get; set; }

    public virtual DbSet<Terrain> Terrains { get; set; }

    public virtual DbSet<TerrainPart> TerrainParts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(DbConfigurations.Zopoesht1ProdContext);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activity");

            entity.Property(e => e.Other).IsUnicode(false);

            entity.HasOne(d => d.ActivityType).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ActivityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activity_ActivityType");
        });

        modelBuilder.Entity<ActivityPart>(entity =>
        {
            entity.ToTable("ActivityPart");

            entity.HasOne(d => d.Commit).WithMany(p => p.ActivityParts)
                .HasForeignKey(d => d.CommitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityPart_OperatorCommit");

            entity.HasOne(d => d.Entity).WithMany(p => p.ActivityParts)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityPart_Activity");

            entity.HasOne(d => d.InitialPart).WithMany(p => p.InverseInitialPart)
                .HasForeignKey(d => d.InitialPartId)
                .HasConstraintName("FK_ActivityPart_ActivityPart");
        });

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.ToTable("ActivityType");

            entity.Property(e => e.Code).HasMaxLength(20);
        });

        modelBuilder.Entity<AdministrativeUnit>(entity =>
        {
            entity.ToTable("AdministrativeUnit");
        });

        modelBuilder.Entity<LegalAct>(entity =>
        {
            entity.ToTable("LegalAct");

            entity.Property(e => e.Number).HasMaxLength(200);

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.LegalActs)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegalAct_AdministrativeUnit");

            entity.HasOne(d => d.Type).WithMany(p => p.LegalActs)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_LegalAct_LegalActType");
        });

        modelBuilder.Entity<LegalActPart>(entity =>
        {
            entity.ToTable("LegalActPart");

            entity.HasOne(d => d.Commit).WithMany(p => p.LegalActParts)
                .HasForeignKey(d => d.CommitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegalActPart_OperatorCommit");

            entity.HasOne(d => d.Entity).WithMany(p => p.LegalActParts)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegalActPart_LegalAct");

            entity.HasOne(d => d.InitialPart).WithMany(p => p.InverseInitialPart)
                .HasForeignKey(d => d.InitialPartId)
                .HasConstraintName("FK_LegalActPart_LegalActPart");
        });

        modelBuilder.Entity<LegalActType>(entity =>
        {
            entity.ToTable("LegalActType");
        });

        modelBuilder.Entity<OperatorBasic>(entity =>
        {
            entity.ToTable("OperatorBasic");

            entity.Property(e => e.ApartmentBuildingEntrance)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentBuildingFlat)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentBuildingFloor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentBuildingNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EraseReason).IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LegalEntityName).HasMaxLength(500);
            entity.Property(e => e.LegalEntityUic).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Neighborhood).HasMaxLength(100);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.StreetNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OperatorBasicPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operator__3214EC079C1FD7F2");

            entity.ToTable("OperatorBasicPart");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Entity).WithMany(p => p.OperatorBasicParts)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OperatorB__Entit__6FE99F9F");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.OperatorBasicPart)
                .HasForeignKey<OperatorBasicPart>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OperatorBasi__Id__6E01572D");
        });

        modelBuilder.Entity<OperatorCommit>(entity =>
        {
            entity.ToTable("OperatorCommit");

            entity.Property(e => e.ApplicationCode).HasMaxLength(20);
        });

        modelBuilder.Entity<OperatorCorrespondenceDataPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operator__3214EC070A473880");

            entity.ToTable("OperatorCorrespondenceDataPart");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Entity).WithMany(p => p.OperatorCorrespondenceDataParts)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OperatorC__Entit__778AC167");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.OperatorCorrespondenceDataPart)
                .HasForeignKey<OperatorCorrespondenceDataPart>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OperatorCorr__Id__75A278F5");
        });

        modelBuilder.Entity<OperatorCorrespondenceDatum>(entity =>
        {
            entity.Property(e => e.ApartmentBuildingEntrance)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentBuildingFlat)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentBuildingFloor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApartmentBuildingNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fax)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Neighborhood).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.StreetNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Terrain>(entity =>
        {
            entity.ToTable("Terrain");

            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.ZipCode).HasMaxLength(20);
        });

        modelBuilder.Entity<TerrainPart>(entity =>
        {
            entity.ToTable("TerrainPart");

            entity.HasOne(d => d.Commit).WithMany(p => p.TerrainParts)
                .HasForeignKey(d => d.CommitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TerrainPart_OperatorCommit");

            entity.HasOne(d => d.Entity).WithMany(p => p.TerrainParts)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TerrainPart_Terrain");

            entity.HasOne(d => d.InitialPart).WithMany(p => p.InverseInitialPart)
                .HasForeignKey(d => d.InitialPartId)
                .HasConstraintName("FK_TerrainPart_TerrainPart");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
