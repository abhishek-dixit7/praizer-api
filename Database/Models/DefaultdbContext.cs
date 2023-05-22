﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace praizer_api.Database.Models;

public partial class DefaultdbContext : DbContext
{
    private readonly IConfiguration configuration;
    public DefaultdbContext()
    {
    }

    public DefaultdbContext(DbContextOptions<DefaultdbContext> options, IConfiguration _configurations)
        : base(options)
    {
        configuration = _configurations;
    }


    public virtual DbSet<Praise> Praises { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Praise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("praises_pkey");

            entity.ToTable("praises");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_on");
            entity.Property(e => e.PraizerId).HasColumnName("praizer_id");
            entity.Property(e => e.UserPraisedId).HasColumnName("user_praised_id");

            entity.HasOne(d => d.Praizer).WithMany(p => p.PraisePraizers)
                .HasForeignKey(d => d.PraizerId)
                .HasConstraintName("praises_praizer_id_fkey");

            entity.HasOne(d => d.UserPraised).WithMany(p => p.PraiseUserPraiseds)
                .HasForeignKey(d => d.UserPraisedId)
                .HasConstraintName("praises_user_praised_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_on");
            entity.Property(e => e.DateOfJoining).HasColumnName("date_of_joining");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.ModifedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifed_on");
            entity.Property(e => e.PointBalance).HasColumnName("point_balance");
            entity.Property(e => e.PointToAward)
                .HasDefaultValueSql("100")
                .HasColumnName("point_to_award");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
