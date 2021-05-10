using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class update_tblissue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateByName",
                table: "tbl_Issue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateByName",
                table: "tbl_Issue");
        }
    }
}
