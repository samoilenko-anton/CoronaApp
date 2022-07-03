using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoronaApp_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificatesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDateUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    RawCertificateData = table.Column<string>(type: "nvarchar(768)", maxLength: 768, nullable: false),
                    CertificateId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Issuer = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    MedicalProduct = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DiseaseCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Vaccine = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    DoseNumber = table.Column<int>(type: "int", nullable: true),
                    TotalDoses = table.Column<int>(type: "int", nullable: true),
                    VaccinationDateUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdentifierPrimaryKey", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificatesData");
        }
    }
}
