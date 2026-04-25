using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabService.API.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    TestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicianID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.TestID);
                });

            migrationBuilder.CreateTable(
                name: "LabReports",
                columns: table => new
                {
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileURI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_LabReports_LabTests_TestID",
                        column: x => x.TestID,
                        principalTable: "LabTests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabReports_TestID",
                table: "LabReports",
                column: "TestID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabReports");

            migrationBuilder.DropTable(
                name: "LabTests");
        }
    }
}
