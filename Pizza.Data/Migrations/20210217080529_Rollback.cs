using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class Rollback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "tbl_Assign");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionDate",
                table: "tbl_Assign",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
