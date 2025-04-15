namespace App.Abstractions;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
    string GetCurrentUserRole();
}