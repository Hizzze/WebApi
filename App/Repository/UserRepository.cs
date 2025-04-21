using System.Security.Claims;
using App.Abstractions;
using App.Contracts;
using App.Database;
using App.Enums;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;
    private readonly ICurrentUserService _currentUserService;

    public UserRepository(UserDbContext dbContext, ILogger<UserRepository> logger, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _currentUserService = currentUserService;

    }

    public async Task<List<User>> GetUsers()
    {
        var users= await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
        
        if(users.Count == 0) _logger.LogError("Users not found");
        
        _logger.LogInformation("Get all users");
        return users;

    }
    

    public async Task<User?> GetUserById(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            _logger.LogError($"User with id {id} not found");
            return null; 
        }

        _logger.LogInformation($"Get user with id {id}");
        return user;
    }

    public async Task<User> CreateUser([FromBody] UserResponse response)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = response.Name,
            LastName = response.LastName,
            Time = DateTime.UtcNow
        };
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Create user with id {user.Id}");

        return user;
    }

    public async Task<Guid> UpdateUser(Guid id,[FromBody] UserResponse response)
    {
        
        var currentUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == _currentUserService.GetCurrentUserId());
        var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        await _dbContext.Users.Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Name, response.Name)
                .SetProperty(u => u.LastName, response.LastName));

        
        if(currentUser?.Role == UserRole.Admin && targetUser?.Role != UserRole.User)
        {
            _logger.LogError($"Admin with id {currentUser.Id} cannot update another admin or owner");
            throw new Exception("Admins can only update Users");
        }
        
        if(currentUser?.Role == UserRole.Owner && targetUser?.Role == UserRole.Owner)
        {
            _logger.LogError($"Owner with id {currentUser.Id} cannot update another owner");
            throw new Exception("Owners can only update Users and Admins");
        }
        
        _logger.LogInformation($"Update user with id {id}");
        return id;    
    }

    public async Task<Guid> DeleteUser(Guid targetUserId, Guid currentUserId)
    {
        var currentUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);
        var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == targetUserId);
        
        if (targetUser == null || currentUser == null)
        {
            _logger.LogError($"User with id {targetUserId} not found");
            throw new Exception("User not found");
        }
        
        if(currentUser.Role == UserRole.User)
        {
            _logger.LogError($"User with id {currentUserId} is not admin");
            throw new Exception("You are not admin");
        }
        
        if(currentUser.Role == UserRole.Admin && targetUser.Role != UserRole.User)
        {
            _logger.LogError($"Admin with id {currentUserId} cannot delete another admin");
            throw new Exception("Admins can only delete Users");
        }
        
        if(currentUser.Role == UserRole.Owner && targetUser.Role == UserRole.Owner)
        {
            _logger.LogError($"Owner with id {currentUserId} cannot delete another owner");
            throw new Exception("Owners can delete Users and Admins");
        }

        _dbContext.Users.Remove(targetUser);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Delete user with id {targetUserId}");
        return targetUserId;
    }
    
 
}