using App.Abstractions;
using App.Database;
using App.Enums;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly UserDbContext _dbContext;
    private readonly ILogger<AdminRepository> _logger;

    public AdminRepository(UserDbContext dbContext, ILogger<AdminRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task ChangeRole(ChangeRoleDto changeRole)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == changeRole.Id);
    
        if (user == null)
        {
            _logger.LogError($"User with id {changeRole.Id} not found");
            throw new Exception($"User with id {changeRole.Id} not found");
        }
        
        
        
        if (!Enum.TryParse<UserRole>(changeRole.NewRole, true, out var newRole) ||
            !Enum.IsDefined(typeof(UserRole), newRole))
        {
            _logger.LogError($"Invalid role: {changeRole.NewRole}");
            throw new Exception($"Invalid role: {changeRole.NewRole}");
        }

        if (newRole == UserRole.Owner)
        {
            var existingOwner = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Role == UserRole.Owner);

            if (existingOwner != null)
            {
                _logger.LogError("Attempt to create second Owner");
                throw new Exception("Attempt to create second Owner");
            }
        }

        user.Role = newRole;

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"User with id {changeRole.Id} changed role to {changeRole.NewRole}");
    }

    
}
