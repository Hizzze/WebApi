using App.Contracts;
using App.Models;
using App.Models.PropertyDtos;
using Microsoft.AspNetCore.Mvc;

namespace App.Abstractions;

public interface IUserRepository
{
    Task<List<User>> GetUsers();
    Task<User?> GetUserById(Guid id);
    Task<ActionResult<List<PropertyDto>>> GetProperties();
    Task<User> CreateUser([FromBody] UserResponse response);
    Task<Guid> UpdateUser(Guid id, [FromBody] UserResponse response);
    Task<Guid> DeleteUser(Guid targetUserId, Guid currentUserId);
}