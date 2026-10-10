using PatientPortal.Web.Data;
using PatientPortal.Web.Endpoints;
using PatientPortal.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddPatientPortalContext();
builder.AddPatientPortalServices(builder.Configuration);

var app = builder.Build();
app.EnsureDatabaseCreation();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapAuthEndpoints();

app.Run();
