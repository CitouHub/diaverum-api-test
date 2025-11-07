using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Diaverum.Data;

public partial class DiaverumDbContext : DbContext
{
    public DiaverumDbContext(DbContextOptions<DiaverumDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DiaverumItem> DiaverumItems { get; set; }

    public virtual DbSet<LabResult> LabResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiaverumItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DiaverumItem_PK");

            entity.ToTable("DiaverumItem");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.EventDate).HasPrecision(0);
            entity.Property(e => e.Text).HasMaxLength(100);
            entity.Property(e => e.TextDetails).HasMaxLength(500);
        });

        modelBuilder.Entity<LabResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LabResult_PK");

            entity.ToTable("LabResult");

            entity.HasIndex(e => e.ClinicNo, "IdxLabResult_ClinicNo");

            entity.HasIndex(e => e.PatientId, "IdxLabResult_PatientId");

            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.ClinicNo).HasMaxLength(100);
            entity.Property(e => e.CollentionTime).HasPrecision(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Dbo).HasColumnName("DBO");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.NonSpecRefs).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.PatientName).HasMaxLength(100);
            entity.Property(e => e.RefrangeHigh).HasMaxLength(100);
            entity.Property(e => e.RefrangeLow).HasMaxLength(100);
            entity.Property(e => e.Result).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.TestCode).HasMaxLength(100);
            entity.Property(e => e.TestName).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
