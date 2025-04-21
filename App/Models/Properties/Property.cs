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

}