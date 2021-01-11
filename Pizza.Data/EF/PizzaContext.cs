using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pizza.Data.EF
{
    public partial class PizzaContext : DbContext
    {
        public PizzaContext()
        {
        }

        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblApproval> TblApproval { get; set; }
        public virtual DbSet<TblAssign> TblAssign { get; set; }
        public virtual DbSet<TblAudit> TblAudit { get; set; }
        public virtual DbSet<TblDropList> TblDropList { get; set; }
        public virtual DbSet<TblFile> TblFile { get; set; }
        public virtual DbSet<TblIssue> TblIssue { get; set; }
        public virtual DbSet<TblIt> TblIt { get; set; }
        public virtual DbSet<TblOba> TblOba { get; set; }
        public virtual DbSet<TblProcess> TblProcess { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblScraptCost> TblScraptCost { get; set; }
        public virtual DbSet<TblVerification> TblVerification { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=HVNN0606\\SQLEXPRESS;initial catalog=Pizza;user id=sa;password=123;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblApproval>(entity =>
            {
                entity.ToTable("tbl_Approval");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Action)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverId)
                    .IsRequired()
                    .HasColumnName("ApproverID")
                    .HasMaxLength(50);

                entity.Property(e => e.ApproverRemark).HasMaxLength(500);

                entity.Property(e => e.Team).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IssueNoNavigation)
                    .WithMany(p => p.TblApproval)
                    .HasForeignKey(d => d.IssueNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Approval_tbl_Issue");
            });

            modelBuilder.Entity<TblAssign>(entity =>
            {
                entity.ToTable("tbl_Assign");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionResult)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentStep)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeadLine).HasColumnType("datetime");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasColumnName("OwnerID")
                    .HasMaxLength(50);

                entity.Property(e => e.RequestContent).IsRequired();

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Team).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IssueNoNavigation)
                    .WithMany(p => p.TblAssign)
                    .HasForeignKey(d => d.IssueNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Assign_tbl_Issue");
            });

            modelBuilder.Entity<TblAudit>(entity =>
            {
                entity.ToTable("tbl_Audit");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuditType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Auditor).HasMaxLength(50);

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.RelatedCapa).HasColumnName("RelatedCAPA");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TblAudit)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Audit_tbl_Issue");
            });

            modelBuilder.Entity<TblDropList>(entity =>
            {
                entity.ToTable("tbl_DropList");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DropListRemark).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TblFile>(entity =>
            {
                entity.ToTable("tbl_File");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CurrentStep)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UploadedBy).HasMaxLength(50);

                entity.Property(e => e.UploadedDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TblFile)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_File_tbl_Issue");
            });

            modelBuilder.Entity<TblIssue>(entity =>
            {
                entity.ToTable("tbl_Issue");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Capadetail).HasColumnName("CAPADetail");

                entity.Property(e => e.CurrentStep)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessType)
                  .IsRequired()
                  .HasMaxLength(50);

                entity.Property(e => e.IssueNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IssueStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NotifiedList).HasMaxLength(500);

                entity.Property(e => e.Rpn).HasColumnName("RPN");

                entity.Property(e => e.Severity)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StepStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<TblIt>(entity =>
            {
                entity.ToTable("tbl_IT");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerService).HasMaxLength(1);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TblIt)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_IT_tbl_Issue");
            });

            modelBuilder.Entity<TblOba>(entity =>
            {
                entity.ToTable("tbl_OBA");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DefectName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DefectPart).HasMaxLength(100);

                entity.Property(e => e.DefectType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DetectingTime).HasColumnType("datetime");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.NgphoneOrdinal)
                    .HasColumnName("NGPhoneOrdinal")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TblOba)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_OBA_tbl_Issue");
            });

            modelBuilder.Entity<TblProcess>(entity =>
            {
                entity.ToTable("tbl_Process");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProcessName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProcessRemark).HasMaxLength(100);

                entity.Property(e => e.RefTable)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.ToTable("tbl_Product");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Custormer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasColumnName("IMEI")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pattern)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ponno).HasColumnName("PONNo");

                entity.Property(e => e.Ponsize).HasColumnName("PONSize");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Psn)
                    .IsRequired()
                    .HasColumnName("PSN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Shift)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Spcode)
                    .IsRequired()
                    .HasColumnName("SPCode")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TblProduct)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Product_tbl_Issue");
            });

            modelBuilder.Entity<TblScraptCost>(entity =>
            {
                entity.ToTable("tbl_ScraptCost");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.PartName).HasMaxLength(100);

                entity.Property(e => e.ScapCost).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TblScraptCost)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ScraptCost_tbl_Issue");
            });

            modelBuilder.Entity<TblVerification>(entity =>
            {
                entity.ToTable("tbl_Verification");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.Judgment).HasMaxLength(500);

                entity.Property(e => e.Ngqty).HasColumnName("NGQty");

                entity.Property(e => e.Ngrate)
                    .HasColumnName("NGRate")
                    .HasMaxLength(50);

                entity.Property(e => e.Ponno)
                    .HasColumnName("PONNo")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TblVerification)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Verification_tbl_Issue");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
