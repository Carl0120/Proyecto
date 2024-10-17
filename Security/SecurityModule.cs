using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Security.Managers;
using Security.Models;
using Security.Services;
using Security.Store;

namespace Security;

public static class SecurityModule
{
    public static void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

       services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddSignInManager()
            .AddSignInManager<AppSignInManager>()
            //Role
            .AddRoles<AppRole>()
            .AddRoleManager<AppRoleManager>()
            .AddRoleStore<AppRoleStore>()
            //User
            .AddUserManager<AppUserManager>()
            .AddUserStore<AppUserStore>()
            
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

      services.AddSingleton<IEmailSender<AppUser>, IdentityNoOpEmailSender>();
    }

    public static void InitModule(WebApplication app)
    {
        app.MapAdditionalIdentityEndpoints();
    }

}