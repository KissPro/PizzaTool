using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class Update_TblProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproverId_Lv1",
                table: "tbl_Process",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApproverId_Lv2",
                table: "tbl_Process",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverId_Lv1",
                table: "tbl_Process");

            migrationBuilder.DropColumn(
                name: "ApproverId_Lv2",
                table: "tbl_Process");
        }
    }
}
