using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientPortal.Web.Services;

namespace PatientPortal.Web.Pages;

public class SearchModel(IPatientService patientService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; } = 10;
    public int TotalPages { get; set; }

    public List<Models.Patient> Results { get; set; } = [];
    public bool HasSearched { get; set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(Query))
        {
            HasSearched = true;
            
            var (patients, totalCount) = 
                await patientService.SearchPatients(Query, PageNumber, PageSize, cancellationToken);
            
            Results = patients;
            
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        }
    }
}