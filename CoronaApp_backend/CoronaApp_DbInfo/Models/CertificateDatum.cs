using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaApp_DbInfo.Models
{
	public class CertificateDatum
	{
		[Key]
		public int Id { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime EntryDateUtc { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(1024)")]
		public string RawCertificateData { get; set; } = null!;

		[Column(TypeName = "nvarchar(64)")]
		public string? CertificateId { get; set; }				// Example: URN:UVCI:01:UA:CDR9QL0WB761LRV07RDSV4XN

		[Required]
		[Column(TypeName = "nvarchar(64)")]
		public string FirstName { get; set; } = null!;

		[Required]
		[Column(TypeName = "nvarchar(64)")]
		public string LastName { get; set; } = null!;

		[Required]
		[Column(TypeName = "date")]
		public DateTime DateOfBirth { get; set; }

		[Column(TypeName = "nvarchar(4)")]
		public string? CountryCode { get; set; }				// Example: "UA"

		[Column(TypeName = "nvarchar(128)")]
		public string? Issuer { get; set; }						// Example: "State Enterprise "DIIA"

		[Column(TypeName = "nvarchar(32)")]
		public string? Manufacturer { get; set; }				// Example: "ORG-100001699" --- code of AstraZeneca AB

		[Column(TypeName = "nvarchar(32)")]
		public string? MedicalProduct { get; set; }				// Example: "EU/1/21/1529" --- code of Vaxzevria

		[Column(TypeName = "nvarchar(16)")]
		public string? DiseaseCode { get; set; }				// Example: "840539006" --- code for COVID-19 from SNOMED CT (GPS)

		[Column(TypeName = "nvarchar(16)")]
		public string? Vaccine { get; set; }					// Example: "J07BX03" --- code of covid-19 vaccines

		[Column(TypeName = "int")]
		public int? DoseNumber { get; set; }

		[Column(TypeName = "int")]
		public int? TotalDoses { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? VaccinationDateUtc { get; set; }		// Example: {09.12.2021 22:00:00} --- UTC

		[Column(TypeName = "datetime")]
		public DateTime? ExpirationTime { get; set; }			// Example: {09.12.2022 22:00:00} --- UTC

		[Required]
		[Column(TypeName = "bit")]
		public bool IsValid { get; set; }
	}
}