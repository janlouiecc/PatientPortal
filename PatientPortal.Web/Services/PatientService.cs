using Microsoft.EntityFrameworkCore;
using PatientPortal.Web.Data;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Services;

public interface IPatientService
{
    Task<(List<Patient> Patients, int TotalCount)> SearchPatients(string query, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
    Task<Patient?> GetPatientDetails(int patientId, CancellationToken ct = default);
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

    public async Task<(List<Patient> Patients, int TotalCount)> SearchPatients(
        string query, 
        int pageNumber = 1, 
        int pageSize = 10, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return ([], 0);

        var baseQuery = dbContext.Patients.AsQueryable();
        var pattern = $"%{query}%";

        if (int.TryParse(query, out var year) && query.Length == 4)
        {
            baseQuery = baseQuery.Where(p => p.DOB.Year == year ||
                                        p.MRN.Contains(query) ||
                                        EF.Functions.Like(p.FirstName, pattern) ||
                                        EF.Functions.Like(p.LastName, pattern));
        }
        else if (DateTime.TryParse(query, out var searchDate))
        {
            baseQuery = baseQuery.Where(p => p.DOB.Date == searchDate.Date ||
                                        p.MRN.Contains(query) ||
                                        EF.Functions.Like(p.FirstName, pattern) ||
                                        EF.Functions.Like(p.LastName, pattern));
        }
        else
        {
            baseQuery = baseQuery.Where(p => EF.Functions.Like(p.FirstName, pattern) ||
                                        EF.Functions.Like(p.LastName, pattern) ||
                                        EF.Functions.Like(p.MRN, pattern));
        }

        int totalCount = await baseQuery.CountAsync(cancellationToken);

        var patients = await baseQuery
            .OrderBy(p => p.LastName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return (patients, totalCount);
    }
}
