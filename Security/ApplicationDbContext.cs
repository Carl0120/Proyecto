using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Security.Models;

namespace Security;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<AppUser,AppRole,string>(options)
{
    public DbSet<AppPermissions> Permissions { get; set;}
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<AppUser>()
            .ToTable("APP_SEC_USERS");
        builder.Entity<AppRole>()
            .ToTable("APP_SEC_ROLES");
        builder.Entity<IdentityUserRole<string>>()
            .ToTable("APP_SEC_USER_ROLES");
        builder.Entity<IdentityUserLogin<string>>()
            .ToTable("APP_SEC_USER_LOGINS");
        builder.Entity<IdentityUserClaim<string>>()
            .ToTable("APP_SEC_USER_CLAIMS");
        builder.Entity<IdentityRoleClaim<string>>()
            .ToTable("APP_SEC_ROLES_CLAIMS");
        builder.Entity<IdentityUserToken<string>>()
            .ToTable("APP_SEC_USER_TOKEN");
        builder.Entity<AppPermissions>()
            .ToTable("APP_SEC_PERMISSIONS");
        
        builder.Entity<AppRole>().HasMany(e => e.Permissions)
            .WithMany(e => e.Roles).UsingEntity("APP_SEC_ROLE_PERMISSIONS");
    }
}