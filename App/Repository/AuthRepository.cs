using System.Security.Cryptography;
using System.Text;
using App.Abstractions;
using App.Database;
using App.Models;
using App.PasswordHasher;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly UserDbContext _dbContext;
    private readonly JwtProvider _jwtProvider;
    private readonly ILogger<AuthRepository> _logger;
    private readonly IPasswordHash _passwordHash;
    public AuthRepository(UserDbContext dbContext, ILogger<AuthRepository> logger, IPasswordHash passwordHash, JwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _logger = logger;
        _passwordHash = passwordHash;
        _jwtProvider = jwtProvider;
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
                PasswordHash = _passwordHash.Generate(registerDto.Password),
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                Time = DateTime.UtcNow
            };
        
        
            await _dbContext.Users.AddAsync(user);
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"User with login {registerDto.Login} registered successfully");
    }
    
    
    public async Task<string> LoginUser(LoginDto loginDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == loginDto.Login);
        
        if(user == null || !_passwordHash.Verify(loginDto.Password, user.PasswordHash))
        {
            _logger.LogError($"Invalid login or password for user {loginDto.Login}");
            throw new Exception("Invalid login or password");
        }
        
        var token = _jwtProvider.GenerateToken(user);
        
        _logger.LogInformation($"User {loginDto.Login} logged in successfully");
        
        return token;
    }

   
   

}