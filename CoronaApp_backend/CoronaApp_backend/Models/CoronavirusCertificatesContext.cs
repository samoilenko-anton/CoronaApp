using Microsoft.EntityFrameworkCore;

namespace CoronaApp_backend.Models
{
	public class CoronavirusCertificatesContext : DbContext
	{
		public CoronavirusCertificatesContext() { }
		public CoronavirusCertificatesContext(DbContextOptions<CoronavirusCertificatesContext> options) : base(options) { }

		public DbSet<CertificateDatum> CertificatesData { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Entity properties
			modelBuilder.Entity<CertificateDatum>(entity =>
			{
				entity.HasKey(e => e.Id)
					.HasName("IdentifierPrimaryKey");

				entity.Property(e => e.EntryDateUtc)
					.HasColumnType("datetime");

				entity.Property(e => e.RawCertificateData)
					.HasColumnType("nvarchar")
					.HasMaxLength(768)
					.IsRequired();

				entity.Property(e => e.CertificateId)
					.HasColumnType("nvarchar")
					.HasMaxLength(64);

				entity.Property(e => e.FirstName)
					.HasColumnType("nvarchar")
					.HasMaxLength(64)
					.IsRequired();

				entity.Property(e => e.LastName)
					.HasColumnType("nvarchar")
					.HasMaxLength(64)
					.IsRequired();

				entity.Property(e => e.DateOfBirth)
					.HasColumnType("date")
					.IsRequired();

				entity.Property(e => e.CountryCode)
					.HasColumnType("nvarchar")
					.HasMaxLength(4);

				entity.Property(e => e.Issuer)
					.HasColumnType("nvarchar")
					.HasMaxLength(128);

				entity.Property(e => e.Manufacturer)
					.HasColumnType("nvarchar")
					.HasMaxLength(32);

				entity.Property(e => e.MedicalProduct)
					.HasColumnType("nvarchar")
					.HasMaxLength(32);

				entity.Property(e => e.DiseaseCode)
					.HasColumnType("nvarchar")
					.HasMaxLength(16);

				entity.Property(e => e.Vaccine)
					.HasColumnType("nvarchar")
					.HasMaxLength(16);

				entity.Property(e => e.DoseNumber)
					.HasColumnType("int");

				entity.Property(e => e.TotalDoses)
					.HasColumnType("int");

				entity.Property(e => e.VaccinationDateUtc)
					.HasColumnType("datetime");

				entity.Property(e => e.ExpirationTime)
					.HasColumnType("datetime");

				entity.Property(e => e.IsValid)
					.HasColumnType("bit");
			});
		}
	}
}