namespace BlazorApp.Areas.Register;

using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using Security.Managers;
using Security.Models;
using Security.Services;


public class RegisterViewModel : ComponentBase
{
   
    [Inject]public AppUserManager UserManager { get; set; }
    
    [Inject]  public IUserStore<AppUser> UserStore { get; set; }
    
    [Inject] public SignInManager<AppUser> SignInManager { get; set; }
    
    [Inject] public ILogger<RegisterViewModel> Logger { get; set; }
    
    [Inject] public  NavigationManager NavigationManager { get; set; }
    
    [Inject] public IdentityRedirectManager RedirectManager { get; set; }
    
    [Inject] public  IEnumerable<IdentityError>? identityErrors { get; set; }

    [SupplyParameterFromForm] protected RegisterModel Input { get; set; } = new();

    [SupplyParameterFromQuery] private string? ReturnUrl { get; set; }

    private string? Message => identityErrors is null ? null : $"Error: {string.Join(", ", identityErrors.Select(error => error.Description))}";

    public async Task RegisterUser()
    {
        var user = CreateUser();

        await UserStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
    
        var result = await UserManager.CreateAsync(user, Input.Password);

        if (!result.Succeeded)
        {
            identityErrors = result.Errors;
            return;
        }

        Logger.LogInformation("User created a new account with password.");

        var userId = await UserManager.GetUserIdAsync(user);
        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
        
        var activateResult = await UserManager.ConfirmEmailAsync(user, code);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = NavigationManager.GetUriWithQueryParameters(
            NavigationManager.ToAbsoluteUri("ActivateAccount").AbsoluteUri,
            new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code, ["returnUrl"] = ReturnUrl });
        
            RedirectManager.RedirectTo(callbackUrl);
    }

    private AppUser CreateUser()
    {
        try
        {
            var user = Activator.CreateInstance<AppUser>();
            user.EmailConfirmed = true;
            return user;
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                                                $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor.");
        }
    }
    
}