using Microsoft.EntityFrameworkCore;
using PatientPortal.Web.Data;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Services;

public interface IPatientService
{
    Task<Patient?> GetPatientDetails(int patientId, CancellationToken cancellationToken = default);
    Task UpdatePatient(Patient patient, CancellationToken cancellationToken = default);
    Task AddAllergy(int patientId, Allergy allergy, CancellationToken cancellationToken = default);
    Task RemoveAllergy(int allergyId, CancellationToken cancellationToken = default);
    Task AddMedication(int patientId, Medication medication, CancellationToken cancellationToken = default);
    Task RemoveMedication(int medicationId, CancellationToken cancellationToken = default);
    Task<(List<Patient> Patients, int TotalCount)> SearchPatients(
        string query,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
    Task AddDocumentAsync(
        int patientId,
        string fileName,
        string filePath,
        string documentType,
        CancellationToken cancellationToken = default);
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
        var isYear = int.TryParse(query, out var year) && query.Length == 4;
        var isDate = DateTime.TryParse(query, out var searchDate);

        baseQuery = baseQuery.Where(p =>
            (isYear && p.DOB.Year == year) ||
            (isDate && p.DOB.Date == searchDate.Date) ||
            p.MRN.Contains(query) ||
            EF.Functions.Like(p.FirstName, pattern) ||
            EF.Functions.Like(p.LastName, pattern) ||
            EF.Functions.Like(p.MRN, pattern)
        );

        int totalCount = await baseQuery.CountAsync(cancellationToken);

        var patients = await baseQuery
            .OrderBy(p => p.LastName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return (patients, totalCount);
    }

    public async Task AddDocumentAsync(
        int patientId,
        string fileName,
        string filePath,
        string documentType,
        CancellationToken cancellationToken = default)
    {
        var document = new Document
        {
            PatientId = patientId,
            DocumentName = fileName,
            DocumentType = documentType,
            DocumentDate = DateTime.Now,
            FilePath = filePath,
            UploadedDate = DateTime.Now
        };

        await dbContext.Documents.AddAsync(document, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePatient(Patient patient, CancellationToken cancellationToken = default)
    {
        dbContext.Patients.Update(patient);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAllergy(int patientId, Allergy allergy, CancellationToken cancellationToken = default)
    {
        allergy.PatientId = patientId;
        allergy.RecordedDate = DateTime.Now;
        await dbContext.Allergies.AddAsync(allergy, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAllergy(int allergyId, CancellationToken cancellationToken = default)
    {
        var allergy = await dbContext.Allergies.FirstOrDefaultAsync(a => 
            a.AllergyId == allergyId, 
            cancellationToken: cancellationToken);

        if (allergy is not null)
        {
            dbContext.Allergies.Remove(allergy);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task AddMedication(int patientId, Medication medication, CancellationToken cancellationToken = default)
    {
        medication.PatientId = patientId;
        medication.StartDate = DateTime.Now;
        await dbContext.Medications.AddAsync(medication, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveMedication(int medicationId, CancellationToken cancellationToken = default)
    {
        var medication = await dbContext.Medications.FirstOrDefaultAsync(m => 
            m.MedicationId == medicationId, 
            cancellationToken: cancellationToken);
            
        if (medication is not null)
        {
            dbContext.Medications.Remove(medication);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
