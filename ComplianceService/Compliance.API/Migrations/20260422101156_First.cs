using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compliance.API.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    AuditID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.AuditID);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceRecords",
                columns: table => new
                {
                    ComplianceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceRecords", x => x.ComplianceID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "ComplianceRecords");
        }
    }
}
