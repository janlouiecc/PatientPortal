using PatientPortal.Web.Models;

namespace PatientPortal.Web.Data;

public static class DataExtensions
{
    public static void AddPatientPortalContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PatientPortal");
        builder.Services.AddSqlite<PatientPortalContext>(
            connectionString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Patient>().Any())
                {
                    context.Set<Patient>().AddRange(
                        new Patient
                        {
                            MRN = "MRN-10492",
                            FirstName = "John",
                            LastName = "Doe",
                            DOB = new DateTime(1982, 5, 12),
                            Gender = "Male",
                            PhoneNumber = "555-019-2041",
                            AddressLine1 = "123 Tech Blvd",
                            City = "Seattle",
                            State = "WA",
                            ZipCode = "98101",
                            Allergies = [
                                new Allergy
                                {
                                    AllergyName = "Penicillin",
                                    Severity = "Severe",
                                    Status = "Active",
                                    RecordedDate = DateTime.Now
                                }
                            ],
                            Medications = [
                                new Medication
                                {
                                    MedicationName = "Lisinopril",
                                    Strength = "10mg",
                                    DosageInstructions = "1 tablet",
                                    Frequency = "Daily",
                                    Route = "Oral",
                                    StartDate = DateTime.Now,
                                    PrescribingProvider = "Dr. Smith",
                                    Status = "Active"
                                }
                            ]
                        }
                    );

                    context.SaveChanges();
                }
            })
        );
    }

    public static void EnsureDatabaseCreation(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PatientPortalContext>();        
        dbContext.Database.EnsureCreated();
    }
}
