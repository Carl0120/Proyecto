namespace Security.Models;

public class AppPermissions 
{
    public required string Id { get; set;}
    
    public required string Name { get; set; }
    
    public required string NormalizedName { get; set; }

    public ICollection<AppRole> Roles { get; set; } = new List<AppRole>();
}