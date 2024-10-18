using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Security.Managers;
using Security.Models;
using Security.Services;

namespace BlazorApp.Areas.Login;

public class LoginViewModel : ComponentBase
{
   
    [Inject] public AppSignInManager SignInManager { get; set; }
    
    [Inject] public ILogger<LoginViewModel> Logger { get; set; }
    
    [Inject] public NavigationManager NavigationManager { get; set; }
    
    [Inject] public  IdentityRedirectManager RedirectManager { get; set; }
    
    protected string? errorMessage;
    
    [Inject] private HttpContextAccessor httpContextAccessor { get; set; }
    
    protected HttpContext? HttpContext { get; set; } = default!;

    [SupplyParameterFromForm] protected LoginModel Input { get; set; } = new();

    [SupplyParameterFromQuery] protected string? ReturnUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        HttpContext = httpContextAccessor.HttpContext;
        if (HttpMethods.IsGet(HttpContext.Request.Method))
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }
    }

    public async Task LoginUser()
    {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await SignInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            Logger.LogInformation("Usuario Logeado");
            RedirectManager.RedirectTo(ReturnUrl);
        }
        else if (result.IsLockedOut)
        {
            Logger.LogWarning("La cuenta no esta confirmada");
            RedirectManager.RedirectTo("Account/Lockout");
        }
        else
        {
            errorMessage = "Error: Intento de logeo no valido";
        }
    }
    

}