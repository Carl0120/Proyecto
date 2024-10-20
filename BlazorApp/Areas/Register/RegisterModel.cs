using System.ComponentModel.DataAnnotations;
using Security.Models;

namespace BlazorApp.Areas.Register;

public class RegisterModel
{
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";
        
        public string Name { get; set; }
    
        public string LastName { get; set; }
    
        public string UserName { get; set; }
        
        public string ConfirmedPasword { get; set; }
    
        public string RoleId { get; set; }
    
        public string WorkAreaId { get; set; }
        
}