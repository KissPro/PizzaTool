using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class update_tbl_product_ponsize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PONSize",
                table: "tbl_Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PONSize",
                table: "tbl_Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
