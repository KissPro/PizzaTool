using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza.Data.Migrations
{
    public partial class Update_Issue_OBA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepeateddSymptom",
                table: "tbl_Issue");

            migrationBuilder.DropColumn(
                name: "RPN",
                table: "tbl_Issue");

            migrationBuilder.AddColumn<string>(
                name: "Auditor",
                table: "tbl_OBA",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "tbl_OBA",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectBy",
                table: "tbl_OBA",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FailureValidate",
                table: "tbl_OBA",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowToDetect",
                table: "tbl_OBA",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "tbl_OBA",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "tbl_OBA",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CARNo",
                table: "tbl_Issue",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepeatedCause",
                table: "tbl_Issue",
                unicode: false,
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepeatedSymptom",
                table: "tbl_Issue",
                unicode: false,
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auditor",
                table: "tbl_OBA");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "tbl_OBA");

            migrationBuilder.DropColumn(
                name: "DetectBy",
                table: "tbl_OBA");

            migrationBuilder.DropColumn(
                name: "FailureValidate",
                table: "tbl_OBA");

            migrationBuilder.DropColumn(
                name: "HowToDetect",
                table: "tbl_OBA");

            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "tbl_OBA");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "tbl_OBA");

            migrationBuilder.DropColumn(
                name: "CARNo",
                table: "tbl_Issue");

            migrationBuilder.DropColumn(
                name: "RepeatedCause",
                table: "tbl_Issue");

            migrationBuilder.DropColumn(
                name: "RepeatedSymptom",
                table: "tbl_Issue");

            migrationBuilder.AddColumn<int>(
                name: "RepeateddSymptom",
                table: "tbl_Issue",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RPN",
                table: "tbl_Issue",
                type: "int",
                nullable: true);
        }
    }
}
