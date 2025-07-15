using App.Abstractions;
using App.Database;
using App.Models;
using App.Models.Properties;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly UserDbContext _dbContext;
    private readonly ILogger<FavoriteRepository> _logger;
    
    public FavoriteRepository(UserDbContext dbContext, ILogger<FavoriteRepository> logger)
    {
        _dbContext  = dbContext;
        _logger = logger;
    }

    public async Task AddFavorite(Guid propertyId, Guid userId)
    {
        var favorite = new Favorite()
        {
            PropertyId = propertyId,
            UserId = userId
        };
        
        _logger.LogInformation($"Add favorite for property with id {propertyId} and user with id {userId}");
        await _dbContext.Favorites.AddAsync(favorite);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task RemoveFavorite(Guid propertyId, Guid userId)
    {
        var favorite = await _dbContext.Favorites
            .FirstOrDefaultAsync(f => f.PropertyId == propertyId && f.UserId == userId);

        if (favorite != null)
        {
            _logger.LogInformation($"Remove favorite for property with id {propertyId} and user with id {userId}");
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Property>> GetPropertiesByIds(List<Guid> ids)
    {
        var properties = await _dbContext.Properties
            .Where(p => ids.Contains(p.Id))
            .Include(p => p.Images)
            .Include(p => p.Details)
            .ToListAsync();

        return properties;
    }
    
    public async Task<List<Favorite>> GetFavoritesByUserId(Guid userId)
    {
        return await _dbContext.Favorites
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }
}