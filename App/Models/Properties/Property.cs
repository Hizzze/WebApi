using App.Enums;

namespace App.Models.Properties;

public class Property
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Location { get; init; } = string.Empty;
    
    public Guid OwnerId { get; init; }
    public User? Owner { get; init; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public PropertyType Type { get; init; } // Enum: Apartment, Room, etc.
    
    public PropertyDetails? Details { get; init; }  // Ссылка на параметры
}