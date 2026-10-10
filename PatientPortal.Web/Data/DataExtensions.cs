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
                    context.Set<Patient>().AddRange(SeedPatients());
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

    private static List<Patient> SeedPatients()
    {
        return
        [
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
                    new Allergy { AllergyName = "Penicillin", Severity = "Severe", Status = "Active", RecordedDate = DateTime.Now.AddMonths(-6) }
                ],
                Medications = [
                    new Medication { MedicationName = "Lisinopril", Strength = "10mg", DosageInstructions = "1 tablet", Frequency = "Daily", Route = "Oral", StartDate = DateTime.Now.AddMonths(-3), PrescribingProvider = "Dr. Smith", Status = "Active" }
                ]
            },
            new Patient
            {
                MRN = "MRN-20391",
                FirstName = "Jane",
                LastName = "Smith",
                DOB = new DateTime(1990, 8, 22),
                Gender = "Female",
                PhoneNumber = "555-023-4950",
                AddressLine1 = "456 Market St",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94105",
                Allergies = [
                    new Allergy { AllergyName = "Latex", Severity = "Moderate", Status = "Active", RecordedDate = DateTime.Now.AddYears(-1) }
                ],
                Medications = [
                    new Medication { MedicationName = "Sertraline", Strength = "50mg", DosageInstructions = "1 tablet", Frequency = "Daily", Route = "Oral", StartDate = DateTime.Now.AddMonths(-6), PrescribingProvider = "Dr. Adams", Status = "Active" }
                ]
            },
            new Patient
            {
                MRN = "MRN-30482",
                FirstName = "Robert",
                LastName = "Johnson",
                DOB = new DateTime(1965, 3, 15),
                Gender = "Male",
                PhoneNumber = "555-034-8192",
                AddressLine1 = "789 Pine Rd",
                City = "Portland",
                State = "OR",
                ZipCode = "97201",
                Allergies = [
                    new Allergy { AllergyName = "Peanuts", Severity = "Severe", Status = "Active", RecordedDate = DateTime.Now.AddYears(-2) }
                ],
                Medications = [
                    new Medication { MedicationName = "Metformin", Strength = "500mg", DosageInstructions = "2 tablets", Frequency = "Twice Daily", Route = "Oral", StartDate = DateTime.Now.AddYears(-1), PrescribingProvider = "Dr. Lee", Status = "Active" },
                    new Medication { MedicationName = "Atorvastatin", Strength = "20mg", DosageInstructions = "1 tablet", Frequency = "Daily", Route = "Oral", StartDate = DateTime.Now.AddYears(-1), PrescribingProvider = "Dr. Lee", Status = "Active" }
                ]
            },
            new Patient
            {
                MRN = "MRN-40591",
                FirstName = "Emily",
                LastName = "Davis",
                DOB = new DateTime(1995, 11, 4),
                Gender = "Female",
                PhoneNumber = "555-045-9203",
                AddressLine1 = "321 Oak Ave",
                City = "Seattle",
                State = "WA",
                ZipCode = "98103",
                Allergies = [],
                Medications = [
                    new Medication { MedicationName = "Albuterol Inhaler", Strength = "90mcg", DosageInstructions = "2 puffs", Frequency = "As needed", Route = "Inhalation", StartDate = DateTime.Now.AddMonths(-2), PrescribingProvider = "Dr. Evans", Status = "Active" }
                ]
            },
            new Patient
            {
                MRN = "MRN-50602",
                FirstName = "Michael",
                LastName = "Brown",
                DOB = new DateTime(1978, 12, 30),
                Gender = "Male",
                PhoneNumber = "555-056-0314",
                AddressLine1 = "654 Elm St",
                City = "Tacoma",
                State = "WA",
                ZipCode = "98402",
                Allergies = [
                    new Allergy { AllergyName = "Sulfa Drugs", Severity = "Moderate", Status = "Active", RecordedDate = DateTime.Now.AddMonths(-4) }
                ],
                Medications = []
            },
            new Patient
            {
                MRN = "MRN-60713",
                FirstName = "Sarah",
                LastName = "Wilson",
                DOB = new DateTime(1988, 7, 19),
                Gender = "Female",
                PhoneNumber = "555-067-1425",
                AddressLine1 = "987 Cedar Blvd",
                City = "Spokane",
                State = "WA",
                ZipCode = "99201",
                Allergies = [],
                Medications = [
                    new Medication { MedicationName = "Levothyroxine", Strength = "50mcg", DosageInstructions = "1 tablet", Frequency = "Daily", Route = "Oral", StartDate = DateTime.Now.AddMonths(-8), PrescribingProvider = "Dr. Baker", Status = "Active" }
                ]
            },
            new Patient
            {
                MRN = "MRN-70824",
                FirstName = "David",
                LastName = "Martinez",
                DOB = new DateTime(1954, 1, 10),
                Gender = "Male",
                PhoneNumber = "555-078-2536",
                AddressLine1 = "111 Washington St",
                City = "Olympia",
                State = "WA",
                ZipCode = "98501",
                Allergies = [
                    new Allergy { AllergyName = "Aspirin", Severity = "Severe", Status = "Active", RecordedDate = DateTime.Now.AddYears(-3) }
                ],
                Medications = [
                    new Medication { MedicationName = "Warfarin", Strength = "5mg", DosageInstructions = "1 tablet", Frequency = "Daily", Route = "Oral", StartDate = DateTime.Now.AddYears(-1), PrescribingProvider = "Dr. Clark", Status = "Active" }
                ]
            },
            new Patient
            {
                MRN = "MRN-80935",
                FirstName = "Jessica",
                LastName = "Taylor",
                DOB = new DateTime(2001, 4, 5),
                Gender = "Female",
                PhoneNumber = "555-089-3647",
                AddressLine1 = "222 Broadway",
                City = "Seattle",
                State = "WA",
                ZipCode = "98122",
                Allergies = [],
                Medications = []
            },
            new Patient
            {
                MRN = "MRN-90046",
                FirstName = "James",
                LastName = "Anderson",
                DOB = new DateTime(1972, 9, 14),
                Gender = "Male",
                PhoneNumber = "555-090-4758",
                AddressLine1 = "333 Lakeview Dr",
                City = "Bellevue",
                State = "WA",
                ZipCode = "98004",
                Allergies = [
                    new Allergy { AllergyName = "Iodine", Severity = "Mild", Status = "Active", RecordedDate = DateTime.Now.AddMonths(-10) }
                ],
                Medications = [
                    new Medication { MedicationName = "Omeprazole", Strength = "20mg", DosageInstructions = "1 capsule", Frequency = "Daily", Route = "Oral", StartDate = DateTime.Now.AddMonths(-4), PrescribingProvider = "Dr. Wright", Status = "Active" }
                ]
            },
            new Patient
            {
                MRN = "MRN-10157",
                FirstName = "Amanda",
                LastName = "Thomas",
                DOB = new DateTime(1986, 6, 25),
                Gender = "Female",
                PhoneNumber = "555-101-5869",
                AddressLine1 = "444 High St",
                City = "Bellingham",
                State = "WA",
                ZipCode = "98225",
                Allergies = [],
                Medications = [
                    new Medication { MedicationName = "Cetirizine", Strength = "10mg", DosageInstructions = "1 tablet", Frequency = "Daily", Route = "Oral", StartDate = DateTime.Now.AddMonths(-2), PrescribingProvider = "Dr. Young", Status = "Active" }
                ]
            }
        ];
    }
}
