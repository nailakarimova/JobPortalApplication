using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationPortal.Models;

public partial class DbJobPortalContext : DbContext
{
    public DbJobPortalContext()
    {
    }

    public DbJobPortalContext(DbContextOptions<DbJobPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAnswer> TAnswers { get; set; }

    public virtual DbSet<TCandidate> TCandidates { get; set; }

    public virtual DbSet<TCandidateAnswer> TCandidateAnswers { get; set; }

    public virtual DbSet<TJob> TJobs { get; set; }

    public virtual DbSet<TQuestion> TQuestions { get; set; }

    public virtual DbSet<TTest> TTests { get; set; }

    public virtual DbSet<TTestResult> TTestResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAnswer>(entity =>
        {
            entity.HasKey(e => e.AId).HasName("PK__Answers__D4825024BBCF003F");

            entity.ToTable("T_ANSWERS");

            entity.Property(e => e.AId).HasColumnName("A_ID");
            entity.Property(e => e.ACreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("A_CREATE_DATE");
            entity.Property(e => e.AIscorrect).HasColumnName("A_ISCORRECT");
            entity.Property(e => e.AQId)
                .HasComment("question id from questions table")
                .HasColumnName("A_Q_ID");
            entity.Property(e => e.AStatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("A_STATUS");
            entity.Property(e => e.AText)
                .HasMaxLength(255)
                .HasColumnName("A_TEXT");
            entity.Property(e => e.AUpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("A_UPDATE_DATE");

            entity.HasOne(d => d.AQ).WithMany(p => p.TAnswers)
                .HasForeignKey(d => d.AQId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Answers__Questio__4222D4EF");
        });

        modelBuilder.Entity<TCandidate>(entity =>
        {
            entity.HasKey(e => e.CId).HasName("PK__Candidat__DF539BFCD06E9F6C");

            entity.ToTable("T_CANDIDATES");

            entity.Property(e => e.CId).HasColumnName("C_ID");
            entity.Property(e => e.CCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("C_CREATE_DATE");
            entity.Property(e => e.CEmail)
                .HasMaxLength(255)
                .HasColumnName("C_EMAIL");
            entity.Property(e => e.CFirstname)
                .HasMaxLength(100)
                .HasColumnName("C_FIRSTNAME");
            entity.Property(e => e.CLastname)
                .HasMaxLength(100)
                .HasColumnName("C_LASTNAME");
            entity.Property(e => e.CPhone)
                .HasMaxLength(20)
                .HasColumnName("C_PHONE");
            entity.Property(e => e.CResumePath)
                .HasMaxLength(1000)
                .HasColumnName("C_RESUME_PATH");
        });

        modelBuilder.Entity<TCandidateAnswer>(entity =>
        {
            entity.HasKey(e => e.CaId).HasName("PK__Candidat__8A624CA064A8E4E9");

            entity.ToTable("T_CANDIDATE_ANSWERS");

            entity.Property(e => e.CaId).HasColumnName("CA_ID");
            entity.Property(e => e.CaAId)
                .HasComment("answer id")
                .HasColumnName("CA_A_ID");
            entity.Property(e => e.CaCId).HasColumnName("CA_C_ID");
            entity.Property(e => e.CaQId)
                .HasComment("question id")
                .HasColumnName("CA_Q_ID");
            entity.Property(e => e.CaRId)
                .HasComment("result id ")
                .HasColumnName("CA_R_ID");

            entity.HasOne(d => d.CaA).WithMany(p => p.TCandidateAnswers)
                .HasForeignKey(d => d.CaAId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidate__Answe__4BAC3F29");

            entity.HasOne(d => d.CaC).WithMany(p => p.TCandidateAnswers)
                .HasForeignKey(d => d.CaCId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_CANDIDATE_ANSWERS_T_CANDIDATES");

            entity.HasOne(d => d.CaQ).WithMany(p => p.TCandidateAnswers)
                .HasForeignKey(d => d.CaQId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidate__Quest__4AB81AF0");

            entity.HasOne(d => d.CaR).WithMany(p => p.TCandidateAnswers)
                .HasForeignKey(d => d.CaRId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidate__Resul__49C3F6B7");
        });

        modelBuilder.Entity<TJob>(entity =>
        {
            entity.HasKey(e => e.JId);

            entity.ToTable("T_JOBS");

            entity.Property(e => e.JId).HasColumnName("J_ID");
            entity.Property(e => e.JCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("J_CREATE_DATE");
            entity.Property(e => e.JQuestionQty).HasColumnName("J_QUESTION_QTY");
            entity.Property(e => e.JRequirements).HasColumnName("J_REQUIREMENTS");
            entity.Property(e => e.JStatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("J_STATUS");
            entity.Property(e => e.JTId)
                .HasComment("test id from test table ")
                .HasColumnName("J_T_ID");
            entity.Property(e => e.JTitle)
                .HasMaxLength(200)
                .HasColumnName("J_TITLE");
            entity.Property(e => e.JUpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("J_UPDATE_DATE");

            entity.HasOne(d => d.JT).WithMany(p => p.TJobs)
                .HasForeignKey(d => d.JTId)
                .HasConstraintName("FK_T_JOBS_T_TEST");
        });

        modelBuilder.Entity<TQuestion>(entity =>
        {
            entity.HasKey(e => e.QId).HasName("PK__Question__0DC06F8CFBBE2DF6");

            entity.ToTable("T_QUESTIONS");

            entity.Property(e => e.QId).HasColumnName("Q_ID");
            entity.Property(e => e.QCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Q_CREATE_DATE");
            entity.Property(e => e.QStatus)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Q_STATUS");
            entity.Property(e => e.QTId)
                .HasComment("test id from tests table")
                .HasColumnName("Q_T_ID");
            entity.Property(e => e.QText)
                .HasMaxLength(500)
                .HasColumnName("Q_TEXT");
            entity.Property(e => e.QUpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Q_UPDATE_DATE");

            entity.HasOne(d => d.QT).WithMany(p => p.TQuestions)
                .HasForeignKey(d => d.QTId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__TestI__3F466844");
        });

        modelBuilder.Entity<TTest>(entity =>
        {
            entity.HasKey(e => e.TId).HasName("PK__Tests__8CC33100A16E5C7D");

            entity.ToTable("T_TEST");

            entity.Property(e => e.TId).HasColumnName("T_ID");
            entity.Property(e => e.TCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("T_CREATE_DATE");
            entity.Property(e => e.TDescription)
                .HasMaxLength(500)
                .HasColumnName("T_DESCRIPTION");
            entity.Property(e => e.TName)
                .HasMaxLength(255)
                .HasColumnName("T_NAME");
            entity.Property(e => e.TStastus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("T_STASTUS");
            entity.Property(e => e.TUpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("T_UPDATE_DATE");
        });

        modelBuilder.Entity<TTestResult>(entity =>
        {
            entity.HasKey(e => e.TrId).HasName("PK__TestResu__976902284B2C469A");

            entity.ToTable("T_TEST_RESULTS");

            entity.Property(e => e.TrId).HasColumnName("TR_ID");
            entity.Property(e => e.TrCId).HasColumnName("TR_C_ID");
            entity.Property(e => e.TrCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TR_CREATE_DATE");
            entity.Property(e => e.TrScore)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("TR_SCORE");
            entity.Property(e => e.TrStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("((1))")
                .HasColumnName("TR_STATUS");
            entity.Property(e => e.TrTId)
                .HasComment("Test id")
                .HasColumnName("TR_T_ID");
            entity.Property(e => e.TrTakenDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TR_TAKEN_DATE");

            entity.HasOne(d => d.TrC).WithMany(p => p.TTestResults)
                .HasForeignKey(d => d.TrCId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestResul__Candi__45F365D3");

            entity.HasOne(d => d.TrT).WithMany(p => p.TTestResults)
                .HasForeignKey(d => d.TrTId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestResul__TestI__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
