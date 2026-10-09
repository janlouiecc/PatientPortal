using PatientPortal.Web.Data;

var builder = WebApplication.CreateBuilder(args);
builder.AddPatientPortalContext();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.EnsureDatabaseCreation();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
