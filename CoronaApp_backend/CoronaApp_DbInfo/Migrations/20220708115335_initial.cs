using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoronaApp_DbInfo.Migrations
{
    public partial class initial : Migration
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
                    RawCertificateData = table.Column<string>(type: "nvarchar(1024)", nullable: false),
                    CertificateId = table.Column<string>(type: "nvarchar(64)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    Issuer = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    MedicalProduct = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    DiseaseCode = table.Column<string>(type: "nvarchar(16)", nullable: true),
                    Vaccine = table.Column<string>(type: "nvarchar(16)", nullable: true),
                    DoseNumber = table.Column<int>(type: "int", nullable: true),
                    TotalDoses = table.Column<int>(type: "int", nullable: true),
                    VaccinationDateUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificatesData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificatesData");
        }
    }
}
