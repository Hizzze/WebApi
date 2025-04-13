using App.Abstractions;
using App.Database;
using App.Extentions;
using App.PasswordHasher;
using App.Repository;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var jwtSection = builder.Configuration.GetSection(nameof(JwtOptions));
var jwtOptions = jwtSection.Get<JwtOptions>();

builder.Services.AddApiAuthentication(Microsoft.Extensions.Options.Options.Create(jwtOptions));
builder.Services.Configure<JwtOptions>(jwtSection);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<UserDbContext>(opt => opt
    .UseNpgsql(builder.Configuration.GetConnectionString(nameof(UserDbContext))));


builder.Logging.ClearProviders();
builder.Host.UseNLog();


var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

NLog.LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<JwtProvider>();
builder.Services.AddScoped<IPasswordHash, PasswordHash>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
});


app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.Run();

