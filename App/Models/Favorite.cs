using App.Models.Properties;

namespace App.Models;

public class Favorite
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public User? User { get; set; } = null;

    public Guid PropertyId { get; set; }
    public Property? Property { get; set; } = null;
    
}