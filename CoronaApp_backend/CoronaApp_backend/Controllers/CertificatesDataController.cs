using Microsoft.AspNetCore.Mvc;
using CoronaApp_DbInfo.Models;
using CoronaApp_DbInfo;
using DCC;

namespace CoronaApp_backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CertificatesDataController : ControllerBase
	{
		private readonly CoronavirusCertificatesDbContext _context;
		
		public CertificatesDataController(CoronavirusCertificatesDbContext context) {
			_context = context;
		}

		// GET: api/CertificatesData
		[HttpGet]
		public ActionResult<IEnumerable<CertificateDatum>> GetCertificatesData()
		{
			if (_context.CertificatesData == null)
				return Ok("There are no registered certificates.");

			return _context.CertificatesData.ToList();
		}

		// GET: api/CertificatesData/5
		[HttpGet("{id}")]
		public ActionResult<CertificateDatum> GetCertificatesDatum(int id)
		{
			return Problem("This method has no implementation.");

			#region implementation
			//CertificateDatum? desired = _context.CertificatesData.Find(id);

			//if (desired == null)
			//	return NotFound();

			//return desired;
			#endregion
		}

		// PUT: api/CertificatesData/5
		[HttpPut("{id}")]
		public IActionResult PutCertificatesDatum(int id, CertificateDatum certificateDatum)
		{
			return Problem("This method has no implementation.");

			#region implementation
			//if (id != certificateDatum.Id)
			//	return BadRequest();

			//CertificatesFillingData(certificateDatum);
			//_context.Entry(certificateDatum).State = EntityState.Modified;
			//_context.SaveChanges();

			//return NoContent();
			#endregion
		}

		// POST: api/CertificatesData
		[HttpPost]
		public ActionResult<CertificateDatum> PostCertificatesDatum(CertificateDatum certificateDatum)
		{
			if (_context.CertificatesData == null)
				return Problem("Entity set \"CoronavirusCertificatesContext.CertificatesData\" is null.");

			CertificatesFillingData(certificateDatum);
			_context.CertificatesData.Add(certificateDatum);
			_context.SaveChanges();

			return Ok();
		}

		// DELETE: api/CertificatesData/5
		[HttpDelete("{id}")]
		public IActionResult DeleteCertificatesDatum(int id)
		{
			return Problem("This method has no implementation.");

			#region implementation
			//if (_context.CertificatesData == null)
			//	return NotFound();

			//var certificatesDatum = _context.CertificatesData.Find(id);

			//if (certificatesDatum == null)
			//	return NotFound();

			//_context.CertificatesData.Remove(certificatesDatum);
			//_context.SaveChanges();

			//return NoContent();
			#endregion
		}

		private void CertificatesFillingData(CertificateDatum certificateDatum)
		{
			GreenCertificateDecoder decoder = new GreenCertificateDecoder();
			CWT cwt = decoder.Decode(certificateDatum.RawCertificateData);

			//certificateDatum.Id				= ...
			certificateDatum.EntryDateUtc		= DateTime.UtcNow;
			//certificateDatum.RawCertificateData= ...
			certificateDatum.CertificateId		= cwt.DGCv1.Vaccination[0].CertificateIdentifier;
			//certificateDatum.FirstName		= ...
			//certificateDatum.LastName			= ...
			//certificateDatum.DateOfBirth		= ...
			certificateDatum.CountryCode		= cwt.DGCv1.Vaccination[0].CountryOfVaccination;
			certificateDatum.Issuer				= cwt.DGCv1.Vaccination[0].Issuer;
			certificateDatum.Manufacturer		= cwt.DGCv1.Vaccination[0].Manufacturer;
			certificateDatum.MedicalProduct		= cwt.DGCv1.Vaccination[0].MedicalProduct;
			certificateDatum.DiseaseCode		= cwt.DGCv1.Vaccination[0].Disease;
			certificateDatum.Vaccine			= cwt.DGCv1.Vaccination[0].Vaccine;
			certificateDatum.DoseNumber			= (int)cwt.DGCv1.Vaccination[0].DoseNumber;
			certificateDatum.TotalDoses			= (int)cwt.DGCv1.Vaccination[0].TotalDoses;
			certificateDatum.VaccinationDateUtc	= cwt.DGCv1.Vaccination[0].VaccinationDate.UtcDateTime;
			certificateDatum.ExpirationTime		= cwt.ExpirationTime;
			//certificateDatum.isValid			= ...
		}
	}
}