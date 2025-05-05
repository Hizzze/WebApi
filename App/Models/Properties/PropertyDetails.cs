namespace App.Models.Properties;

public class PropertyDetails
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public double Area { get; init; } // площадь
    public int Rooms { get; init; } // количество комнат
    public int Floor { get; init; } // этаж
    public int TotalFloors { get; init; }
    public bool HasBalcony { get; init; }
    public bool HasFurniture { get; init; }
    
    public Guid PropertyId { get; init; }
    public Property? Property { get; init; }
}