using System.Security.Cryptography;
using System.Text;
using App.Abstractions;
using App.Database;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly UserDbContext _dbContext;
    private readonly ILogger<AuthRepository> _logger;
    public AuthRepository(UserDbContext dbContext, ILogger<AuthRepository> logger) 
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task RegisterUser(RegisterDto registerDto)
    {
        var userExists = await _dbContext.Users.AnyAsync(u => u.Login == registerDto.Login);

        if (userExists)
        {
            _logger.LogError($"User with login {registerDto.Login} already exists");
            throw new Exception("User with this login already exists");
        }

        if (registerDto.Password != null)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = registerDto.Login,
                PasswordHash = HashPassword(registerDto.Password),
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                Time = DateTime.UtcNow
            };
        
        
            await _dbContext.Users.AddAsync(user);
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"User with login {registerDto.Login} registered successfully");
    }
    
    
    public async Task LoginUser(LoginDto loginDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == loginDto.Login);
        
        if(user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            _logger.LogError($"Invalid login or password for user {loginDto.Login}");
            throw new Exception("Invalid login or password");
        }
        
        _logger.LogInformation($"User {loginDto.Login} logged in successfully");
    }
    
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
    
    private static bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }

}