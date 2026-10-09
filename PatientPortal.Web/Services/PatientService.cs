using Microsoft.EntityFrameworkCore;
using PatientPortal.Web.Data;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Services;

public interface IPatientService
{
    Task<List<Patient>> SearchPatients(string query, CancellationToken cancellationToken = default);
    Task<Patient?> GetPatientDetails(int patientId, CancellationToken cancellationToken = default);
}

public class PatientService(PatientPortalContext dbContext) : IPatientService
{
    public async Task<Patient?> GetPatientDetails(int patientId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Patients
            .Include(p => p.Allergies)
            .Include(p => p.Medications)
            .Include(p => p.Documents)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.PatientId == patientId, cancellationToken);
    }

    public async Task<List<Patient>> SearchPatients(string query, CancellationToken cancellationToken = default)
    {        
        if (string.IsNullOrWhiteSpace(query))
            return [];

        return await dbContext.Patients
            .Where(p => p.FirstName.Contains(query)
                || p.LastName.Contains(query)
                || p.MRN.Contains(query))
            .Take(50)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
