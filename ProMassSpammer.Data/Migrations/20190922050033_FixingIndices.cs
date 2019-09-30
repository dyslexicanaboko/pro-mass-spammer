using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMassSpammer.Data.Migrations
{
    public partial class FixingIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipient_TransmissionStatusId",
                schema: "dbo",
                table: "Recipient");

            migrationBuilder.DropIndex(
                name: "IX_MassCommunication_DeliveryMethodId",
                schema: "dbo",
                table: "MassCommunication");

            migrationBuilder.DropIndex(
                name: "IX_MassCommunication_MassCommunicationStatusId",
                schema: "dbo",
                table: "MassCommunication");

            migrationBuilder.RenameIndex(
                name: "IX_Recipient_MassCommunicationId",
                schema: "dbo",
                table: "Recipient",
                newName: "IX_dbo.Recipient_MassCommunicationId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo.Recipient_TransmissionStatusId",
                schema: "dbo",
                table: "Recipient",
                column: "TransmissionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo.MassCommunication_DeliveryMethodId",
                schema: "dbo",
                table: "MassCommunication",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo.MassCommunication_MassCommunicationStatusId",
                schema: "dbo",
                table: "MassCommunication",
                column: "MassCommunicationStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_dbo.Recipient_TransmissionStatusId",
                schema: "dbo",
                table: "Recipient");

            migrationBuilder.DropIndex(
                name: "IX_dbo.MassCommunication_DeliveryMethodId",
                schema: "dbo",
                table: "MassCommunication");

            migrationBuilder.DropIndex(
                name: "IX_dbo.MassCommunication_MassCommunicationStatusId",
                schema: "dbo",
                table: "MassCommunication");

            migrationBuilder.RenameIndex(
                name: "IX_dbo.Recipient_MassCommunicationId",
                schema: "dbo",
                table: "Recipient",
                newName: "IX_Recipient_MassCommunicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_TransmissionStatusId",
                schema: "dbo",
                table: "Recipient",
                column: "TransmissionStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MassCommunication_DeliveryMethodId",
                schema: "dbo",
                table: "MassCommunication",
                column: "DeliveryMethodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MassCommunication_MassCommunicationStatusId",
                schema: "dbo",
                table: "MassCommunication",
                column: "MassCommunicationStatusId",
                unique: true);
        }
    }
}
