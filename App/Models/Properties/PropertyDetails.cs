namespace App.Models.Properties;

public class PropertyDetails
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public double Area { get; set; } // площадь
    public int Rooms { get; set; } // количество комнат
    public int Floor { get; set; } // этаж
    public int TotalFloors { get; set; }
    public bool HasBalcony { get; set; }
    public bool HasFurniture { get; set; }
    
    public Guid PropertyId { get; set; }
    public Property? Property { get; set; }
}