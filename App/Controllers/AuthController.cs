using System.Security.Cryptography;
using System.Text;
using App.Abstractions;
using App.Database;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserDbContext _dbContext;
    private readonly IAuthRepository _authRepository;

    public AuthController(UserDbContext dbContext, IAuthRepository authRepository)
    {
        _authRepository = authRepository;
        _dbContext = dbContext;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
    {
        await _authRepository.RegisterUser(registerDto);
        return Ok("User registered successfully");  
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
    {
        await _authRepository.LoginUser(loginDto);
        return Ok("Login successful");
    }
    
}