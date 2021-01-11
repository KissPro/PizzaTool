using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class FirstUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_DropList",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(maxLength: 200, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DropListRemark = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DropList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Issue",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueNo = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(nullable: false),
                    RPN = table.Column<int>(nullable: true),
                    Severity = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    RepeateddSymptom = table.Column<int>(nullable: true),
                    FailureDesc = table.Column<string>(nullable: true),
                    FileAttack = table.Column<int>(nullable: true),
                    NotifiedList = table.Column<string>(maxLength: 500, nullable: true),
                    IssueStatus = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    CurrentStep = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    StepStatus = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    ContainmentAction = table.Column<string>(nullable: true),
                    AnalysisDetail = table.Column<string>(nullable: true),
                    RecommendedAction = table.Column<string>(nullable: true),
                    EscapeCause = table.Column<string>(nullable: true),
                    CAPADetail = table.Column<string>(nullable: true),
                    VerifyNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Issue", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Process",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProcessName = table.Column<string>(maxLength: 100, nullable: false),
                    RefTable = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProcessRemark = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Process", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Approval",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueNo = table.Column<Guid>(nullable: false),
                    ApproverID = table.Column<string>(maxLength: 50, nullable: false),
                    Team = table.Column<string>(maxLength: 100, nullable: true),
                    Action = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ApproverRemark = table.Column<string>(maxLength: 500, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Approval", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_Approval_tbl_Issue",
                        column: x => x.IssueNo,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Assign",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueNo = table.Column<Guid>(nullable: false),
                    CurrentStep = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Team = table.Column<string>(maxLength: 100, nullable: true),
                    OwnerID = table.Column<string>(maxLength: 50, nullable: false),
                    RequestContent = table.Column<string>(nullable: false),
                    ActionResult = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ActionContent = table.Column<string>(nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeadLevel = table.Column<int>(nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Assign", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_Assign_tbl_Issue",
                        column: x => x.IssueNo,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Audit",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueID = table.Column<Guid>(nullable: false),
                    AuditType = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    RelatedCAPA = table.Column<string>(nullable: true),
                    Auditor = table.Column<string>(maxLength: 50, nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Audit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_Audit_tbl_Issue",
                        column: x => x.IssueID,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_File",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueID = table.Column<Guid>(nullable: false),
                    CurrentStep = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(maxLength: 200, nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    UploadedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UploadedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_File", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_File_tbl_Issue",
                        column: x => x.IssueID,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IT",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueID = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CustomerService = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_IT_tbl_Issue",
                        column: x => x.IssueID,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OBA",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueID = table.Column<Guid>(nullable: false),
                    DetectingTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DefectPart = table.Column<string>(maxLength: 100, nullable: true),
                    DefectName = table.Column<string>(maxLength: 100, nullable: false),
                    DefectType = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    SamplingQty = table.Column<int>(nullable: true),
                    NGPhoneOrdinal = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OBA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_OBA_tbl_Issue",
                        column: x => x.IssueID,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Product",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueID = table.Column<Guid>(nullable: false),
                    IMEI = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Custormer = table.Column<string>(maxLength: 50, nullable: false),
                    Product = table.Column<string>(maxLength: 50, nullable: false),
                    PSN = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    PONNo = table.Column<long>(nullable: false),
                    PONSize = table.Column<int>(nullable: false),
                    SPCode = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Line = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Pattern = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Shift = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Product", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_Product_tbl_Issue",
                        column: x => x.IssueID,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ScraptCost",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueID = table.Column<Guid>(nullable: false),
                    PartName = table.Column<string>(maxLength: 100, nullable: true),
                    ScapQty = table.Column<long>(nullable: true),
                    ScapCost = table.Column<decimal>(type: "decimal(18, 4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ScraptCost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_ScraptCost_tbl_Issue",
                        column: x => x.IssueID,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Verification",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    IssueID = table.Column<Guid>(nullable: false),
                    PONNo = table.Column<string>(maxLength: 50, nullable: true),
                    Size = table.Column<long>(nullable: true),
                    NGQty = table.Column<long>(nullable: true),
                    NGRate = table.Column<string>(maxLength: 50, nullable: true),
                    Judgment = table.Column<string>(maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Verification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_Verification_tbl_Issue",
                        column: x => x.IssueID,
                        principalTable: "tbl_Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Approval_IssueNo",
                table: "tbl_Approval",
                column: "IssueNo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Assign_IssueNo",
                table: "tbl_Assign",
                column: "IssueNo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Audit_IssueID",
                table: "tbl_Audit",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_File_IssueID",
                table: "tbl_File",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IT_IssueID",
                table: "tbl_IT",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OBA_IssueID",
                table: "tbl_OBA",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_IssueID",
                table: "tbl_Product",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ScraptCost_IssueID",
                table: "tbl_ScraptCost",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Verification_IssueID",
                table: "tbl_Verification",
                column: "IssueID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Assign");

            migrationBuilder.DropTable(
                name: "tbl_Audit");

            migrationBuilder.DropTable(
                name: "tbl_DropList");

            migrationBuilder.DropTable(
                name: "tbl_File");

            migrationBuilder.DropTable(
                name: "tbl_IT");

            migrationBuilder.DropTable(
                name: "tbl_OBA");

            migrationBuilder.DropTable(
                name: "tbl_Process");

            migrationBuilder.DropTable(
                name: "tbl_Product");

            migrationBuilder.DropTable(
                name: "tbl_ScraptCost");

            migrationBuilder.DropTable(
                name: "tbl_Verification");

            migrationBuilder.DropTable(
                name: "tbl_Issue");
        }
    }
}
