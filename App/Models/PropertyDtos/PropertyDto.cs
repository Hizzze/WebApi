using App.Enums;

namespace App.Models.PropertyDtos;

public class PropertyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public PropertyType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid OwnerId { get; set; }

    public PropertyDetailsDto Details { get; set; } = new();
    public ContactDto? Contact { get; set; }

    public List<PropertyImageDto> Images { get; set; } = new();
}