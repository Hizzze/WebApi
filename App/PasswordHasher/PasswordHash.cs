using App.Abstractions;

namespace App.PasswordHasher;

public class PasswordHash : IPasswordHash
{
    public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}