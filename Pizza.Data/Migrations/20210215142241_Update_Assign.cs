using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class Update_Assign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tbl_Assign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tbl_Assign",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tbl_Assign");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "tbl_Assign");
        }
    }
}
