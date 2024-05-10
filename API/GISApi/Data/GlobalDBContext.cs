using System;
using System.Collections.Generic;
using GISApi.Data.GlobalEntities;
using Microsoft.EntityFrameworkCore;

namespace GISApi.Data;

public partial class GlobalDBContext : DbContext
{
    public GlobalDBContext()
    {
    }

    public GlobalDBContext(DbContextOptions<GlobalDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<HolidayMaster> HolidayMasters { get; set; }

    public virtual DbSet<Inspection> Inspections { get; set; }

    public virtual DbSet<InspectionNumberGenerator> InspectionNumberGenerators { get; set; }

    public virtual DbSet<InspectionType> InspectionTypes { get; set; }

    public virtual DbSet<InspectionsWithRejectedAttrView> InspectionsWithRejectedAttrViews { get; set; }

    public virtual DbSet<TblAttribute> TblAttributes { get; set; }

    public virtual DbSet<TblEmailRequest> TblEmailRequests { get; set; }

    public virtual DbSet<TblInspection> TblInspections { get; set; }

    public virtual DbSet<TblInspectionAttribute> TblInspectionAttributes { get; set; }

    public virtual DbSet<TblInspectionAttribute1> TblInspectionAttributes1 { get; set; }

    public virtual DbSet<TblInspectionType> TblInspectionTypes { get; set; }

    public virtual DbSet<TblLoginInfo> TblLoginInfos { get; set; }

    public virtual DbSet<TblResult> TblResults { get; set; }

    public virtual DbSet<UserResetPassword> UserResetPasswords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
       .AddJsonFile("appsettings.json")
       .Build();

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InspectionNumberGenerator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InspecctionNumberGenerator");
        });

        modelBuilder.Entity<InspectionsWithRejectedAttrView>(entity =>
        {
            entity.ToView("InspectionsWithRejectedAttr_View");
        });

        modelBuilder.Entity<TblEmailRequest>(entity =>
        {
            entity.Property(e => e.AttemptNumber).HasDefaultValueSql("((0))");
            entity.Property(e => e.LastAttemptDateTime).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblInspection>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Result).WithMany(p => p.TblInspections).HasConstraintName("FK_tblInspection_tblResult");

            entity.HasOne(d => d.User).WithMany(p => p.TblInspections).HasConstraintName("FK_tblInspection_tblLoginInfo");
        });

        modelBuilder.Entity<TblInspectionAttribute>(entity =>
        {
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InsAtrId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblInspectionAttribute1>(entity =>
        {
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Attr).WithMany(p => p.TblInspectionAttribute1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblInspectionAttributes_tblAttributes");
        });

        modelBuilder.Entity<TblInspectionType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<TblLoginInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_LoginInfo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
