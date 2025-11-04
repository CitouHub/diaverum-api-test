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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiaverumItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DiaverumItem_PK");

            entity.ToTable("DiaverumItem");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.EventDate).HasPrecision(0);
            entity.Property(e => e.Text).HasMaxLength(100);
            entity.Property(e => e.TextDetails).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
