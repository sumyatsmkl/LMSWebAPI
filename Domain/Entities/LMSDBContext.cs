using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Domain.Entities
{
    public partial class LMSDBContext : DbContext
    {
        public LMSDBContext()
        {
        }

        public LMSDBContext(DbContextOptions<LMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TProfile> TProfiles { get; set; } = null!;
        public virtual DbSet<TProfileAccount> TProfileAccounts { get; set; } = null!;
        public virtual DbSet<TProfileEducation> TProfileEducations { get; set; } = null!;
        public virtual DbSet<TProfileRole> TProfileRoles { get; set; } = null!;
        public virtual DbSet<TRole> TRoles { get; set; } = null!;
        public virtual DbSet<TSystemCodeValue> TSystemCodeValues { get; set; } = null!;
        public virtual DbSet<TSystemCurrency> TSystemCurrencies { get; set; } = null!;
        public virtual DbSet<TTenantMain> TTenantMains { get; set; } = null!;
        public virtual DbSet<TTenantSub> TTenantSubs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TProfile>(entity =>
            {
                entity.HasKey(e => new { e.ProfileId, e.TenantSubId });

                entity.ToTable("T_Profile");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.TenantSubId).HasColumnName("TenantSubID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Idno)
                    .HasMaxLength(50)
                    .HasColumnName("IDNo");

                entity.Property(e => e.IdtypeId).HasColumnName("IDTypeID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.PhoneNo).HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.SalutationTitleId).HasColumnName("SalutationTitleID");
            });

            modelBuilder.Entity<TProfileAccount>(entity =>
            {
                entity.HasKey(e => new { e.ProfileAccountId, e.TenantSubId });

                entity.ToTable("T_Profile_Account");

                entity.Property(e => e.ProfileAccountId).HasColumnName("ProfileAccountID");

                entity.Property(e => e.TenantSubId).HasColumnName("TenantSubID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.ReactivationDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.TProfile)
                    .WithMany(p => p.TProfileAccounts)
                    .HasForeignKey(d => new { d.ProfileId, d.TenantSubId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Profile_Account_T_Profile");
            });

            modelBuilder.Entity<TProfileEducation>(entity =>
            {
                entity.HasKey(e => new { e.EducationId, e.TenantSubId });

                entity.ToTable("T_Profile_Education");

                entity.Property(e => e.EducationId).HasColumnName("EducationID");

                entity.Property(e => e.TenantSubId).HasColumnName("TenantSubID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Institution).HasMaxLength(500);

                entity.Property(e => e.LanguageProficiencyId).HasColumnName("LanguageProficiencyID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.QualificationLevelId).HasColumnName("QualificationLevelID");

                entity.Property(e => e.QualificationName).HasMaxLength(500);

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.SchoolEndDate).HasColumnType("datetime");

                entity.Property(e => e.SchoolStartDate).HasColumnType("datetime");

                entity.Property(e => e.YearObtained).HasColumnType("datetime");

                entity.HasOne(d => d.TProfile)
                    .WithMany(p => p.TProfileEducations)
                    .HasForeignKey(d => new { d.ProfileId, d.TenantSubId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Profile_Education_T_Profile");
            });

            modelBuilder.Entity<TProfileRole>(entity =>
            {
                entity.HasKey(e => new { e.ProfileRoleId, e.TenantSubId });

                entity.ToTable("T_Profile_Role");

                entity.Property(e => e.ProfileRoleId).HasColumnName("ProfileRoleID");

                entity.Property(e => e.TenantSubId).HasColumnName("TenantSubID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.TRole)
                    .WithMany(p => p.TProfileRoles)
                    .HasForeignKey(d => new { d.RoleId, d.TenantSubId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Profile_Role_T_Role");
            });

            modelBuilder.Entity<TRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.TenantSubId });

                entity.ToTable("T_Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.TenantSubId).HasColumnName("TenantSubID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(200);

                entity.Property(e => e.RoleCode).HasMaxLength(20);

                entity.Property(e => e.RoleName).HasMaxLength(100);

                entity.HasOne(d => d.TenantSub)
                    .WithMany(p => p.TRoles)
                    .HasForeignKey(d => d.TenantSubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Role_T_Tenant_Sub");
            });

            modelBuilder.Entity<TSystemCodeValue>(entity =>
            {
                entity.HasKey(e => e.CodeValueId);

                entity.ToTable("T_System_Code_Value");

                entity.Property(e => e.CodeValueId)
                    .ValueGeneratedNever()
                    .HasColumnName("CodeValueID");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CodeTypeId).HasColumnName("CodeTypeID");

                entity.Property(e => e.CodeValue).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.TenantSubId).HasColumnName("TenantSubID");

                entity.HasOne(d => d.TenantSub)
                    .WithMany(p => p.TSystemCodeValues)
                    .HasForeignKey(d => d.TenantSubId)
                    .HasConstraintName("FK_T_System_Code_Value_T_Tenant_Sub");
            });

            modelBuilder.Entity<TSystemCurrency>(entity =>
            {
                entity.HasKey(e => new { e.CurrencyId, e.TenantSubId });

                entity.ToTable("T_System_Currency");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.TenantSubId).HasColumnName("TenantSubID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyName).HasMaxLength(50);

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TTenantMain>(entity =>
            {
                entity.HasKey(e => e.TenantMainId);

                entity.ToTable("T_Tenant_Main");

                entity.Property(e => e.TenantMainId)
                    .ValueGeneratedNever()
                    .HasColumnName("TenantMainID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.TenantMainName).HasMaxLength(200);
            });

            modelBuilder.Entity<TTenantSub>(entity =>
            {
                entity.HasKey(e => e.TenantSubId);

                entity.ToTable("T_Tenant_Sub");

                entity.Property(e => e.TenantSubId)
                    .ValueGeneratedNever()
                    .HasColumnName("TenantSubID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FaxNo).HasMaxLength(50);

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.MaxActiveUserCount).HasDefaultValueSql("((50))");

                entity.Property(e => e.TelNo).HasMaxLength(50);

                entity.Property(e => e.TenantCode).HasMaxLength(100);

                entity.Property(e => e.TenantMainId).HasColumnName("TenantMainID");

                entity.Property(e => e.TenantName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Website).HasMaxLength(300);

                entity.HasOne(d => d.TenantMain)
                    .WithMany(p => p.TTenantSubs)
                    .HasForeignKey(d => d.TenantMainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Tenant_Sub_T_Tenant_Main");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
