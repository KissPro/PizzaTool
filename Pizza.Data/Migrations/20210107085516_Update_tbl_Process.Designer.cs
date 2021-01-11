﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pizza.Data.EF;

namespace Pizza.Data.Migrations
{
    [DbContext(typeof(PizzaContext))]
    [Migration("20210107085516_Update_tbl_Process")]
    partial class Update_tbl_Process
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pizza.Data.EF.TblApproval", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("ApproverId")
                        .IsRequired()
                        .HasColumnName("ApproverID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ApproverRemark")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<Guid>("IssueNo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Team")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("IssueNo");

                    b.ToTable("tbl_Approval");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblAssign", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActionResult")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CurrentStep")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int?>("DeadLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IssueNo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnName("OwnerID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Team")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("IssueNo");

                    b.ToTable("tbl_Assign");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblAudit", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuditType")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Auditor")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RelatedCapa")
                        .HasColumnName("RelatedCAPA")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("tbl_Audit");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblDropList", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DropListRemark")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("tbl_DropList");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblFile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurrentStep")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("UploadedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UploadedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("tbl_File");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblIssue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnalysisDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Capadetail")
                        .HasColumnName("CAPADetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContainmentAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentStep")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("EscapeCause")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FailureDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FileAttack")
                        .HasColumnType("int");

                    b.Property<string>("IssueNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("IssueStatus")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("NotifiedList")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("RecommendedAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RepeateddSymptom")
                        .HasColumnType("int");

                    b.Property<int?>("Rpn")
                        .HasColumnName("RPN")
                        .HasColumnType("int");

                    b.Property<string>("Severity")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("StepStatus")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerifyNote")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_Issue");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblIt", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerService")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("tbl_IT");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblOba", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DefectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DefectPart")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DefectType")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<DateTime>("DetectingTime")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NgphoneOrdinal")
                        .HasColumnName("NGPhoneOrdinal")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("SamplingQty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("tbl_OBA");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblProcess", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProcessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ProcessRemark")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("RefTable")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("tbl_Process");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Custormer")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Imei")
                        .IsRequired()
                        .HasColumnName("IMEI")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Line")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Pattern")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<long>("Ponno")
                        .HasColumnName("PONNo")
                        .HasColumnType("bigint");

                    b.Property<int>("Ponsize")
                        .HasColumnName("PONSize")
                        .HasColumnType("int");

                    b.Property<string>("Product")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Psn")
                        .IsRequired()
                        .HasColumnName("PSN")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Shift")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Spcode")
                        .IsRequired()
                        .HasColumnName("SPCode")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("tbl_Product");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblScraptCost", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PartName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal?>("ScapCost")
                        .HasColumnType("decimal(18, 4)");

                    b.Property<long?>("ScapQty")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("tbl_ScraptCost");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblVerification", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Judgment")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<long?>("Ngqty")
                        .HasColumnName("NGQty")
                        .HasColumnType("bigint");

                    b.Property<string>("Ngrate")
                        .HasColumnName("NGRate")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Ponno")
                        .HasColumnName("PONNo")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<long?>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("tbl_Verification");
                });

            modelBuilder.Entity("Pizza.Data.EF.TblApproval", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "IssueNoNavigation")
                        .WithMany("TblApproval")
                        .HasForeignKey("IssueNo")
                        .HasConstraintName("FK_tbl_Approval_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblAssign", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "IssueNoNavigation")
                        .WithMany("TblAssign")
                        .HasForeignKey("IssueNo")
                        .HasConstraintName("FK_tbl_Assign_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblAudit", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "Issue")
                        .WithMany("TblAudit")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_tbl_Audit_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblFile", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "Issue")
                        .WithMany("TblFile")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_tbl_File_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblIt", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "Issue")
                        .WithMany("TblIt")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_tbl_IT_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblOba", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "Issue")
                        .WithMany("TblOba")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_tbl_OBA_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblProduct", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "Issue")
                        .WithMany("TblProduct")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_tbl_Product_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblScraptCost", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "Issue")
                        .WithMany("TblScraptCost")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_tbl_ScraptCost_tbl_Issue")
                        .IsRequired();
                });

            modelBuilder.Entity("Pizza.Data.EF.TblVerification", b =>
                {
                    b.HasOne("Pizza.Data.EF.TblIssue", "Issue")
                        .WithMany("TblVerification")
                        .HasForeignKey("IssueId")
                        .HasConstraintName("FK_tbl_Verification_tbl_Issue")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
