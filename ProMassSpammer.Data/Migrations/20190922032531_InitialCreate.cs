using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMassSpammer.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "DeliveryMethod",
                schema: "dbo",
                columns: table => new
                {
                    DeliveryMethodId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "1"),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.DeliveryMethod_DeliveryMethodId", x => x.DeliveryMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                schema: "dbo",
                columns: table => new
                {
                    LogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MachineName = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Level = table.Column<string>(maxLength: 5, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Logger = table.Column<string>(maxLength: 300, nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    Callsite = table.Column<string>(maxLength: 300, nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Log_LogId", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "MassCommunicationStatus",
                schema: "dbo",
                columns: table => new
                {
                    MassCommunicationStatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.MassCommunicationStatus_MassCommunicationStatusId", x => x.MassCommunicationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "TransmissionStatus",
                schema: "dbo",
                columns: table => new
                {
                    TransmissionStatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TransmissionStatus_TransmissionStatusId", x => x.TransmissionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "MassCommunication",
                schema: "dbo",
                columns: table => new
                {
                    MassCommunicationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MassCommunicationStatusId = table.Column<int>(nullable: false),
                    MassCommunicationStatus = table.Column<int>(nullable: false),
                    StatusMessage = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Catalyst = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    DeliveryMethodId = table.Column<int>(nullable: false),
                    DeliveryMethod = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(unicode: false, maxLength: 78, nullable: false),
                    Body = table.Column<string>(nullable: false),
                    From = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.MassCommunication_MassCommunicationId", x => x.MassCommunicationId);
                    table.ForeignKey(
                        name: "FK_dbo.MassCommunication_dbo.DeliveryMethod_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalSchema: "dbo",
                        principalTable: "DeliveryMethod",
                        principalColumn: "DeliveryMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.MassCommunication_dbo.MassCommunicationStatus_MassCommunicationStatusId",
                        column: x => x.MassCommunicationStatusId,
                        principalSchema: "dbo",
                        principalTable: "MassCommunicationStatus",
                        principalColumn: "MassCommunicationStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                schema: "dbo",
                columns: table => new
                {
                    RecipientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MassCommunicationId = table.Column<int>(nullable: false),
                    TransmissionStatusId = table.Column<int>(nullable: false),
                    TransmissionStatus = table.Column<int>(nullable: false),
                    TransmissionStatusMessage = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    ContactString = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Recipient_RecipientId", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_dbo.Recipient_dbo.MassCommunication_MassCommunicationId",
                        column: x => x.MassCommunicationId,
                        principalSchema: "dbo",
                        principalTable: "MassCommunication",
                        principalColumn: "MassCommunicationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Recipient_dbo.TransmissionStatus_TransmissionStatusId",
                        column: x => x.TransmissionStatusId,
                        principalSchema: "dbo",
                        principalTable: "TransmissionStatus",
                        principalColumn: "TransmissionStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_MassCommunicationId",
                schema: "dbo",
                table: "Recipient",
                column: "MassCommunicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_TransmissionStatusId",
                schema: "dbo",
                table: "Recipient",
                column: "TransmissionStatusId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Recipient",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MassCommunication",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransmissionStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DeliveryMethod",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MassCommunicationStatus",
                schema: "dbo");
        }
    }
}
