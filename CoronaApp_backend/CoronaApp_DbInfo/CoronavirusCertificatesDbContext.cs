using CoronaApp_DbInfo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoronaApp_DbInfo
{
	public class CoronavirusCertificatesDbContext : DbContext
	{
		public CoronavirusCertificatesDbContext() { }
		public CoronavirusCertificatesDbContext(DbContextOptions<CoronavirusCertificatesDbContext> options) : base(options) { }

		public DbSet<CertificateDatum> CertificatesData { get; private set; } = null!;

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { optionsBuilder.EnableSensitiveDataLogging(); }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}