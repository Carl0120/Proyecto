using Microsoft.AspNetCore.Identity;

namespace Security.Models;

public class AppRole : IdentityRole
{
    public ICollection<AppPermissions> Permissions { get; set; } = new List<AppPermissions>();

}