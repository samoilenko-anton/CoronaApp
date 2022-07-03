namespace CoronaApp_backend.Models
{
	public class CertificateDatum
	{
		public int Id { get; set; }
		public DateTime EntryDateUtc { get; set; }
		public string RawCertificateData { get; set; } = null!;
		public string? CertificateId { get; set; }				// Example: URN:UVCI:01:UA:CDR9QL0WB761LRV07RDSV4XN
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public DateTime DateOfBirth { get; set; }
		public string? CountryCode { get; set; }				// Example: "UA"
		public string? Issuer { get; set; }						// Example: "State Enterprise "DIIA"
		public string? Manufacturer { get; set; }				// Example: "ORG-100001699" --- code of AstraZeneca AB
		public string? MedicalProduct { get; set; }				// Example: "EU/1/21/1529" --- code of Vaxzevria
		public string? DiseaseCode { get; set; }				// Example: "840539006" --- code for COVID-19 from SNOMED CT (GPS)
		public string? Vaccine { get; set; }					// Example: "J07BX03" --- code of covid-19 vaccines
		public int? DoseNumber { get; set; }
		public int? TotalDoses { get; set; }
		public DateTime? VaccinationDateUtc { get; set; }		// Example: {09.12.2021 22:00:00} --- UTC
		public DateTime? ExpirationTime { get; set; }			// Example: {09.12.2022 22:00:00} --- UTC
		public bool IsValid { get; set; }
	}
}