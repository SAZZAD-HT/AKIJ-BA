﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Samurai_V2_.Net_8.DbContexts.Models;

namespace Samurai_V2_.Net_8.DbContexts;

public partial class ReadWriteContext : DbContext
{
    public ReadWriteContext()
    {
    }

    public ReadWriteContext(DbContextOptions<ReadWriteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-E4E9TJI;Initial Catalog=AKIJ;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Integrated Security=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.Mobile).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
