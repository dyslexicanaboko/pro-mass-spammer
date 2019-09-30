using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMassSpammer.Data.Migrations
{
    public partial class dateTime2Zero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "dbo",
                table: "MassCommunication",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "dbo",
                table: "MassCommunication",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "getutcdate()");
        }
    }
}
