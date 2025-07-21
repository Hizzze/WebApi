using App.Abstractions;
using App.Database;
using App.Models;
using App.Models.Properties;
using App.Models.PropertyDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class PropertiesRepository : IPropertiesRepository
{
    private readonly UserDbContext _dbContext;
    private readonly ILogger<PropertiesRepository> _logger;
    private readonly ICurrentUserService _currentUserService;

    public PropertiesRepository(
        UserDbContext dbContext,
        ILogger<PropertiesRepository> logger,
        ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _currentUserService = currentUserService;
    }


    public async Task<List<PropertyDto>> GetAll()
    {
        var properties = await _dbContext.Properties
            .Include(p => p.Details)
            .Include(p => p.Images)
            .ToListAsync();

        return properties.Select(MapToDto).ToList();
    }
    
    public async Task<PropertyDto> GetPropertyById(Guid id)
    {
        var property = await _dbContext.Properties
            .Include(p => p.Details)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);
        _logger.LogInformation("Fetching property with ID: {Id}", id);
        return property is null ? null : MapToDto(property);
    }

    [HttpPost]
    public async Task<PropertyDto> Create(
        [FromForm] PropertyCreateDto dto,
        [FromForm] List<IFormFile> images)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var property = new Property
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Location = dto.Location,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            OwnerId = userId,
            Type = dto.Type,
            ContactEmail = dto.Contact?.Email,
            ContactPhone = dto.Contact?.Phone,
            Details = new PropertyDetails
            {
                Area = dto.Details.Area,
                Rooms = dto.Details.Rooms,
                Floor = dto.Details.Floor,
                TotalFloors = dto.Details.TotalFloors,
                HasBalcony = dto.Details.HasBalcony,
                HasFurniture = dto.Details.HasFurniture
            }
        };

        _dbContext.Properties.Add(property);
        await _dbContext.SaveChangesAsync();
        
        
        
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        foreach (var image in images)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imageEntity = new PropertyImage
            {
                PropertyId = property.Id,
                ImageUrl = "/uploads/" + fileName
            };

            _dbContext.PropertyImages.Add(imageEntity);
        }

        await _dbContext.SaveChangesAsync();

        var propertyWithImages = await _dbContext.Properties
            .Include(p => p.Images)
            .Include(p => p.Details)
            .FirstOrDefaultAsync(p => p.Id == property.Id);
        
        return MapToDto(propertyWithImages);
    }


[HttpPut]
    public async Task<PropertyDto> Update(Guid id, PropertyCreateDto dto)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var property = await _dbContext.Properties
            .Include(p => p.Details)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
            throw new Exception("Property not found");

        if (property.OwnerId != userId)
            throw new UnauthorizedAccessException("You don't have permission to update this property");

        property.Title = dto.Title;
        property.Description = dto.Description;
        property.Price = dto.Price;
        property.Location = dto.Location;
        property.Latitude = dto.Latitude;
        property.Longitude = dto.Longitude;
        property.Type = dto.Type;
        property.ContactEmail = dto.Contact?.Email;
        property.ContactPhone = dto.Contact?.Phone;
        
        property.Details.Area = dto.Details.Area;
        property.Details.Rooms = dto.Details.Rooms;
        property.Details.Floor = dto.Details.Floor;
        property.Details.TotalFloors = dto.Details.TotalFloors;
        property.Details.HasBalcony = dto.Details.HasBalcony;
        property.Details.HasFurniture = dto.Details.HasFurniture;
        

        await _dbContext.SaveChangesAsync();
        return MapToDto(property);
    }

    
    
    public async Task<bool> Delete(Guid id)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var property = await _dbContext.Properties
            .Include(p => p.Details)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
            throw new Exception("id not found");

        if (property.OwnerId != userId)
            throw new UnauthorizedAccessException("You don't have permission to delete this property");

        
        _dbContext.Properties.Remove(property);

        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    private PropertyDto MapToDto(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Price = property.Price,
            Location = property.Location,
            Type = property.Type,
            CreatedAt = property.CreatedAt,
            OwnerId = property.OwnerId,
            Contact = new ContactDto
            {
                Email = property.ContactEmail,
                Phone = property.ContactPhone
            },
            Details = new PropertyDetailsDto
            {
                Area = property.Details?.Area ?? 0,
                Rooms = property.Details?.Rooms ?? 0,
                Floor = property.Details?.Floor ?? 0,
                TotalFloors = property.Details?.TotalFloors ?? 0,
                HasBalcony = property.Details?.HasBalcony ?? false,
                HasFurniture = property.Details?.HasFurniture ?? false
            },
            Images = property.Images?.Select(img => new PropertyImageDto
            {
                ImageUrl = img.ImageUrl
            }).ToList() ?? new List<PropertyImageDto>() 
        };
    }
}