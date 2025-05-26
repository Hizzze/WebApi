using App.Enums;

namespace App.Models.PropertyDtos;

public class PropertyCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public PropertyType Type { get; set; }

    public PropertyDetailsDto Details { get; set; } = new();
}