using PatientPortal.Web.Models;

namespace PatientPortal.Web.Services;

public interface IPatientService
{
    Task<List<Patient>> SearchPatientsAsync(string query);
    Task<Patient?> GetPatientDetailsAsync(int patientId);
}

public class PatientService : IPatientService
{
    public Task<Patient?> GetPatientDetailsAsync(int patientId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Patient>> SearchPatientsAsync(string query)
    {
        throw new NotImplementedException();
    }
}
