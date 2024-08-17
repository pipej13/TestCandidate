using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Data;

public class CandidateFactoryContext : DbContext
{

    public CandidateFactoryContext(DbContextOptions<CandidateFactoryContext> options) : base(options)
    {

    }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<CandidateEsperiences> CandidateEsperiences { get; set; }
    public DbSet<Logs> Logs { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.IdCandidate);

            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Candidate>().ToTable("Candidates");

        modelBuilder.Entity<CandidateEsperiences>(entity =>
        {
            entity.HasKey(e => e.IdCandidateExperience);

            entity.Property(e => e.BeginDate).HasColumnType("datetime");
            entity.Property(e => e.Company)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.Job)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Salary).HasColumnType("numeric(10, 2)");

            entity.HasOne(d => d.IdCandidateNavigation).WithMany(p => p.CandidateEsperiences)
                .HasForeignKey(d => d.IdCandidate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateEsperiences_Candidates");
        });

        modelBuilder.Entity<CandidateEsperiences>().ToTable("CandidateEsperiences");

        modelBuilder.Entity<Logs>(entity =>
        {
            entity.HasKey(e => e.IdLog);

            entity.Property(e => e.Error)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Modulo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Logs>().ToTable("Logs");
    }
}
