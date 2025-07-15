using App.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/favorite")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteController(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddFavorite([FromQuery] Guid userId, [FromQuery] Guid propertyId)
    {
        await _favoriteRepository.AddFavorite(propertyId, userId);
        return Ok(new { message = "Dodano do ulubionych" });
    }
    
    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFavorite([FromQuery] Guid userId, [FromQuery] Guid propertyId)
    {
        await _favoriteRepository.RemoveFavorite(propertyId, userId);
        return Ok(new { message = "Usunieta z ulubionych" });
    }
    
    [HttpGet("byIds")]
    public async Task<IActionResult> GetPropertiesByIds([FromQuery] List<Guid> ids)
    {
        var properties = await _favoriteRepository.GetPropertiesByIds(ids);
        return Ok(properties);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetFavoritesByUser(Guid userId)
    {
        var favorites = await _favoriteRepository.GetFavoritesByUserId(userId);
        return Ok(favorites);
    }
}