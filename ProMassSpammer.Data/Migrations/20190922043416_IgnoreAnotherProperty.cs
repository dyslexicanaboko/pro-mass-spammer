using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMassSpammer.Data.Migrations
{
    public partial class IgnoreAnotherProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MassCommunicationStatus",
                schema: "dbo",
                table: "MassCommunication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MassCommunicationStatus",
                schema: "dbo",
                table: "MassCommunication",
                nullable: false,
                defaultValue: 0);
        }
    }
}
