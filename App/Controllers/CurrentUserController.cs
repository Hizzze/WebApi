using App.Abstractions;
using App.Models;
using App.Models.PropertyDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/user/me")]
public class CurrentUserController : ControllerBase
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;

    public CurrentUserController(ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetUserById(userId); 

        if (user == null)
        {
            return NotFound("User not found");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Login = user.Login,
        };

        return Ok(userDto);
    }
    
    [HttpGet("my-properties")]
    [Authorize]
    public async Task<ActionResult<List<PropertyDto>>> GetMyProperties()
    {
        var properties = await _userRepository.GetProperties();
        return Ok(properties);
    }

}