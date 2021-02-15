using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class Add_ExtendDeadLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ExtendDeadLine",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AssignNo = table.Column<Guid>(nullable: false),
                    CurrentDeadLine = table.Column<DateTime>(type: "datetime", nullable: false),
                    RequestDeadLine = table.Column<DateTime>(type: "datetime", nullable: false),
                    Reason = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    ApprovalContent = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Status = table.Column<string>(nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ExtendDeadLine", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ExtendDeadLine");
        }
    }
}
