using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientPortal.Web.Models;
using PatientPortal.Web.Services;

namespace PatientPortal.Web.Pages;

public class PatientDetailsModel(IPatientService patientService, IWebHostEnvironment environment) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public Patient? PatientRecord { get; set; }

    [BindProperty]
    public IFormFile? UploadFile { get; set; }

    [BindProperty]
    public string DocumentType { get; set; } = "Miscellaneous";

    [BindProperty]
    public Allergy NewAllergy { get; set; } = new();

    [BindProperty]
    public Medication NewMedication { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        PatientRecord = await patientService.GetPatientDetails(id, cancellationToken);

        if (PatientRecord is null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        if (UploadFile is not null)
        {
            var uploadsFolder = Path.Combine(environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{UploadFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await UploadFile.CopyToAsync(fileStream, cancellationToken);

            await patientService.AddDocumentAsync(
                id,
                UploadFile.FileName,
                $"/uploads/{uniqueFileName}",
                DocumentType,
                cancellationToken);
        }

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostAddAllergyAsync(int id, CancellationToken cancellationToken)
    {
        await patientService.AddAllergy(id, NewAllergy, cancellationToken);
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostRemoveAllergyAsync(int id, int allergyId, CancellationToken cancellationToken)
    {
        await patientService.RemoveAllergy(allergyId, cancellationToken);
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostAddMedicationAsync(int id, CancellationToken cancellationToken)
    {
        await patientService.AddMedication(id, NewMedication, cancellationToken);
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostRemoveMedicationAsync(int id, int medicationId, CancellationToken cancellationToken)
    {
        await patientService.RemoveMedication(medicationId, cancellationToken);
        return RedirectToPage(new { id });
    }
}