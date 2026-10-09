using PatientPortal.Web.Data;
using PatientPortal.Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddPatientPortalContext();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.EnsureDatabaseCreation();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
