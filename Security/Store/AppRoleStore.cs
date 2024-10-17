using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Security.Models;

namespace Security.Store;

public class AppRoleStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<AppRole>(context, describer)
{
    
};