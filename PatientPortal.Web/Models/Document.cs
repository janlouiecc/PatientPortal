using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Web.Models;

public class Document
{
    public int DocumentId { get; set; }
    
    public int PatientId { get; set; }
    
    [MaxLength(200)]
    public string DocumentName { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string DocumentType { get; set; } = string.Empty;
    
    public DateTime DocumentDate { get; set; }
    
    [MaxLength(500)]
    public string FilePath { get; set; } = string.Empty;
    
    public DateTime UploadedDate { get; set; }
}
