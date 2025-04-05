using App.Abstractions;
using App.Contracts;
using App.Database;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(UserDbContext dbContext, ILogger<UserRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
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
        var user = await _dbContext.Users.Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Name, response.Name)
                .SetProperty(u => u.LastName, response.LastName));
        
        _logger.LogInformation($"Update user with id {id}");
        return id;    
    }

    public async Task<Guid> DeleteUser(Guid id)
    {
        var deletedRow = await _dbContext.Users.Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        if(deletedRow == 0)
        {
            _logger.LogError($"User with id {id} not found");
        }
        
        _logger.LogInformation($"Delete user with id {id}");
        return id;
    }
}