using App.Enums;

namespace App.Models.Properties;

public class Property
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public PropertyType Type { get; set; } // Enum: Apartment, Room, etc.
    
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    
    public PropertyDetails? Details { get; set; }  // Ссылка на параметры
    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();

    public List<Favorite> Favorites { get; set; } = new List<Favorite>();

}