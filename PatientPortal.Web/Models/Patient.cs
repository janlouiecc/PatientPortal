using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Web.Models;

public class Patient
{
    public int PatientId { get; set; }

    [MaxLength(20)]
    public string MRN { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public DateTime DOB { get; set; }

    [MaxLength(10)]
    public string Gender { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(100)]
    public string AddressLine1 { get; set; } = string.Empty;

    [MaxLength(50)]
    public string City { get; set; } = string.Empty;

    [MaxLength(20)]
    public string State { get; set; } = string.Empty;

    [MaxLength(10)]
    public string ZipCode { get; set; } = string.Empty;

    public ICollection<Allergy> Allergies { get; set; } = [];
    public ICollection<Medication> Medications { get; set; } = [];
    public ICollection<Document> Documents { get; set; } = [];
}
