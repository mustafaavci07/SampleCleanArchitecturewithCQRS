using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCleanArchitecture.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journey",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    DepartureTimeUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArrivalTimeUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DepartureFrom = table.Column<string>(type: "text", nullable: false),
                    ArrivalTo = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "integer", nullable: false),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NationalIdentityNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    MailAddress = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Situation = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    WorkflowName = table.Column<string>(type: "text", nullable: false),
                    RuleExpression = table.Column<string>(type: "text", nullable: false),
                    RuleName = table.Column<string>(type: "text", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: false),
                    SuccessEvent = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassengerJourney",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    PassengerId = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    JourneyId = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    IsCanceled = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerJourney", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerJourney_Journey_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerJourney_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    PassengerId = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    JourneyId = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    Bank = table.Column<string>(type: "text", nullable: false),
                    CardNo = table.Column<string>(type: "text", nullable: false),
                    CardValidTill = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BankProcessId = table.Column<string>(type: "text", nullable: false),
                    PaymentState = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Journey_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerJourney_JourneyId",
                table: "PassengerJourney",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerJourney_PassengerId",
                table: "PassengerJourney",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_JourneyId",
                table: "Payment",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PassengerId",
                table: "Payment",
                column: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassengerJourney");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Journey");

            migrationBuilder.DropTable(
                name: "Passenger");
        }
    }
}
