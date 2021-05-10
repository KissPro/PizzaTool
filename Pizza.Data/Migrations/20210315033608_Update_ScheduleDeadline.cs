using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class Update_ScheduleDeadline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScheduleDeadLine",
                table: "tbl_Assign",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleDeadLine",
                table: "tbl_Assign");
        }
    }
}
