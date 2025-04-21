using App.Abstractions;
using App.Contracts;
using App.Database;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace App.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    

    public UserController(UserDbContext dbContext, IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        var usersList = await _userRepository.GetUsers();
        return Ok(usersList);
    }
    

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User?>> GetById(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
        
        if(user == null) return NotFound();
      

        return Ok(user);
    }


    [HttpPost]
    [Authorize(Roles = "Admin, Owner")]
    public async Task<ActionResult<User>> Create([FromBody] UserResponse response)
    { 
        var user = await _userRepository.CreateUser(response);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, Guid.NewGuid()); // HTTP 201 + ссылка на объект
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin, Owner")]
    public async Task<ActionResult<Guid>> Update(Guid id, [FromBody]UserResponse response)
    {
        var userExists = await _dbContext.Users.AnyAsync(u => u.Id == id);

        if (!userExists)
        {
            return NotFound();
        }
        
        await _userRepository.UpdateUser(id, response);
        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Owner")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    { 
        var currentUserId = _currentUserService.GetCurrentUserId();
        
      await _userRepository.DeleteUser(id, currentUserId);
      return NoContent();
    }
}