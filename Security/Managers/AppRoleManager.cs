using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Security.Models;

namespace Security.Managers;

public class AppRoleManager(
    IRoleStore<AppRole> store,
    IEnumerable<IRoleValidator<AppRole>> roleValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    ILogger<RoleManager<AppRole>> logger)

    : RoleManager<AppRole>(store, roleValidators, keyNormalizer, errors, logger)
{
    
};