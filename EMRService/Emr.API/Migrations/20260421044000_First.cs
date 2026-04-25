using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emr.API.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMRs",
                columns: table => new
                {
                    EMRID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMRs", x => x.EMRID);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EMRID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_EMRs_EMRID",
                        column: x => x.EMRID,
                        principalTable: "EMRs",
                        principalColumn: "EMRID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentLogs",
                columns: table => new
                {
                    LogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EMRID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_TreatmentLogs_EMRs_EMRID",
                        column: x => x.EMRID,
                        principalTable: "EMRs",
                        principalColumn: "EMRID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_EMRID",
                table: "Prescriptions",
                column: "EMRID");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentLogs_EMRID",
                table: "TreatmentLogs",
                column: "EMRID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "TreatmentLogs");

            migrationBuilder.DropTable(
                name: "EMRs");
        }
    }
}
