using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Areas.Login;

public class LoginModel
{
    [Required] [EmailAddress] public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Display(Name = "Recuerdame?")] public bool RememberMe { get; set; } 
}