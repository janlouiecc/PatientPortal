using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using PatientPortal.Web.Data;
using PatientPortal.Web.Endpoints;
using PatientPortal.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddPatientPortalContext();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:ClientId"] ??
        throw new InvalidOperationException("ClientId has not been set in environment.");
    options.ClientSecret = builder.Configuration["Authentication:ClientSecret"] ??
        throw new InvalidOperationException("ClientSecret has not been set in environment");
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.EnsureDatabaseCreation();
app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();

app.Run();
