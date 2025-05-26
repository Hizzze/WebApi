namespace App.Models.PropertyDtos;

public class PropertyDetailsDto
{
    public double Area { get; set; }
    public int Rooms { get; set; }
    public int Floor { get; set; }
    public int TotalFloors { get; set; }
    public bool HasBalcony { get; set; }
    public bool HasFurniture { get; set; }
}