using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Security.Models;

namespace Security.Store;

public class AppUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : UserStore<AppUser>(context, describer)
{
    
};