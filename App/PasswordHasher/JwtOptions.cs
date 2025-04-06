namespace App.PasswordHasher;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public int ExpieresHours { get; set; }
}