using System.Security.Cryptography;
using System.Text;
using App.Abstractions;
using App.Database;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
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
        if (HttpContext.User.Identity?.IsAuthenticated == true)
        {
            return BadRequest("User is already logged in");
        }
        
    
       var token =  await _authRepository.LoginUser(loginDto);
       
        HttpContext.Response.Cookies.Append("cookies", token);
       
       return Ok(new {message = "User logged in successfully"});
    }
    

    
}