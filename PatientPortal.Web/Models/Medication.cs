using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Web.Models;

public class Medication
{
    public int MedicationId { get; set; }
    
    public int PatientId { get; set; }
    
    [MaxLength(100)]
    public string MedicationName { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Strength { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string DosageInstructions { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Frequency { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Route { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    [MaxLength(100)]
    public string PrescribingProvider { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;
}
