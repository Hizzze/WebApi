using App.Abstractions;
using App.Enums;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;


[ApiController]
[Route("api/admin")]

public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;

    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    
    [HttpPost("change-role")]
    [Authorize(Roles = "Owner")]
    public async Task<string> ChangeRole([FromBody] ChangeRoleDto changeRole)
    {
        await _adminRepository.ChangeRole(changeRole);
        
        return changeRole.NewRole;
    }

}