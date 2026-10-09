using Microsoft.EntityFrameworkCore;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Data;

public class PatientPortalContext(DbContextOptions<PatientPortalContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Allergy> Allergies => Set<Allergy>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<Document> Documents => Set<Document>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Allergies)
            .WithOne()
            .HasForeignKey(a => a.PatientId);

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Medications)
            .WithOne()
            .HasForeignKey(m => m.PatientId);

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Documents)
            .WithOne()
            .HasForeignKey(d => d.PatientId);
    }
}
