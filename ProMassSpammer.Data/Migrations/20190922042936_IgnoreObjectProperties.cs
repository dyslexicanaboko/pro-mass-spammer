using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMassSpammer.Data.Migrations
{
    public partial class IgnoreObjectProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransmissionStatus",
                schema: "dbo",
                table: "Recipient");

            migrationBuilder.DropColumn(
                name: "DeliveryMethod",
                schema: "dbo",
                table: "MassCommunication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransmissionStatus",
                schema: "dbo",
                table: "Recipient",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethod",
                schema: "dbo",
                table: "MassCommunication",
                nullable: false,
                defaultValue: 0);
        }
    }
}
