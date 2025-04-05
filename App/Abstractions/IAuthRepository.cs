using App.Models;

namespace App.Abstractions;

public interface IAuthRepository
{
    public Task RegisterUser(RegisterDto registerDto);
    public Task LoginUser(LoginDto loginDto);
}