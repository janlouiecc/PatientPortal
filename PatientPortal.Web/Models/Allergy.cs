using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Web.Models;

public class Allergy
{
    public int AllergyId { get; set; }

    public int PatientId { get; set; }

    [MaxLength(100)]
    public string AllergyName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Severity { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;
    
    public DateTime RecordedDate { get; set; }
}
