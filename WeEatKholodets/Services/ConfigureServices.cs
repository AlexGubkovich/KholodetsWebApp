using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using WeEatKholodets.Data;
using WeEatKholodets.Models;

namespace WeEatKholodets.Services
{
    public static class ConfigureService
    {
        public static void AddDbConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter(); 
        }

        public static void AddLocalizationConfig(this IServiceCollection services)
        {
            services.AddPortableObjectLocalization(options => options.ResourcesPath = "Localization");
            services.Configure<RequestLocalizationOptions>(options => {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("en"),
                    new CultureInfo("ru-ru"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        public static void AddIdentityAndAuthenticationConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddGoogle("google", options =>
                {
                    options.ClientId = configuration["Google:ClientId"];
                    options.ClientSecret = configuration["Google:ClientSecret"];
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.ClaimActions.MapJsonKey("image", "picture");
                }); 
        }

        public static void AddSessionConfig(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
        }
    }
}