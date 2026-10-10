using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using PatientPortal.Web.Services;

namespace PatientPortal.Web.Extensions;

public static class BuilderExtensions
{
    public static void AddPatientPortalServices(this WebApplicationBuilder builder, IConfiguration configuration)
    {
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
            options.ClientId = configuration["Authentication:ClientId"] ??
                throw new InvalidOperationException("ClientId has not been set in environment.");
            options.ClientSecret = configuration["Authentication:ClientSecret"] ??
                throw new InvalidOperationException("ClientSecret has not been set in environment");
        });

        builder.Services.AddAuthorization();

        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizePage("/PatientDetails");
            options.Conventions.AuthorizePage("/Search");
        });
    }
}
