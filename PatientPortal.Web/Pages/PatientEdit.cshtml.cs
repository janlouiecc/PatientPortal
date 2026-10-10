using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientPortal.Web.Models;
using PatientPortal.Web.Services;

namespace PatientPortal.Web.Pages;

public class PatientEditModel(IPatientService patientService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public required Patient PatientRecord { get; set; }

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        var patient = await patientService.GetPatientDetails(Id, cancellationToken);
        if (patient is null) 
            return NotFound();
        
        PatientRecord = patient;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        PatientRecord.PatientId = Id;
        await patientService.UpdatePatient(PatientRecord, cancellationToken);

        return RedirectToPage("/PatientDetails", new { id = Id });
    }
}