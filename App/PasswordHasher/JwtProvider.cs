using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;
using App.Abstractions;
using App.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.PasswordHasher;

public class JwtProvider
{
    private readonly JwtOptions _options;
    
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    public string GenerateToken(User user)
    {
        Claim[] claims = 
        [
            new("Id", user.Id.ToString()),
            new("Admin", "true")
        ];
        var singningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: singningCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpieresHours));

        var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenHandler;
    }
}