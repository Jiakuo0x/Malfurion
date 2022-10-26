using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Students",
                type: "timestamp(0) with time zone",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(3) with time zone",
                oldPrecision: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Students",
                type: "timestamp(0) with time zone",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(3) with time zone",
                oldPrecision: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Students",
                type: "timestamp(3) with time zone",
                precision: 3,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(0) with time zone",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Students",
                type: "timestamp(3) with time zone",
                precision: 3,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(0) with time zone",
                oldPrecision: 0);
        }
    }
}
