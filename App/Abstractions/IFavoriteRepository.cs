using App.Models;
using App.Models.Properties;

namespace App.Abstractions;

public interface IFavoriteRepository
{
    Task AddFavorite(Guid propertyId, Guid userId);
    Task RemoveFavorite(Guid propertyId, Guid userId);
    Task<List<Property>> GetPropertiesByIds(List<Guid> ids);
    Task<List<Favorite>> GetFavoritesByUserId(Guid userId);
}