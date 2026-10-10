using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientPortal.Web.Services;

namespace PatientPortal.Web.Pages;

public class PatientDetailsModel(IPatientService patientService) : PageModel
{
    public Models.Patient? PatientRecord { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        PatientRecord = await patientService.GetPatientDetails(id, cancellationToken);

        if (PatientRecord is null)
            return NotFound();

        return Page();
    }
}