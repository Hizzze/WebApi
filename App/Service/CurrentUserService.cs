using System.Security.Claims;
using App.Abstractions;

namespace App.Service;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var id = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;

        return Guid.TryParse(id, out var guid)
            ? guid
            : throw new Exception("Invalid or missing user ID");
    }

    public string GetCurrentUserRole()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value 
               ?? throw new Exception("User role not found");
    }
}