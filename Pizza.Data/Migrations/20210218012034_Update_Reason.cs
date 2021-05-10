using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class Update_Reason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "tbl_ExtendDeadLine",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalContent",
                table: "tbl_ExtendDeadLine",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "tbl_ExtendDeadLine",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalContent",
                table: "tbl_ExtendDeadLine",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
