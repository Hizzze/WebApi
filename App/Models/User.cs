using App.Enums;
using App.Models.Properties;

namespace App.Models;

public class User
{
    public Guid Id { get; init; }
    
    public string? Login { get; init; } = string.Empty;
    public string? PasswordHash { get; init; } = string.Empty;
    public UserRole? Role { get; set; } = UserRole.User;
    public string? Name { get; init; } = string.Empty;
    public string? LastName { get; init; } = string.Empty;
    public DateTime? Time { get; init; } = DateTime.Now;
    
    public List<Favorite> Favorites { get; init; } = new List<Favorite>();
    
    public List<Property> Properties { get; init; } = new List<Property>();
}