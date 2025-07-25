using App.Enums;

namespace App.Models.PropertyDtos;

public class PropertyCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public PropertyType Type { get; set; }

    public PropertyDetailsDto Details { get; set; } = new();
    public ContactDto? Contact { get; set; } 
}