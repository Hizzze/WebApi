using App.Models;

namespace App.Abstractions;

public interface IAdminRepository
{
    public Task ChangeRole(ChangeRoleDto changeRole);
}