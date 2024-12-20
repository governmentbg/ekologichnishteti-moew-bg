using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zopoesht2Migration.DbContexts.Zopoesht2.Models;

namespace Zopoesht2Migration.DbContexts.Zopoesht2;

public partial class Zopoesht2Context : DbContext
{
    public Zopoesht2Context()
    {
    }

    public Zopoesht2Context(DbContextOptions<Zopoesht2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Activityoffering> Activityofferings { get; set; }

    public virtual DbSet<Acttype> Acttypes { get; set; }

    public virtual DbSet<Authority> Authorities { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Municipality> Municipalities { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<Operatorcorrespondence> Operatorcorrespondences { get; set; }

    public virtual DbSet<Settlement> Settlements { get; set; }

    public virtual DbSet<Subactivity> Subactivities { get; set; }

    public virtual DbSet<Terrain> Terrains { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(DbConfigurations.Zopoesht2Context);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");
        });

        modelBuilder.Entity<Activityoffering>(entity =>
        {
            entity.ToTable("activityoffering");

            entity.HasIndex(e => e.Activityid, "IX_activityoffering_activityid");

            entity.HasIndex(e => e.Authoritybasinid, "IX_activityoffering_authoritybasinid");

            entity.HasIndex(e => e.Authorityriosvid, "IX_activityoffering_authorityriosvid");

            entity.HasIndex(e => e.Operatorid, "IX_activityoffering_operatorid");

            entity.HasIndex(e => e.Subactivityid, "IX_activityoffering_subactivityid");

            entity.HasIndex(e => e.Terrainid, "IX_activityoffering_terrainid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activityid).HasColumnName("activityid");
            entity.Property(e => e.Authoritybasinid).HasColumnName("authoritybasinid");
            entity.Property(e => e.Authorityriosvid).HasColumnName("authorityriosvid");
            entity.Property(e => e.Operatorid).HasColumnName("operatorid");
            entity.Property(e => e.Subactivityid).HasColumnName("subactivityid");
            entity.Property(e => e.Terrainid).HasColumnName("terrainid");

            entity.HasOne(d => d.Activity).WithMany(p => p.Activityofferings).HasForeignKey(d => d.Activityid);

            entity.HasOne(d => d.Authoritybasin).WithMany(p => p.ActivityofferingAuthoritybasins).HasForeignKey(d => d.Authoritybasinid);

            entity.HasOne(d => d.Authorityriosv).WithMany(p => p.ActivityofferingAuthorityriosvs).HasForeignKey(d => d.Authorityriosvid);

            entity.HasOne(d => d.Operator).WithMany(p => p.Activityofferings).HasForeignKey(d => d.Operatorid);

            entity.HasOne(d => d.Subactivity).WithMany(p => p.Activityofferings).HasForeignKey(d => d.Subactivityid);

            entity.HasOne(d => d.Terrain).WithMany(p => p.Activityofferings).HasForeignKey(d => d.Terrainid);
        });

        modelBuilder.Entity<Acttype>(entity =>
        {
            entity.ToTable("acttype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");
        });

        modelBuilder.Entity<Authority>(entity =>
        {
            entity.ToTable("authority");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Authoritytype)
                .HasDefaultValue(0)
                .HasColumnName("authoritytype");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Migrationid).HasColumnName("migrationid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("district");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Code2).HasColumnName("code2");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Mainsettlementcode).HasColumnName("mainsettlementcode");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Secondlevelregioncode).HasColumnName("secondlevelregioncode");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");
        });

        modelBuilder.Entity<Municipality>(entity =>
        {
            entity.ToTable("municipality");

            entity.HasIndex(e => e.Districtid, "IX_municipality_districtid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Code2).HasColumnName("code2");
            entity.Property(e => e.Districtid).HasColumnName("districtid");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Mainsettlementcode).HasColumnName("mainsettlementcode");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");

            entity.HasOne(d => d.District).WithMany(p => p.Municipalities)
                .HasForeignKey(d => d.Districtid)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.ToTable("operator");

            entity.HasIndex(e => e.Operatorcorrespondenceid, "IX_operator_operatorcorrespondenceid");

            entity.HasIndex(e => e.Settlementid, "IX_operator_settlementid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Firstname).HasColumnName("firstname");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Legalentityname).HasColumnName("legalentityname");
            entity.Property(e => e.Legalentityuic).HasColumnName("legalentityuic");
            entity.Property(e => e.Managementaddress).HasColumnName("managementaddress");
            entity.Property(e => e.Middlename).HasColumnName("middlename");
            entity.Property(e => e.Migrationid).HasColumnName("migrationid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Operatorcorrespondenceid).HasColumnName("operatorcorrespondenceid");
            entity.Property(e => e.Postalcode).HasColumnName("postalcode");
            entity.Property(e => e.Settlementid).HasColumnName("settlementid");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");

            entity.HasOne(d => d.Operatorcorrespondence).WithMany(p => p.Operators)
                .HasForeignKey(d => d.Operatorcorrespondenceid)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Settlement).WithMany(p => p.Operators).HasForeignKey(d => d.Settlementid);
        });

        modelBuilder.Entity<Operatorcorrespondence>(entity =>
        {
            entity.ToTable("operatorcorrespondence");

            entity.HasIndex(e => e.Settlementid, "IX_operatorcorrespondence_settlementid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contactperson).HasColumnName("contactperson");
            entity.Property(e => e.Correspondenceaddress).HasColumnName("correspondenceaddress");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Fax).HasColumnName("fax");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Postalcode).HasColumnName("postalcode");
            entity.Property(e => e.Settlementid).HasColumnName("settlementid");

            entity.HasOne(d => d.Settlement).WithMany(p => p.Operatorcorrespondences).HasForeignKey(d => d.Settlementid);
        });

        modelBuilder.Entity<Settlement>(entity =>
        {
            entity.ToTable("settlement");

            entity.HasIndex(e => e.Districtid, "IX_settlement_districtid");

            entity.HasIndex(e => e.Municipalityid, "IX_settlement_municipalityid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Altitude).HasColumnName("altitude");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Districtcode).HasColumnName("districtcode");
            entity.Property(e => e.Districtcode2).HasColumnName("districtcode2");
            entity.Property(e => e.Districtid).HasColumnName("districtid");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Isdistrict).HasColumnName("isdistrict");
            entity.Property(e => e.Mayoraltycode).HasColumnName("mayoraltycode");
            entity.Property(e => e.Municipalitycode).HasColumnName("municipalitycode");
            entity.Property(e => e.Municipalitycode2).HasColumnName("municipalitycode2");
            entity.Property(e => e.Municipalityid).HasColumnName("municipalityid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Settlementname).HasColumnName("settlementname");
            entity.Property(e => e.Typecode).HasColumnName("typecode");
            entity.Property(e => e.Typename).HasColumnName("typename");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");

            entity.HasOne(d => d.District).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.Districtid)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Municipality).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.Municipalityid)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Subactivity>(entity =>
        {
            entity.ToTable("subactivity");

            entity.HasIndex(e => e.Activityid, "IX_subactivity_activityid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activityid).HasColumnName("activityid");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");

            entity.HasOne(d => d.Activity).WithMany(p => p.Subactivities)
                .HasForeignKey(d => d.Activityid)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Terrain>(entity =>
        {
            entity.ToTable("terrain");

            entity.HasIndex(e => e.Districtid, "IX_terrain_districtid");

            entity.HasIndex(e => e.Municipalityid, "IX_terrain_municipalityid");

            entity.HasIndex(e => e.Operatorid, "IX_terrain_operatorid");

            entity.HasIndex(e => e.Settlementid, "IX_terrain_settlementid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Alias).HasColumnName("alias");
            entity.Property(e => e.Districtid).HasColumnName("districtid");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(false)
                .HasColumnName("isactive");
            entity.Property(e => e.Migrationid).HasColumnName("migrationid");
            entity.Property(e => e.Municipalityid).HasColumnName("municipalityid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Namealt).HasColumnName("namealt");
            entity.Property(e => e.Operatorid)
                .HasDefaultValue(0)
                .HasColumnName("operatorid");
            entity.Property(e => e.Settlementid).HasColumnName("settlementid");
            entity.Property(e => e.Version)
                .HasDefaultValue(0)
                .HasColumnName("version");
            entity.Property(e => e.Vieworder).HasColumnName("vieworder");

            entity.HasOne(d => d.District).WithMany(p => p.Terrains).HasForeignKey(d => d.Districtid);

            entity.HasOne(d => d.Municipality).WithMany(p => p.Terrains).HasForeignKey(d => d.Municipalityid);

            entity.HasOne(d => d.Operator).WithMany(p => p.Terrains)
                .HasForeignKey(d => d.Operatorid)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Settlement).WithMany(p => p.Terrains).HasForeignKey(d => d.Settlementid);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
